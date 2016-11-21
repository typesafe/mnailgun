﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using NUnit.Framework;
using Typesafe.Mailgun.Extensions;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun.Tests
{
	using Typesafe.Mailgun.Extensions;

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
			var message = BuildMessage(x => { x.To.Clear(); x.To.Add(to); });
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("to", to.ToString());
		}

		[Test]
		public void Build_CCSpecified_AddsCCPart()
		{
			var cc = new MailAddress("test@success.com", "Test Passed");
			var message = BuildMessage(x => { x.CC.Clear(); x.CC.Add(cc); });
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("cc", cc.ToString());
		}

		[Test]
		public void Build_BCCSpecified_AddsCCPart()
		{
			var bcc = new MailAddress("test@success.com", "Test Passed");
			var message = BuildMessage(x => { x.Bcc.Clear(); x.Bcc.Add(bcc); });
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("bcc", bcc.ToString());
		}

		[Test]
		public void Build_ReplyToSpecified_AddsReplyToPart()
		{
			var replyTo = new MailAddress("test@success.com", "Test Passed");
			var message = BuildMessage(x => { x.ReplyToList.Clear(); x.ReplyToList.Add(replyTo); });
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("h:Reply-To", replyTo.ToString());
		}

		[Test]
		public void Build_SubjectSpecified_AddsSubjectPart()
		{
			var message = BuildMessage(x => x.Subject = "Test Passed");
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("subject", "Test Passed");
		}

		[Test]
		public void Build_VariablesSpecified_AddsVariablesPart()
		{
			var message = BuildMessage(x => x.AddVariable("my_message_id", "123"));
			var result = FormPartsBuilder.Build(message);

			result.AssertContains("v:my_message_id", "123");
		}

		[Test]
		public void Build_BodyIsText_AddsTextPart_NoHtmlPart()
		{
			var message = BuildMessage(x => { x.IsBodyHtml = false; x.Body = "Test Passed"; });
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("text", "Test Passed");
			result.AssertDoesntContain("html");
		}

		[Test]
		public void Build_BodyIsHtml_AddsHtmlPart_NoTextPart()
		{
			var message = BuildMessage(x => { x.IsBodyHtml = true; x.Body = "<h1>Test Passed</h1>"; });
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("html", "<h1>Test Passed</h1>");
			result.AssertDoesntContain("text");
		}


		[Test]
		public void Build_TagsSpecified_AddsTagPart()
		{
			var message = BuildMessage(x => { x.AddTag("TagExample"); });
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("o:tag", "TagExample");
		}

		[Test]
		public void Build_MessageHasAttachments_AddsAttachmentParts()
		{
			var attachment = new Attachment(new MemoryStream(), "Test Attachment");
			var message = BuildMessage(x => x.Attachments.Add(attachment));
			var result = FormPartsBuilder.Build(message);
			result.AssertContains(attachment);
		}

		[Test]
		public void Build_MessageHasPlainTextAlternateView_AddsTextPart_NotHtmlPart()
		{
			var view = AlternateView.CreateAlternateViewFromString("plaintext", new ContentType("text/plain"));
			var message = BuildMessage(x => x.AlternateViews.Add(view));
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("text", "plaintext");
			result.AssertDoesntContain("html");
		}

		[Test]
		public void Build_MessageHasPlainTextAlternateViewAndBodyIsText_BodyWins()
		{
			var view = AlternateView.CreateAlternateViewFromString("plaintext", new ContentType("text/plain"));
			var message = BuildMessage(x =>
										   {
											   x.AlternateViews.Add(view);
											   x.Body = "success";
											   x.IsBodyHtml = false;
										   });
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("text", "success");
			result.AssertDoesntContain("text", "plaintext");
		}

		[Test]
		public void Build_MessageHasHtmlTextAlternateView_AddsHtmlPart()
		{
			var view = AlternateView.CreateAlternateViewFromString("<h1>html</h1>", new ContentType("text/html"));
			var message = BuildMessage(x => x.AlternateViews.Add(view));
			var result = FormPartsBuilder.Build(message);
			result.AssertContains("html", "<h1>html</h1>");
		}

        #region MailgunMessage
        [Test]
        public void Build_MailgunMessage_AddsCampaign()
        {
            var message = BuildMailgunMessage(m => m.Campaign = "campaign value");
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("o:campaign", "campaign value");
        }

        [Test]
        public void Build_MailgunMessage_AddsTagPart()
        {
            var message = BuildMailgunMessage(m => m.Tags.Add("foobar"));
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("o:tag", "foobar");
        }

        [Test]
        public void Build_MailgunMessage_AddsVarPart()
        {
            var message = BuildMailgunMessage(m => m.Vars.Add("myvar", 123));
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("v:myvar", "123");
        }

        [Test]
        public void Build_MailgunMessage_AddsDeliveryTime()
        {
            var message = BuildMailgunMessage(m => m.DeliveryTime = DateTime.Parse("2016-11-20"));
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("o:deliverytime", "Sun, 20 Nov 2016 00:00:00 GMT");
        }

        [Test]
        public void Build_MailgunMessage_AddsHeaderPart()
        {
            var message = BuildMailgunMessage(m => m.CustomHeaders.Add("X-Foobar", "Some value"));
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("h:X-Foobar", "Some value");
        }

        [Test]
        public void Build_MailgunMessage_AddsYesNoBool()
        {
            var message = BuildMailgunMessage(m => m.Dkim = false);
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("o:dkim", "no");
        }

        [Test]
        public void Build_MailgunMessage_AddsTrueFalseBool()
        {
            var message = BuildMailgunMessage(m => m.RequireTls = false);
            var result = FormPartsBuilder.Build(message);
            result.AssertContains("o:require-tls", "False");
        }

        private MailgunMessage BuildMailgunMessage(Action<MailgunMessage> mutator = null)
        {
            var sender = new MailAddress("somesender@mailinator.com", "Sender");
            var recipient = new MailAddress("somerecipient@mailinator.com", "Recipient");
            var message = new MailgunMessage(sender, recipient);

            if (mutator != null)
                mutator(message);

            return message;
        }
        #endregion

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
		}

		internal static void AssertDoesntContain(this List<FormPart> parts, string name)
		{
			if (parts == null || !parts.Any()) return;

			if (parts.OfType<SimpleFormPart>().Any(x => x.Name == name))
			{
				Assert.Fail("Found FormPart item in list named '{0}'.", name);
			}
		}

		internal static void AssertDoesntContain(this List<FormPart> parts, string name, string value)
		{
			if (parts == null || !parts.Any())
				return;

			var match = parts.OfType<SimpleFormPart>().Where(x => x.Name == name && x.Value == value);

			if (match == null || !match.Any())
				return;

			Assert.Fail("Found FormPart item in list named '{0}' with value '{1}'.", name, value);
		}

		internal static void AssertContains(this List<FormPart> parts, Attachment attachment)
		{
			if (parts == null || !parts.Any())
				Assert.Fail("Expected FormParts list to contain attachment '{0}', but the list is null/empty.", attachment.Name);

			var match = parts.OfType<AttachmentFormPart>().Where(x => x.Attachment == attachment);

			if (match == null || !match.Any())
				Assert.Fail("Expected FormParts list to contain attachment '{0}', but no matching item was found.", attachment.Name);
		}
	}
}
