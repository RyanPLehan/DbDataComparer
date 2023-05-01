using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Interfaces;
using DbDataComparer.Domain.Models;
using System.Net.Mime;

namespace DbDataComparer.Domain
{
    /// <summary>
    /// This is designed to notify, by email, the overall comparison results based upon the Notification options.
    /// This is has the ability to generate separate files based upon email
    /// </summary>
    public class TestDefinitionNotifier : IEmailNotifier
    {
        private const char SEPARATOR_COMMA = ',';
        private const char SEPARATOR_SEMI_COLON = ';';

        private readonly NotificationSettings Settings;
        private readonly IDictionary<string, string> EmailToFileLookup;

        public TestDefinitionNotifier(NotificationSettings settings)
        {
            this.Settings = settings ??
                throw new ArgumentNullException(nameof(settings));

            this.EmailToFileLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }


        public bool IsNotificationEnabled(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            bool ret = false;

            ret = ret || testDefinition.NotificationOptions.NotifyOnEveryCompare;
            ret = ret || (testDefinition.NotificationOptions.NotifyOnFailure &&
                          TestDefinitionComparer.IsAny(comparisonResults, Enums.ComparisonResultTypeEnum.Failed));

            return ret;
        }

        public async Task AddNotification(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            if (!String.IsNullOrWhiteSpace(testDefinition.NotificationOptions.Email) &&
                !this.EmailToFileLookup.ContainsKey(testDefinition.NotificationOptions.Email))
            {
                this.EmailToFileLookup.Add(testDefinition.NotificationOptions.Email.Trim().ToLower(),
                                           Path.GetTempFileName());
            }

            // Get temp file name and path to write results to
            string pathName = this.EmailToFileLookup[testDefinition.NotificationOptions.Email.Trim()];
            await WriteOverallResults(pathName, testDefinition, comparisonResults);
        }

        public async Task SendNotification(string subject)
        {
            StringBuilder body = new StringBuilder();

            body.AppendLine("Database Data Comparer User,");
            body.AppendLine();
            body.AppendLine();
            body.AppendLine("You have chosen to be notified in the event of a data comparison execution or a data comparison failure.");
            body.AppendLine("Attached is a file that contains the overall results of one or more Test Definitions that you have selected for to be notified.");
            body.AppendLine();
            body.AppendLine("If you no longer wish to receive a notification, you will need to change the Notification Options within each Test Definition file.");
            body.AppendLine();
            body.AppendLine("Thank you.");

            await SendNotification(subject, body.ToString());
        }



        public async Task SendNotification(string subject, string body)
        {
            EmailSettings emailSettings = this.Settings.Email;
            SmtpClient client = CreateCient(emailSettings.Server);

            // Iterate through lookup and send file file to 
            foreach (KeyValuePair<string, string> kvp in this.EmailToFileLookup)
            {
                try
                {
                    // Email
                    MailMessage mailMsg = new MailMessage();
            
                    AddToMailAddressCollection(mailMsg.To, kvp.Key);
                    mailMsg.From = new MailAddress(emailSettings.From);
                    mailMsg.Subject = (String.IsNullOrEmpty(subject) ? emailSettings.Subject : subject);
                    mailMsg.Body = body;
                    mailMsg.IsBodyHtml = false;
                    mailMsg.Attachments.Add(CreateAttachment(kvp.Value));

                    await client.SendMailAsync(mailMsg);

                    // Delete Temp file
                    File.Delete(kvp.Value);
                }
                catch
                { }
            }
        }

        private static async Task WriteOverallResults(string pathName, TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await TestDefinitionComparer.WriteOverallResults(sw, testDefinition, comparisonResults);
                }
            }
        }


        private SmtpClient CreateCient(string server)
        {
            SmtpClient client = new SmtpClient(server);
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            return client;
        }

        private Attachment CreateAttachment(string pathName)
        {
            ContentType ct = new ContentType(MediaTypeNames.Text.Plain);
            Attachment attachment = new Attachment(pathName, ct);
            attachment.ContentDisposition.FileName = "Overall Results.txt";

            return attachment;
        }



        private Attachment CreateAttachment(Stream stream)
        {
            ContentType ct = new ContentType(MediaTypeNames.Text.Plain);
            Attachment attachment = new Attachment(stream, ct);
            attachment.ContentDisposition.FileName = "Overall Results.txt";

            return attachment;
        }

        private IEnumerable<string> ParseEmailAddresses(string emailAddresses)
        {
            List<string> parsedAddresses = new List<string>();

            if (!String.IsNullOrWhiteSpace(emailAddresses))
            {
                if (emailAddresses.Contains(SEPARATOR_COMMA))
                {
                    foreach (var emailAddress in emailAddresses.Split(SEPARATOR_COMMA))
                    {
                        if (!String.IsNullOrWhiteSpace(emailAddress))
                            parsedAddresses.Add(emailAddress.Trim());
                    }
                }

                //if (emailAddresses.Contains(SEPARATOR_SEMI_COLON))
                //{
                //    foreach (var emailAddress in emailAddresses.Split(SEPARATOR_SEMI_COLON))
                //    {
                //        if (!String.IsNullOrWhiteSpace(emailAddress))
                //            parsedAddresses.Add(emailAddress.Trim());
                //    }
                //}
            }

            return parsedAddresses;
        }

        private void AddToMailAddressCollection(MailAddressCollection mailAddresses, string emailAddress)
        {
            mailAddresses.Add(new MailAddress(emailAddress));
        }


        private void AddToMailAddressCollection(MailAddressCollection mailAddresses, IEnumerable<string> emailAddresses)
        {
            foreach (var emailAddress in emailAddresses)
            {
                mailAddresses.Add(new MailAddress(emailAddress));
            }
        }

    }

}
