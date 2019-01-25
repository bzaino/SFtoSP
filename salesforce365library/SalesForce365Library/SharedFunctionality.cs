using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Salesforce.Force;
using Salesforce.Common;
using Salesforce.Common.Models.Json;
using System.Configuration;
using SalesForce365Library.Models;
using SalesForce365Library.Models.Salesforce;
using Microsoft.SharePoint.Client;
using System.Security;
using System.Net.Mail;

namespace SalesForce365Library
{
    public static class SharedFunctionality
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string SalesforceConsumerKey = ConfigurationManager.AppSettings["SalesforceConsumerKey"];
        private static readonly string SalesforceConsumerSecret = ConfigurationManager.AppSettings["SalesforceConsumerSecret"];
        private static readonly string SalesforceUserName = ConfigurationManager.AppSettings["SalesforceUserName"];
        private static readonly string SalesforcePassword = ConfigurationManager.AppSettings["SalesforcePassword"];
        private static readonly string SalesforceSecurityToken = ConfigurationManager.AppSettings["SalesforceSecurityToken"];
        private static readonly string SalesforceSandboxURL = ConfigurationManager.AppSettings["SalesforceSandboxURL"];
        private static readonly string SalesforceProdURL = ConfigurationManager.AppSettings["SalesforceProdURL"];
        private static readonly string SalesforceRecordTypeIdList = ConfigurationManager.AppSettings["SalesforceRecordTypeIdList"];
        private static readonly string IsUsingSFSandbox = ConfigurationManager.AppSettings["IsUsingSFSandbox"];
        private static readonly string RecordTypeArray = ConfigurationManager.AppSettings["RecordTypeArray"];
        private static readonly string SharepointUrl = ConfigurationManager.AppSettings["SharepointUrl"];
        private static readonly string SharepointUser = ConfigurationManager.AppSettings["SharepointUser"];
        private static readonly string SharepointPassword = ConfigurationManager.AppSettings["SharepointPassword"];
        private static readonly string SharepointLibraryName = ConfigurationManager.AppSettings["SharepointLibraryName"];
        private static readonly string EmailAddress = ConfigurationManager.AppSettings["NotificationEmailAddress"];
        private static readonly string MailServer = ConfigurationManager.AppSettings["SMTPServer"];
        private static readonly string MailServerPort = ConfigurationManager.AppSettings["SMTPServerPort"];

        private static string FolderRelativeUrl = String.Empty;


        public static async Task<AuthenticationClient> CreateAuthClient()
        {
            var authenticationClient = new AuthenticationClient();

            string tokenUrl = SalesforceSandboxURL;

            //Use prod instead of sandbox
            if (IsUsingSFSandbox != "true")
                tokenUrl = SalesforceProdURL;

            await authenticationClient.UsernamePasswordAsync(SalesforceConsumerKey, SalesforceConsumerSecret, SalesforceUserName, SalesforcePassword, tokenUrl);

            return authenticationClient;
        }

        /// <summary>
        /// Gets a ForceClient that has been authenticated using the UserName, Password, and SecurityToken settings
        /// specified in the config file.
        /// </summary>
        /// <returns>The authenticated ForceClient.</returns>
        public static ForceClient GetUserNamePasswordForceClientAsync(AuthenticationClient authclient)
        {
            using (authclient)
            {
                return new ForceClient(
                    authclient.InstanceUrl,
                    authclient.AccessToken,
                    authclient.ApiVersion);
            }
        }

        /// <summary>
        /// Creates folder in Sharepoint with a name of the Contact ID. All attachments for that user will go into this folder
        /// </summary>
        /// <returns></returns>
        public static void CreateContactSharepointFolder(ClientContext ctx, List list, string parentFolderUrl, string contactId)
        {
            //Set Folder URL for later use
            SetContactFolderUrl(parentFolderUrl + "/" + contactId);

            CreateSharepointFolder(ctx, list, contactId, parentFolderUrl);
        }

        /// <summary>
        /// Sets a variable that is used later in the process. Needs to be its own method since we can't ensure that 
        /// CreateContactSharepointFolder will always be called
        /// </summary>
        /// <returns></returns>
        public static void SetContactFolderUrl(string folderUrl)
        {
            SetFolderRelativeUrl(folderUrl);
        }

        public static void SetFolderRelativeUrl(string folderUrl)
        {
            FolderRelativeUrl = folderUrl;
        }

        /// <summary>
        /// Creates folder in Sharepoint
        /// </summary>
        /// <returns></returns>
        public static void CreateSharepointFolder(ClientContext ctx, List list, string folderName, string folderUrl)
        {
            var itemCreationInfo = new ListItemCreationInformation();
            itemCreationInfo.UnderlyingObjectType = FileSystemObjectType.Folder;
            itemCreationInfo.LeafName = folderName;
            //itemCreationInfo.FolderUrl must equal the parent folder of the new folder being created
            itemCreationInfo.FolderUrl = folderUrl;
            ListItem folderItemCreated = list.AddItem(itemCreationInfo);
            folderItemCreated.Update();
            ctx.Load(folderItemCreated, f => f.Folder);
            ctx.ExecuteQuery();
        }

        private static ContentType GetContentType(ClientContext ctx, List docs, string contentType)
        {
            ContentTypeCollection listContentTypes = docs.ContentTypes;
            ctx.Load(listContentTypes, types => types.Include(type => type.Id, type => type.Name, type => type.Parent));

            var result = ctx.LoadQuery(listContentTypes.Where(c => c.Name == contentType));
            ctx.ExecuteQuery();

            ContentType targetDocumentSetContentType = result.FirstOrDefault();

            return targetDocumentSetContentType;
        }

        public static void UploadSalesforceFileToSharepoint(ClientContext ctx, QueryResult<Contact> contact, FileStream fs, SharepointFile fileInfo, List list)
        {
            //build a KVP of RecordTypeId and Text description. Used to set metadata on uploaded file
            var contentArray = RecordTypeArray.Split('|');

            var contentTypeList = new List<KeyValuePair<string, string>>();

            for (int x = 0; x < contentArray.Length; x++)
            {
                var array = contentArray[x].ToString().Split(',');
                contentTypeList.Add(new KeyValuePair<string, string>(array[0], array[1]));
            }

            //Program name = EOC/Talent Search/Upward Bound
            var programName = contentTypeList.First(kvp => kvp.Key == contact.Records[0].RecordTypeId).Value;

            var newFile = new FileCreationInformation();
            //use this method to account for files > 2 MB in size
            newFile.ContentStream = fs;
            newFile.Overwrite = true;
            newFile.Url = fileInfo.FileName;

            var contentTypeString = programName + " Document";

            //WHAT IS SHAREPOINT CONTENTTYPE?????????
            ContentType targetContentType = GetContentType(ctx, list, contentTypeString);

            Folder uploadFolder = ctx.Web.GetFolderByServerRelativeUrl(FolderRelativeUrl);

            var uploadFile = uploadFolder.Files.Add(newFile);
            Log("Uploading " + fileInfo.FileName);
            ctx.ExecuteQuery();

            Log("Setting metadata on " + fileInfo.FileName);

            uploadFile.ListItemAllFields.ParseAndSetFieldValue("FirstName", contact.Records[0].FirstName);
            uploadFile.ListItemAllFields.ParseAndSetFieldValue("Last_x0020_Name", contact.Records[0].LastName);
            uploadFile.ListItemAllFields.ParseAndSetFieldValue("Birthday", contact.Records[0].Birthdate.Value.ToShortDateString());
            uploadFile.ListItemAllFields.ParseAndSetFieldValue("ContentTypeId", targetContentType.Id.ToString());
            uploadFile.ListItemAllFields.ParseAndSetFieldValue("Salesforce_x0020_Contact_x0020_ID", fileInfo.ParentId);
            uploadFile.ListItemAllFields.Update();
            ctx.Load(uploadFile);
            ctx.ExecuteQuery();

            Log("Upload of " + fileInfo.FileName + " Complete");
        }

        public static void UploadFileToSharepoint(ClientContext ctx, FileStream fs, string fileName, string contentType)
        {
            var newFile = new FileCreationInformation();
            //use this method to account for files > 2 MB in size
            fs.Seek(0, SeekOrigin.Begin);
            newFile.ContentStream = fs;
            newFile.Overwrite = true;
            newFile.Url = fileName;

            Folder uploadFolder = ctx.Web.GetFolderByServerRelativeUrl(FolderRelativeUrl);

            var uploadFile = uploadFolder.Files.Add(newFile);
            Log("Uploading " + fileName);
            ctx.ExecuteQuery();

            if (contentType != "")
            {
                uploadFile.ListItemAllFields.ParseAndSetFieldValue("ContentType0", contentType);
                uploadFile.ListItemAllFields.Update();
                Log("Setting metadata on " + fileName);

                ctx.Load(uploadFile);
                ctx.ExecuteQuery();
            }

            Log("Upload of " + fileName + " Complete");
        }

        public static void UploadFileBinaryDirect(MemoryStream ms, string fileName)
        {
            using (ClientContext context = new ClientContext(SharepointUrl))
            {
                SecureString securePwd = new SecureString();
                foreach (char c in SharepointPassword.ToCharArray()) securePwd.AppendChar(c);
                securePwd.MakeReadOnly();

                context.Credentials = new SharePointOnlineCredentials(SharepointUser, securePwd);


                // Load the directory
                List list = context.Web.Lists.GetByTitle(SharepointLibraryName);
                context.Load(list.RootFolder);
                context.ExecuteQuery();

                // Upload to the directory
                Microsoft.SharePoint.Client.File.SaveBinaryDirect(context, list.RootFolder.ServerRelativeUrl.ToString() + "/" + fileName, ms, false);

                //Fetch the file to check it in
                Microsoft.SharePoint.Client.File file = context.Web.GetFileByServerRelativeUrl(list.RootFolder.ServerRelativeUrl.ToString() + "/" + fileName);
                ListItem lstItem = file.ListItemAllFields;
                context.Load(lstItem);
                context.ExecuteQuery();

                lstItem.File.CheckIn("Scanned doc check-in", CheckinType.MinorCheckIn);
                context.ExecuteQuery();
            }
        }

        public static void AddLinkToSalesforce(ForceClient forceClient, SharepointFile fileInfo, string url)
        {
            ImageLink__c linkToAdd = new ImageLink__c() { Name = fileInfo.FileName, Contact__c = fileInfo.ParentId, File_Name__c = fileInfo.FileName, URL__c = url };

            // Wait synchronously for the result
            // TODO: maybe offer async option in an overload
            Task<SuccessResponse> response = forceClient.CreateAsync("ImageLink__c", linkToAdd);
            response.Wait();
            SuccessResponse result = response.Result;

            if (result.Success)
            {
                Log("uploaded link successfully to salesforce:\n ID: " + result.Id + " \n filename: " + fileInfo.FileName + " \n ContactID: " + fileInfo.ParentId);
            }
            else
            {
                log.Error("Error inserting link: " + fileInfo.FileName + " for: " + fileInfo.ParentId);
                log.Error(result.Errors.ToString());
                //TODO maybe throw exception
            }
        }

        public static void Log(string message)
        {
            log.Info(message);
        }

        public static void SendEmail(string subject, string text)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("SalesforceToSharepoint@asa.org");
                message.To.Add(new MailAddress(EmailAddress));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = text;
                smtp.Port = 25;
                smtp.Host = MailServer;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                log.Error(String.Format("There was an error trying to send an email to {0}", EmailAddress));
                log.Error(ex.Message);
            }
        }

    }
}
