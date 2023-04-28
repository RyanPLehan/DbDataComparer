using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Interfaces;

namespace DbDataComparer.Domain.Helpers
{
    public class EmailNotification : IEmailNotification
    {
        private const char SEPARATOR_COMMA = ',';
        private const char SEPARATOR_SEMI_COLON = ';';

        protected readonly NotificationSettings Settings;

        public EmailNotification(NotificationSettings notificationSettings)
        {
            this.Settings = notificationSettings ??
                throw new ArgumentNullException(nameof(notificationSettings));
        }


        public async Task Send(string toAddress, string fromAddress, string ccAddress, string subject, string body, bool isBodyHtml = false)
        {
            IEnumerable<string> toAddresses = ParseEmailAddresses(toAddress);
            IEnumerable<string> ccAddresses = ParseEmailAddresses(ccAddress);
            await Send(toAddresses, fromAddress, ccAddresses, subject, body, isBodyHtml);
        }

        public async Task Send(IEnumerable<string> toAddresses, string fromAddress, IEnumerable<string> ccAddresses, string subject, string body, bool isBodyHtml = false)
        {
            if (!toAddresses.Any())
                return;

            try
            {
                EmailSettings emailSettings = this.Settings.Email;
                SmtpClient client = CreateCient(emailSettings.Server);
                MailMessage mailMsg = new MailMessage();

                fromAddress = (String.IsNullOrWhiteSpace(fromAddress)) ? emailSettings.From : fromAddress;
                subject = (String.IsNullOrWhiteSpace(subject)) ? emailSettings.Subject : subject;

                AddToMailAddressCollection(mailMsg.To, toAddresses);
                mailMsg.From = new MailAddress(fromAddress);
                AddToMailAddressCollection(mailMsg.CC, ccAddresses);

                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = isBodyHtml;

                await client.SendMailAsync(mailMsg);
            }
            catch
            {  }
        }


        private SmtpClient CreateCient(string server)
        {
            SmtpClient client = new SmtpClient(server);
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            return client;
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

        private void AddToMailAddressCollection(MailAddressCollection mailAddresses, IEnumerable<string> emailAddresses)
        {
            foreach (var emailAddress in emailAddresses)
            {
                mailAddresses.Add(new MailAddress(emailAddress));
            }
        }

    }
}
