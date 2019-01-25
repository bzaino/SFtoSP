using Newtonsoft.Json;
using Salesforce.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesForce365Library.Models.Salesforce
{
    public class Contact
    {
        [Key]
        [Display(Name = "Contact ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [Display(Name = "Deleted")]
        [Createable(false), Updateable(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "Master Record ID")]
        [Createable(false), Updateable(false)]
        public string MasterRecordId { get; set; }

        [Display(Name = "Account ID")]
        public string AccountId { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(80)]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(40)]
        public string FirstName { get; set; }

        public string Salutation { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(121)]
        [Createable(false), Updateable(false)]
        public string Name { get; set; }

        [Display(Name = "Record Type ID")]
        public string RecordTypeId { get; set; }

        [Display(Name = "Other Street")]
        public string OtherStreet { get; set; }

        [Display(Name = "Other City")]
        [StringLength(40)]
        public string OtherCity { get; set; }

        [Display(Name = "Other State/Province")]
        [StringLength(80)]
        public string OtherState { get; set; }

        [Display(Name = "Other Zip/Postal Code")]
        [StringLength(20)]
        public string OtherPostalCode { get; set; }

        [Display(Name = "Other Country")]
        [StringLength(80)]
        public string OtherCountry { get; set; }

        [Display(Name = "Other Latitude")]
        public double? OtherLatitude { get; set; }

        [Display(Name = "Other Longitude")]
        public double? OtherLongitude { get; set; }

        [Display(Name = "Mailing Street")]
        public string MailingStreet { get; set; }

        [Display(Name = "Mailing City")]
        [StringLength(40)]
        public string MailingCity { get; set; }

        [Display(Name = "Mailing State/Province")]
        [StringLength(80)]
        public string MailingState { get; set; }

        [Display(Name = "Mailing Zip/Postal Code")]
        [StringLength(20)]
        public string MailingPostalCode { get; set; }

        [Display(Name = "Mailing Country")]
        [StringLength(80)]
        public string MailingCountry { get; set; }

        [Display(Name = "Mailing Latitude")]
        public double? MailingLatitude { get; set; }

        [Display(Name = "Mailing Longitude")]
        public double? MailingLongitude { get; set; }

        [Display(Name = "Business Phone")]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Business Fax")]
        [Phone]
        public string Fax { get; set; }

        [Display(Name = "Mobile Phone")]
        [Phone]
        public string MobilePhone { get; set; }

        [Display(Name = "Home Phone")]
        [Phone]
        public string HomePhone { get; set; }

        [Display(Name = "Other Phone")]
        [Phone]
        public string OtherPhone { get; set; }

        [Display(Name = "Asst. Phone")]
        [Phone]
        public string AssistantPhone { get; set; }

        [Display(Name = "Reports To ID")]
        public string ReportsToId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(128)]
        public string Title { get; set; }

        [StringLength(80)]
        public string Department { get; set; }

        [Display(Name = "Assistant's Name")]
        [StringLength(40)]
        public string AssistantName { get; set; }

        [Display(Name = "Lead Source")]
        public string LeadSource { get; set; }

        public DateTime? Birthdate { get; set; }

        [Display(Name = "Contact Description")]
        public string Description { get; set; }

        [Display(Name = "Owner ID")]
        [Updateable(false)]
        public string OwnerId { get; set; }

        [Display(Name = "Email Opt Out")]
        public bool HasOptedOutOfEmail { get; set; }

        [Display(Name = "Do Not Call")]
        public bool DoNotCall { get; set; }

        [Display(Name = "Created Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset CreatedDate { get; set; }

        [Display(Name = "Created By ID")]
        [Createable(false), Updateable(false)]
        public string CreatedById { get; set; }

        [Display(Name = "Last Modified Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset LastModifiedDate { get; set; }

        [Display(Name = "Last Modified By ID")]
        [Createable(false), Updateable(false)]
        public string LastModifiedById { get; set; }

        [Display(Name = "System Modstamp")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset SystemModstamp { get; set; }

        [Display(Name = "Last Activity")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastActivityDate { get; set; }

        [Display(Name = "Last Stay-in-Touch Request Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastCURequestDate { get; set; }

        [Display(Name = "Last Stay-in-Touch Save Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastCUUpdateDate { get; set; }

        [Display(Name = "Last Viewed Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastViewedDate { get; set; }

        [Display(Name = "Last Referenced Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastReferencedDate { get; set; }

        [Display(Name = "Email Bounced Reason")]
        [StringLength(255)]
        public string EmailBouncedReason { get; set; }

        [Display(Name = "Email Bounced Date")]
        public DateTimeOffset? EmailBouncedDate { get; set; }

        [Display(Name = "Is Email Bounced")]
        [Createable(false), Updateable(false)]
        public bool IsEmailBounced { get; set; }

        [Display(Name = "Photo URL")]
        [Url]
        [Createable(false), Updateable(false)]
        public string PhotoUrl { get; set; }

        [Display(Name = "Data.com Key")]
        [StringLength(20)]
        public string Jigsaw { get; set; }

        [Display(Name = "Jigsaw Contact ID")]
        [StringLength(20)]
        [Createable(false), Updateable(false)]
        public string JigsawContactId { get; set; }

        [Display(Name = "Role to ASA")]
        public string Role_to_ASA__c { get; set; }

        [Display(Name = "ASA Direct ID")]
        [StringLength(80)]
        public string ASA_Direct_ID__c { get; set; }

        [Display(Name = "No Longer Here")]
        public bool No_Longer_Here__c { get; set; }

        [Display(Name = "Sortable Title")]
        public string Sortable_Title__c { get; set; }

        [Display(Name = "Business Phone 2")]
        [StringLength(20)]
        public string Business_Phone_2__c { get; set; }

        [Display(Name = "TTY/TDD Phone")]
        [StringLength(20)]
        public string TTY_TDD_Phone__c { get; set; }

        [Display(Name = "Web Page")]
        [Url]
        public string Web_Page__c { get; set; }

        [Display(Name = "Holiday Card")]
        public bool Holiday_Card__c { get; set; }

        [Display(Name = "MI")]
        [StringLength(2)]
        public string Middle__c { get; set; }

        [Display(Name = "Finished SALT Courses")]
        public string Finished_MM101_Courses__c { get; set; }

        [Display(Name = "eClips")]
        public bool eClips__c { get; set; }

        [Display(Name = "Breakfast/Lunch Series")]
        public bool Breakfast_Lunch_Series__c { get; set; }

        [Display(Name = "INTID")]
        [StringLength(20)]
        public string INTID__c { get; set; }

        [Display(Name = "Pardot Campaign")]
        [StringLength(255)]
        public string pi__campaign__c { get; set; }

        [Display(Name = "Reference")]
        public bool Reference__c { get; set; }

        [Display(Name = "SALT Ambassador Advisor")]
        public bool SALT_Ambassador_Advisor__c { get; set; }

        [Display(Name = "Pardot Opt Out")]
        public bool Pardot_Opt_Out__c { get; set; }

        [Display(Name = "ASA Member")]
        public string ASA_Member__c { get; set; }

        [Display(Name = "Suffix")]
        public string Suffix__c { get; set; }

        [Display(Name = "Hours of Opp.")]
        [StringLength(50)]
        public string Hours_of_Opp__c { get; set; }

        [Display(Name = "SALT Ambassador Newsletter")]
        public bool SALT_Ambassador_Newsletter__c { get; set; }

        [Display(Name = "Communications")]
        public string Communications__c { get; set; }

        [Display(Name = "Academic Year")]
        public string Academic_Year__c { get; set; }

        [Display(Name = "LSR Focus Group")]
        public bool LSR_Focus_Group__c { get; set; }

        [Display(Name = "International School Communication")]
        public bool International_School_Communication__c { get; set; }

        [Display(Name = "Amount of Lobbying Time")]
        [StringLength(10)]
        public string Amount_of_Lobbying_Time__c { get; set; }

        [Display(Name = "Marketing Notes:")]
        public string Marketing_Notes__c { get; set; }

        [Display(Name = "Strategic Services")]
        public string Strategic_Services__c { get; set; }

        [Display(Name = "Associated Travel Expenses?")]
        public bool Associated_Travel_Expenses__c { get; set; }

        [Display(Name = "Lobbying Activity?")]
        public bool Lobbying_Activity__c { get; set; }

        [Display(Name = "Lobbying Notes")]
        public string Lobbying_Notes__c { get; set; }

        [Display(Name = "Mailing Street #2")]
        [StringLength(30)]
        public string Mailing_Street_2__c { get; set; }

        [Display(Name = "Government Office")]
        public string Govt_Office__c { get; set; }

        [Display(Name = "Target Office")]
        public string Target_Office__c { get; set; }

        [Display(Name = "Contact Owner Phone")]
        public string Contact_Owner_Phone__c { get; set; }

        [Display(Name = "Ambassador Certified")]
        public bool Ambassador_Certified__c { get; set; }

        [Display(Name = "Receive Newsletter?")]
        public bool Receive_Newsletter__c { get; set; }

        [Display(Name = "Ambassador Account")]
        public string Ambassador_Account__c { get; set; }

        [Display(Name = "Portal User")]
        public bool Portal_User__c { get; set; }

        [Display(Name = "Customer Survey")]
        public string Customer_Survey__c { get; set; }

        [Display(Name = "Co-Presenter")]
        public bool Co_Presenter__c { get; set; }

        [Display(Name = "SALT Newsletter")]
        public bool SALT_Newsletter__c { get; set; }

        [Display(Name = "Alternate Email")]
        [EmailAddress]
        public string Alternate_Email__c { get; set; }

        [Display(Name = "Sponsor Newsletter")]
        public bool Sponsor_Newsletter__c { get; set; }

        [Display(Name = "Pardot Comments")]
        public string pi__comments__c { get; set; }

        [Display(Name = "Pardot Conversion Date")]
        public DateTimeOffset? pi__conversion_date__c { get; set; }

        [Display(Name = "Pardot Conversion Object Name")]
        [StringLength(255)]
        public string pi__conversion_object_name__c { get; set; }

        [Display(Name = "Pardot Conversion Object Type")]
        [StringLength(255)]
        public string pi__conversion_object_type__c { get; set; }

        [Display(Name = "Pardot Created Date")]
        public DateTimeOffset? pi__created_date__c { get; set; }

        [Display(Name = "Pardot First Activity")]
        public DateTimeOffset? pi__first_activity__c { get; set; }

        [Display(Name = "Pardot First Referrer Query")]
        [StringLength(255)]
        public string pi__first_search_term__c { get; set; }

        [Display(Name = "Pardot First Referrer Type")]
        [StringLength(255)]
        public string pi__first_search_type__c { get; set; }

        [Display(Name = "Pardot First Referrer")]
        public string pi__first_touch_url__c { get; set; }

        [Display(Name = "Pardot Grade")]
        [StringLength(10)]
        public string pi__grade__c { get; set; }

        [Display(Name = "Pardot Last Activity")]
        public DateTimeOffset? pi__last_activity__c { get; set; }

        [Display(Name = "Pardot Notes")]
        public string pi__notes__c { get; set; }

        [Display(Name = "Pardot Score")]
        public double? pi__score__c { get; set; }

        [Display(Name = "Pardot URL")]
        [Url]
        public string pi__url__c { get; set; }

        [Display(Name = "Google Analytics Campaign")]
        [StringLength(255)]
        public string pi__utm_campaign__c { get; set; }

        [Display(Name = "Google Analytics Content")]
        [StringLength(255)]
        public string pi__utm_content__c { get; set; }

        [Display(Name = "Google Analytics Medium")]
        [StringLength(255)]
        public string pi__utm_medium__c { get; set; }

        [Display(Name = "Google Analytics Source")]
        [StringLength(255)]
        public string pi__utm_source__c { get; set; }

        [Display(Name = "Google Analytics Term")]
        [StringLength(255)]
        public string pi__utm_term__c { get; set; }

        [Display(Name = "Holiday Gift")]
        public bool Holiday_Gift__c { get; set; }

        [Display(Name = "Campus Stakeholder")]
        public bool Campus_Stakeholder__c { get; set; }

        [Display(Name = "SALT Team")]
        public bool SALT_Team__c { get; set; }

        [Display(Name = "SALT Store User")]
        public bool SALT_Store_User__c { get; set; }

        [Display(Name = "RETAIN CONTACT")]
        public bool RETAIN_CONTACT__c { get; set; }

        [Display(Name = "Auto-Created Source")]
        [StringLength(60)]
        public string Auto_Created_Source__c { get; set; }

        [Display(Name = "Informed Consent Date")]
        public DateTimeOffset? qualtrics__Informed_Consent_Date__c { get; set; }

        [Display(Name = "Informed Consent")]
        public bool qualtrics__Informed_Consent__c { get; set; }

        [Display(Name = "Last Survey Invitation")]
        public DateTimeOffset? qualtrics__Last_Survey_Invitation__c { get; set; }

        [Display(Name = "Last Survey Response")]
        public DateTimeOffset? qualtrics__Last_Survey_Response__c { get; set; }

        [Display(Name = "Net Promoter Score")]
        public double? qualtrics__Net_Promoter_Score__c { get; set; }

        [Display(Name = "Achievement plan")]
        public string Achievement_plan__c { get; set; }

        [Display(Name = "Actual Date of HS Grad (or HS Eq Cert)")]
        public DateTimeOffset? Actual_Date_of_HS_Grad_or_HS_Eq_Cert__c { get; set; }

        [Display(Name = "Additional Languages")]
        [StringLength(50)]
        public string Additional_Languages__c { get; set; }

        [Display(Name = "Additional Program: Internship")]
        [StringLength(255)]
        public string Additional_Program_Internship__c { get; set; }

        [Display(Name = "Additional Program: Other")]
        [StringLength(255)]
        public string Additional_Program_Other__c { get; set; }

        [Display(Name = "Additional program participation")]
        public string Additional_program_participation__c { get; set; }

        [Display(Name = "Age")]
        [Createable(false), Updateable(false)]
        public double? Age__c { get; set; }

        [Display(Name = "Age at Intake")]
        [Createable(false), Updateable(false)]
        public double? Age_at_Intake__c { get; set; }

        [Display(Name = "Annual Taxable Income")]
        public string Annual_Taxable_Income__c { get; set; }

        [Display(Name = "At Risk: Alg Not Completed 10th (ATOIS)")]
        public string At_Risk_Alg_Not_Completed_10th_ATOIS__c { get; set; }

        [Display(Name = "At Risk: Low GPA")]
        public string At_Risk_Low_GPA_ATOIS__c { get; set; }

        [Display(Name = "At Risk: Reading/LA or Math (ATOIS)")]
        public string At_Risk_Reading_LA_or_Math_ATOIS__c { get; set; }

        [Display(Name = "Attending academically rigorous program?")]
        public bool Attending_academically_rigorous_program__c { get; set; }

        [Display(Name = "BirthDate Text")]
        [StringLength(100)]
        public string BirthDate_Text__c { get; set; }

        [Display(Name = "Birth Year")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Birth_Year__c { get; set; }

        [Display(Name = "Birthdate Lead Mapped")]
        public DateTimeOffset? Birthdate_Lead_Mapped__c { get; set; }

        [Display(Name = "CPS Advisor")]
        public string CPS_Advisor__c { get; set; }

        [Display(Name = "Career Goals")]
        [StringLength(255)]
        public string Career_Goals__c { get; set; }

        [Display(Name = "Count")]
        [Createable(false), Updateable(false)]
        public double? Count__c { get; set; }

        [Display(Name = "Current Ed Goal")]
        [StringLength(255)]
        public string Current_Ed_Goal__c { get; set; }

        [Display(Name = "Degrees Goal")]
        public string Degrees_Goal__c { get; set; }

        [Display(Name = "Disconnected Youth")]
        public string Disconnected_Youth__c { get; set; }

        [Display(Name = "Latest Grant Program")]
        public string Latest_Grant_Program__c { get; set; }

        [Display(Name = "Secondary School Talent Search")]
        public string Secondary_School_at_Intake_Picklist__c { get; set; }

        [Display(Name = "Signature Data is for Service Year (EOC)")]
        public string Signature_Data_is_for_Service_Year_EOC__c { get; set; }

        [Display(Name = "Expected Graduation Year")]
        [StringLength(4)]
        public string Expected_Graduation_Year__c { get; set; }

        [Display(Name = "Meets LIFG requirements")]
        [Createable(false), Updateable(false)]
        public bool Elidgable_Report_Support__c { get; set; }

        [Display(Name = "Eligibility")]
        public string Eligibility__c { get; set; }

        [Display(Name = "Ethnic Type")]
        public string Ethnic_Type__c { get; set; }

        [Display(Name = "Ethnicity")]
        public string Ethnicity__c { get; set; }

        [Display(Name = "Expected Graduation date")]
        public DateTimeOffset? Expected_Graduation_date__c { get; set; }

        [Display(Name = "Father's college")]
        [StringLength(255)]
        public string Father_s_college__c { get; set; }

        [Display(Name = "Gender Identity")]
        public string Gender__c { get; set; }

        [Display(Name = "Grade Level at Intake")]
        public string Grade_Level_at_Entry__c { get; set; }

        [Display(Name = "Has IEP?")]
        public bool Has_IEP__c { get; set; }

        [Display(Name = "Home Phone (FOR WF)")]
        [Phone]
        public string Home_Phone_FOR_WF__c { get; set; }

        [Display(Name = "Household Size")]
        public double? Household_Size__c { get; set; }

        [Display(Name = "Primary Income Source")]
        public string Income_Source__c { get; set; }

        [Display(Name = "Legacy_id")]
        [StringLength(255)]
        public string Legacy_id__c { get; set; }

        [Display(Name = "Intake Location")]
        public string Intake_Location__c { get; set; }

        [Display(Name = "Is Converted from Lead")]
        public bool Is_Converted_from_Lead__c { get; set; }

        [Display(Name = "Is Deceased")]
        public bool Is_Deceased__c { get; set; }

        [Display(Name = "Is Disabled")]
        public bool Is_Disabled__c { get; set; }

        [Display(Name = "Is Father 4-yr. U.S. College grad?")]
        public string Is_Father_College_graduate__c { get; set; }

        [Display(Name = "Is Home Parent/Guardian")]
        public bool Is_Home_Parent_Guardian_checkbox__c { get; set; }

        [Display(Name = "Is Independent Minor")]
        public bool Is_Independent_Minor__c { get; set; }

        [Display(Name = "Is Mobile Parent/Guardian")]
        public bool Is_Mobile_Parent_Guardian__c { get; set; }

        [Display(Name = "Is Mother 4-yr. U.S. College grad?")]
        public string Is_Mother_College_graduate__c { get; set; }

        [Display(Name = "Is Other Parent/Guardian")]
        public bool Is_Other_Parent_Guardian__c { get; set; }

        [Display(Name = "Student Status")]
        public string Student_Status__c { get; set; }

        [Display(Name = "Learned about program")]
        public string Learned_about_program__c { get; set; }

        [Display(Name = "Limited English Proficiency")]
        public string Limited_English_Proficiency__c { get; set; }

        [Display(Name = "Long-term Goal")]
        [StringLength(255)]
        public string Long_term_Goal__c { get; set; }

        [Display(Name = "Marital Status")]
        public string Marital_Status__c { get; set; }

        [Display(Name = "Intake Form Footer Line1")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Intake_Form_Footer_Line1__c { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(25)]
        public string Middle_Name__c { get; set; }

        [Display(Name = "Mobile Phone (FOR WF)")]
        [Phone]
        public string Mobile_Phone_FOR_WF__c { get; set; }

        [Display(Name = "Mother's college")]
        [StringLength(255)]
        public string Mother_s_college__c { get; set; }

        [Display(Name = "Intake Form Footer Line2")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Intake_Form_Footer_Line2__c { get; set; }

        [Display(Name = "Other Academic Need")]
        public string Other_Academic_Need__c { get; set; }

        [Display(Name = "Other: Learned about program")]
        [StringLength(100)]
        public string Other_Learned_about_program__c { get; set; }

        [Display(Name = "Other Phone (FOR WF)")]
        [Phone]
        public string Other_Phone_FOR_WF__c { get; set; }

        [Display(Name = "Other Programs")]
        [StringLength(255)]
        public string Other_Programs__c { get; set; }

        [Display(Name = "Parent Email")]
        [EmailAddress]
        public string Parent_Email__c { get; set; }

        [Display(Name = "Parent/Guardian1 First Name")]
        [StringLength(25)]
        public string Mother_Guardian1_First_Name__c { get; set; }

        [Display(Name = "Parent/Guardian1 Last Name")]
        [StringLength(25)]
        public string Mother_Guardian1_Last_Name__c { get; set; }

        [Display(Name = "Parent/Guardian2 First Name")]
        [StringLength(25)]
        public string Father_Guardian2_First_Name__c { get; set; }

        [Display(Name = "Parent/Guardian2 Last Name")]
        [StringLength(25)]
        public string Father_Guardian2_Last_Name__c { get; set; }

        [Display(Name = "Intake Form Footer Line3")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Intake_Form_Footer_Line3__c { get; set; }

        [Display(Name = "Participant’s Name Change (Optional)")]
        [StringLength(100)]
        public string Participants_Name_Change_Optional__c { get; set; }

        [Display(Name = "Participants in 2011-12 postsecondary")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? Participants_in_2011_12_postsecondary__c { get; set; }

        [Display(Name = "Postsec Education Enrollment Cohort")]
        public string Post_Secondary_Ed_Enrollment_Cohort__c { get; set; }

        [Display(Name = "Post Secondary Ins 1st Attended")]
        [StringLength(100)]
        public string Post_Secondary_Ins_1st_Attended__c { get; set; }

        [Display(Name = "Post Secondary Tracking")]
        public bool Post_Secondary_Tracking__c { get; set; }

        [Display(Name = "Preferred Phone Type")]
        public string Preferred_Phone_Type__c { get; set; }

        [Display(Name = "Primary Language")]
        public string Primary_Language__c { get; set; }

        [Display(Name = "Profile Name")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Profile_Name__c { get; set; }

        [Display(Name = "Projected Graduation Year")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Projected_Graduation_Year__c { get; set; }

        [Display(Name = "Referred by")]
        [StringLength(100)]
        public string Referred_by__c { get; set; }

        [Display(Name = "Permanent Resident ID")]
        public string Resident_Alien_Resident_ID__c { get; set; }

        [Display(Name = "SSN")]
        public string SSN__c { get; set; }

        [Display(Name = "MobilePhoneNumeric")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string MobilePhoneNumeric__c { get; set; }

        [Display(Name = "School Guidance Counselor")]
        [StringLength(100)]
        public string School_Guidance_Counselor__c { get; set; }

        [Display(Name = "Statement of Need")]
        public string Statement_of_Need__c { get; set; }

        [Display(Name = "SSN Generated for MassEdCo")]
        [StringLength(9)]
        public string SSN_Generated_for_MassEdCo__c { get; set; }

        [Display(Name = "Student Name")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Student_Name__c { get; set; }

        [Display(Name = "Tutoring Topic")]
        [StringLength(100)]
        public string Tutoring_Topic__c { get; set; }

        [Display(Name = "Intake Date")]
        public DateTime? Intake_Date__c { get; set; }

        [Display(Name = "Intake Center or Satellite Location(EOC)")]
        public string Satellite_Intake_Location_EOC__c { get; set; }

        [Display(Name = "Intake Form Title")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Intake_Form_Title__c { get; set; }

        [Display(Name = "Intake Form Expire Date EOC")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? Intake_Form_Expire_Date_EOC__c { get; set; }

        [Display(Name = "Academic Course Level")]
        public string Academic_Course_Level__c { get; set; }

        [Display(Name = "Is Media Release Authorized?")]
        public string Is_Media_Release_Authorized__c { get; set; }

        [Display(Name = "Is Mobile OK to Text?")]
        public string Is_Mobile_OK_to_Text__c { get; set; }

        [Display(Name = "Is Text ED Participant? (BPL only)")]
        public string Is_Text_ED_Participant__c { get; set; }

        [Display(Name = "Parent Signature Date")]
        public DateTimeOffset? Parent_Signature_Date__c { get; set; }

        [Display(Name = "Parent Signature Needed/Received")]
        public string Parent_Signature_Needed_Received__c { get; set; }

        [Display(Name = "Secondary School Other")]
        [StringLength(255)]
        public string Secondary_School_Other__c { get; set; }

        [Display(Name = "Student ID")]
        [StringLength(6)]
        public string Student_ID__c { get; set; }

        [Display(Name = "Text ED Sign Up Date (BPL only)")]
        public DateTimeOffset? Text_ED_Sign_Up_Date__c { get; set; }

        [Display(Name = "Veteran Status")]
        public string Veteran_Status__c { get; set; }

        [Display(Name = "Are you a US Citizen?")]
        public string Are_you_a_US_Citizen__c { get; set; }

        [Display(Name = "Client Signature Date")]
        public DateTimeOffset? Client_Signature_Date__c { get; set; }

        [Display(Name = "Client Signature Needed/Received")]
        public string Client_Signature_Needed_Received__c { get; set; }

        [Display(Name = "Current Education Level or Last Grade")]
        public string Current_Education_Level_or_Last_Grade__c { get; set; }

        [Display(Name = "Unique ID")]
        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Unique_ID__c { get; set; }

    }
}
