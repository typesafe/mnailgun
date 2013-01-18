using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using NUnit.Framework;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun.Tests
{
    [TestFixture]
    public class FormPartsBuilderTests
    {
        [Test]
        public void Build_NullMessage_ReturnsEmptyList()
        {
            var result = FormPartsBuilder.Build(null);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Build_FromSpecified_AddsFromPart()
        {
            var from = new MailAddress("test@success.com", "Test Passed");
            var message = BuildMessage(x => x.From = from);
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("from", from.ToString());
        }
        
        [Test]
        public void Build_ToSpecified_AddsToPart()
        {
            var to = new MailAddress("test@success.com", "Test Passed");
            var message = BuildMessage(x => { x.To.Clear(); x.To.Add(to);});
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("to", to.ToString());
        }
        
        [Test]
        public void Build_CCSpecified_AddsCCPart()
        {
            var cc = new MailAddress("test@success.com", "Test Passed");
            var message = BuildMessage(x => { x.CC.Clear(); x.CC.Add(cc);});
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("cc", cc.ToString());
        }
        
        [Test]
        public void Build_BCCSpecified_AddsCCPart()
        {
            var bcc = new MailAddress("test@success.com", "Test Passed");
            var message = BuildMessage(x => { x.Bcc.Clear(); x.Bcc.Add(bcc);});
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("bcc", bcc.ToString());
        }
        
        [Test]
        public void Build_SubjectSpecified_AddsSubjectPart()
        {
            var message = BuildMessage(x => x.Subject = "Test Passed");
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("subject", "Test Passed");
        }
        
        [Test]
        public void Build_BodyIsText_AddsTextPart()
        {
            var message = BuildMessage(x => { x.IsBodyHtml = false; x.Body = "Test Passed"; });
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("text", "Test Passed");
        }
        
        [Test]
        public void Build_BodyIsHtml_AddsHtmlPart()
        {
            var message = BuildMessage(x => { x.IsBodyHtml = true; x.Body = "<h1>Test Passed</h1>"; });
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("html", "<h1>Test Passed</h1>");
        }
        
        [Test]
        public void Build_MessageHasAttachments_AddsAttachmentParts()
        {
            var attachment = new Attachment(new MemoryStream(), "Test Attachment");
            var message = BuildMessage(x => x.Attachments.Add(attachment));
            var result = FormPartsBuilder.Build(message);
            result.AssertContains(attachment);
        }

        // TODO (RC): AlternateView for HTML Sets HTML Param
        // TODO (RC): AlternateView for Text Sets Text Param
        // TODO (RC): Body Overrides (IsHtml False)
        // TODO (RC): Body Overrides (IsHtml True)

        /// <summary>
        /// Factory method for spinning up a MailMessage to play with.
        /// </summary>
        private MailMessage BuildMessage(Action<MailMessage> mutator = null)
        {
            var sender = new MailAddress("somesender@mailinator.com", "Sender");
            var recipient = new MailAddress("somerecipient@mailinator.com", "Recipient");
            var message = new MailMessage(sender, recipient);

            if (mutator != null)
                mutator(message);

            return message;
        }
    }

    internal static class FormPartsBuilderTestsExtensions
    {
        internal static void AssertContains(this List<FormPart> parts, string name, string value)
        {
            if (parts == null || !parts.Any())
                Assert.Fail("Expected FormParts list to contain '{0}' with value '{1}', but the list is null/empty.", name, value);

            var match = parts.OfType<SimpleFormPart>().Where(x => x.Name == name && x.Value == value);

            if (match == null || !match.Any())
                Assert.Fail("Expected FormParts list to contain '{0}' with value '{1}', but no matching name/value pair was found.", name, value);

            Assert.Pass("Found FormPart item in list named '{0}' with value '{1}'.", name, value);
        }
        
        internal static void AssertContains(this List<FormPart> parts, Attachment attachment)
        {
            if (parts == null || !parts.Any())
                Assert.Fail("Expected FormParts list to contain attachment '{0}', but the list is null/empty.", attachment.Name);

            var match = parts.OfType<AttachmentFormPart>().Where(x => x.Attachment == attachment);

            if (match == null || !match.Any())
                Assert.Fail("Expected FormParts list to contain attachment '{0}', but no matching item was found.", attachment.Name);

            Assert.Pass("Found FormPart attachment item in list named '{0}'.", attachment.Name);
        }
    }
}
