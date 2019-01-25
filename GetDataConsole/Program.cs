using System.Threading.Tasks;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.SharePoint.Client;
using System.Security;
using Salesforce.Common;
using Salesforce.Force;
using System.Collections.Generic;
using System.Diagnostics;
using SalesForce365Library;
using SalesForce365Library.Models;
using SalesForce365Library.Models.Salesforce;
using Salesforce.Common.Models.Json;
using log4net;

namespace GetDataConsole
{
    class Program
    {
        private static readonly string SharepointUrl = ConfigurationManager.AppSettings["SharepointUrl"];
        private static readonly string SharepointUser = ConfigurationManager.AppSettings["SharepointUserName"];
        private static readonly string SharepointPassword = ConfigurationManager.AppSettings["SharepointPassword"]; 
        private static readonly string SalesforceRecordTypeIdList = ConfigurationManager.AppSettings["SalesforceRecordTypeIdList"];
        private static readonly string RecordTypeArray = ConfigurationManager.AppSettings["RecordTypeArray"];
        private static readonly string DownloadedFilePath = ConfigurationManager.AppSettings["DownloadedFilePath"];
        private static readonly string DeleteSFAttachment = ConfigurationManager.AppSettings["DeleteAttachmentFromSF"];

        static void Main(string[] args)
        {
            //clean up. Take out try catch here and put it in HandleSalesforceAttachment
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                HandleSalesForceAttachmentAsync().Wait();
                Console.WriteLine("Process Complete");
            }
            catch (Exception ex)
            {
                SharedFunctionality.SendEmail("ERROR: Salesforce to Sharepoint process failed", ex.InnerException.Message);
                SharedFunctionality.Log(ex.ToString());
                Console.WriteLine("Error Occurred. Please check log for more details."); ;
            }
        }

        private static async Task HandleSalesForceAttachmentAsync()
        {
            //log.Info("Starting Process");
            SharedFunctionality.Log("Starting Process");

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            var authClient = await SharedFunctionality.CreateAuthClient();

            //build a KVP of RecordTypeId and Text description. Used to set metadata on uploaded file
            var contentArray = RecordTypeArray.Split('|');

            var contentTypeList = new List<KeyValuePair<string, string>>();

            var contactDocumentIdList = new List<string>();

            for (int x = 0; x < contentArray.Length; x++)
            {
                var array = contentArray[x].ToString().Split(',');
                contentTypeList.Add(new KeyValuePair<string, string>(array[0], array[1]));
            }

            using (var forceClient = SharedFunctionality.GetUserNamePasswordForceClientAsync(authClient))
            {

                //Get all ContentDocumentId's and put into a list for SOQL subquery as part of getting the ContectVersion
                var contactDocuments = await forceClient.QueryAsync<ContentDocumentLink>("SELECT ContentDocumentId, LinkedEntityId FROM ContentDocumentLink WHERE LinkedEntityId in (Select Id FROM Contact WHERE RecordTypeId in (" + SalesforceRecordTypeIdList + "))").ConfigureAwait(false);

                //Due to limitation of the Salesforce Query Language (SOQL) we cannot to a LIKE search on ParentId. We should only get Files for users
                //considered a CPS Contact, whose ID starts with 003
                var filteredDocList = contactDocuments.Records.FindAll(q => (q.LinkedEntityId.StartsWith("003")));

                //wrap ContentDocumentId's in single quotes, needed for subquery later on
                filteredDocList.ForEach(o => contactDocumentIdList.Add("'" + o.ContentDocumentId + "'"));

                SharedFunctionality.Log("total Contact Document IDs returned: " + filteredDocList.Count);

                //convert list to comma separated list
                var contactDocumentIdString = string.Join(",", contactDocumentIdList);

                var attachments = await forceClient.QueryAsync<SalesForce365Library.Models.Salesforce.Attachment>("SELECT id, Name, ParentId FROM Attachment WHERE ParentID in (Select Id FROM Contact WHERE RecordTypeId in (" + SalesforceRecordTypeIdList + ")) ORDER BY CreatedDate DESC LIMIT 1").ConfigureAwait(false);

                var files = await forceClient.QueryAsync<ContentVersion>("SELECT Id, VersionData, PathOnClient, ContentDocumentId FROM ContentVersion WHERE IsLatest = True AND ContentDocumentId IN (" + contactDocumentIdString + ")").ConfigureAwait(false);

                for (var x = 0; x < files.Records.Count; x++)
                {
                    var documentRecord = filteredDocList.Find(d => d.ContentDocumentId == files.Records[x].ContentDocumentId);

                    files.Records[x].ParentId = documentRecord.LinkedEntityId;
                }

                using (System.Net.WebClient wclient = new System.Net.WebClient())
                {
                    wclient.Headers.Add("Authorization: OAuth " + authClient.AccessToken);

                    try
                    {
                        //Create directory that we will download the files to. If it already exists, no action will be taken
                        Directory.CreateDirectory(DownloadedFilePath);
                    }
                    catch (Exception ex)
                    {
                        SharedFunctionality.Log("Error trying to create directory " + DownloadedFilePath);
                        SharedFunctionality.Log(ex.Message);
                        throw ex;
                    }

                    //Due to limitation of the Salesforce Query Language (SOQL) we cannot to a LIKE search on ParentId. We should only get attachments for users
                    //considered a CPS Contact, whose ID starts with 003
                    var filteredIdList = attachments.Records.FindAll(q => (q.ParentId.StartsWith("003")));

                    var mergedList = MergeFilesAttachments(filteredIdList, files, authClient);

                    SharedFunctionality.Log("Found " + mergedList.Count + " records.");

                    //Loop thru records to get body for each file. SF query limitation only returns one body at a time, so we need to make numerous http GET calls
                    for (int i = 0; i < mergedList.Count; i++)
                    {
                        try
                        {
                            SharedFunctionality.Log("Attachment for ID: " + mergedList[i].ParentId);

                            wclient.DownloadFile(mergedList[i].Uri, mergedList[i].FilePath);

                            using (ClientContext context = new ClientContext(SharepointUrl))
                            {
                                var securePwd = new SecureString();
                                foreach (char c in SharepointPassword.ToCharArray()) securePwd.AppendChar(c);

                                context.Credentials = new SharePointOnlineCredentials(SharepointUser, securePwd);

                                //Get contact record to populate metadata
                                var contact = await forceClient.QueryAsync<Contact>("SELECT Birthdate,FirstName,LastName,RecordTypeId,Intake_Date__c FROM Contact WHERE Id = '" + mergedList[i].ParentId + "'");

                                //Program name = EOC/Talent Search/Upward Bound
                                var programName = contentTypeList.First(kvp => kvp.Key == contact.Records[0].RecordTypeId).Value;

                                var list = context.Web.Lists.GetByTitle(programName);

                                context.Load(list.RootFolder);
                                context.ExecuteQuery();

                                var folders = list.RootFolder.Folders;
                                context.Load(folders);
                                context.ExecuteQuery();
                                //only do this if the contact has an intake date
                                if (contact.Records[0].Intake_Date__c.Value != null)
                                {

                                    var intakeYearMonth = contact.Records[0].Intake_Date__c.Value.ToString("yyyyMM", System.Globalization.CultureInfo.GetCultureInfo("en-US"));

                                    var intakeDateFolderUrl = list.RootFolder.ServerRelativeUrl + "/" + intakeYearMonth;
                                    //Check to make sure date folder does not already exist. If it doesn't, error is thrown. Catch it, and create folder
                                    try
                                    {
                                        var checkFolder = context.Web.GetFolderByServerRelativeUrl(intakeDateFolderUrl);
                                        context.ExecuteQuery();
                                    }
                                    catch
                                    {
                                        //Create a parent folder in SP for file
                                        SharedFunctionality.CreateSharepointFolder(context, list, intakeYearMonth, list.RootFolder.ServerRelativeUrl);
                                    }

                                    //Url of root folder for contact ID subfolders
                                    var contactFolderParentUrl = list.RootFolder.ServerRelativeUrl + "/" + intakeYearMonth;
                                    //Url of contact ID subfolder
                                    var contactFolderUrl = list.RootFolder.ServerRelativeUrl + "/" + intakeYearMonth + "/" + mergedList[i].ParentId;

                                    //Check to make sure folder does not already exist. If it doesn't, error is thrown. Catch it, and create folder
                                    try
                                    {
                                        var checkFolder = context.Web.GetFolderByServerRelativeUrl(contactFolderUrl);
                                        context.ExecuteQuery();

                                        //if this folder exists, set the variable FolderRelativeUrl in the shared class
                                        SharedFunctionality.SetContactFolderUrl(contactFolderUrl);
                                    }
                                    catch
                                    {
                                        //Create a folder in SP for file
                                        SharedFunctionality.CreateContactSharepointFolder(context, list, contactFolderParentUrl, mergedList[i].ParentId);
                                    }

                                    //Check to make sure file does not already exist. If it doesn't, process upload to Sharepoint
                                    var checkFile = context.Web.GetFileByServerRelativeUrl(contactFolderUrl + "/" + mergedList[i].FileName);

                                    context.Load(checkFile, fe => fe.Exists);
                                    context.ExecuteQuery();

                                    if (!checkFile.Exists)
                                    {
                                        //upload file to Sharepoint
                                        var fileLink = SharepointUrl + programName + "/" + intakeYearMonth + "/" + mergedList[i].ParentId + "/" + mergedList[i].FileName;

                                        using (FileStream fs = new FileStream(mergedList[i].FilePath, FileMode.Open))
                                        {
                                            SharedFunctionality.UploadSalesforceFileToSharepoint(context, contact, fs, mergedList[i], list);

                                            //Add new Image object to Salesforce contact
                                            SharedFunctionality.AddLinkToSalesforce(forceClient, mergedList[i], fileLink);
                                        }

                                        if (DeleteSFAttachment != "false")
                                        {
                                            await DeleteSalesforceAttachmentAsync(forceClient, mergedList[i].Id);
                                        }
                                    }
                                    else
                                    {
                                        SharedFunctionality.Log("File " + contactFolderParentUrl + "/" + mergedList[i].FileName + " exists.");
                                    }
                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            SharedFunctionality.Log("Error trying to upload " + mergedList[i].FileName);
                            SharedFunctionality.Log(ex.Message);
                            throw ex;
                        }
                    }
                }
            }

            try
            {
                //After looping thru records, delete the downloaded files from the DownloadedFilePath
                var di = new DirectoryInfo(DownloadedFilePath);
                SharedFunctionality.Log("Deleting files in " + DownloadedFilePath);
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    SharedFunctionality.Log("Deleting " + file.FullName);
                    file.Delete();
                }
                SharedFunctionality.Log("All files deleted");
            }
            catch (Exception ex)
            {
                SharedFunctionality.Log("Error trying to delete all files in folder " + DownloadedFilePath);
                SharedFunctionality.Log(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Deletes attachment from Salesforce once the attachment has been added to Sharepoint and Image link put on Contact record
        /// </summary>
        private static async Task DeleteSalesforceAttachmentAsync(ForceClient client, string attachmentId)
        {
            await client.DeleteAsync("Attachment", attachmentId);
        }

        /// <summary>
        /// Merges Salesforce Attachment objects and ContentVersion objects into a single list of a custom SharepointFile object
        /// </summary>
        /// <returns>List of SharepointFile objects</returns>
        private static List<SharepointFile> MergeFilesAttachments(List<SalesForce365Library.Models.Salesforce.Attachment> attachments, QueryResult<ContentVersion> files, AuthenticationClient authClient)
        {
            var mergedList = new List<SharepointFile>();

            mergedList = attachments.ConvertAll(x => new SharepointFile
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId,
                FileName = x.Name,
                FilePath = DownloadedFilePath + x.Name,
                Uri = authClient.InstanceUrl + "/services/data/" + authClient.ApiVersion + "/sobjects/Attachment/" + x.Id + "/Body"
            });

            //Add the ContentVersion file attachments to list
            mergedList.AddRange(files.Records.ConvertAll(x => new SharepointFile
            {
                Id = x.Id,
                Name = x.PathOnClient,
                ParentId = x.ParentId,
                FileName = x.PathOnClient,
                FilePath = DownloadedFilePath + x.PathOnClient,
                Uri = authClient.InstanceUrl + x.VersionData
            }));


            return mergedList;
        }



    }
}
