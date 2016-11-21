using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Typesafe.Mailgun
{
    /// <summary>
    /// Descendant of <see cref="MailMessage"/> that gives access to Mailgun-specific fields.
    /// </summary>
    public class MailgunMessage: MailMessage
    {
        /// <summary>
        /// Tags for categorizing messages for stats purposes.
        /// </summary>
        [MailgunField(Name = "tag")]
        public List<object> Tags { get; set; }

        /// <summary>
        /// ID of the campaign the message belongs to.
        /// </summary>
        [MailgunField]
        public string Campaign { get; set; }

        /// <summary>
        /// Enables/disables DKIM signatures on per-message basis.
        /// </summary>
        [MailgunField]
        [MailgunBoolValues("yes", "no")]
        public bool? Dkim { get; set; }

        /// <summary>
        /// Desired time of delivery. 
        /// </summary>
        [MailgunField(Name = "deliverytime")]
        public DateTime? DeliveryTime { get; set; }

        /// <summary>
        /// Enables sending in test mode. You are charged for messages sent in test mode.
        /// </summary>
        [MailgunField(Name = "testmode")]
        [MailgunBoolValues("yes", "no")]
        public bool? TestMode { get; set; }

        /// <summary>
        /// Toggles tracking on a per-message basis.
        /// </summary>
        [MailgunField]
        [MailgunBoolValues("yes", "no")]
        public bool? Tracking { get; set; }

        /// <summary>
        /// Toggles opens tracking on a per-message basis.
        /// </summary>
        [MailgunField("o", "tracking-opens")]
        [MailgunBoolValues("yes", "no")]
        public bool? TrackingOpens { get; set; }

        /// <summary>
        /// Toggles clicks tracking on a per-message basis.
        /// </summary>
        [MailgunField("o", "tracking-clicks")]
        [MailgunBoolValues("yes", "no")]
        public bool? TrackingClicks { get; set; }

        /// <summary>
        /// If set to True this requires the message only be sent over a TLS connection.
        /// </summary>
        [MailgunField(Name = "require-tls")]
        [MailgunBoolValues("True", "False")]
        public bool? RequireTls { get; set; }

        /// <summary>
        /// If set to True, the certificate and hostname will not be verified when trying to establish a TLS connection.
        /// </summary>
        [MailgunField(Name = "skip-verification")]
        [MailgunBoolValues("True", "False")]
        public bool? SkipVerification { get; set; }

        /// <summary>
        /// Custom MIME headers to append to message.
        /// </summary>
        [MailgunField("h", "header")]
        public Dictionary<object, object> CustomHeaders { get; set; }

        /// <summary>
        /// Custom variables to attach to message.
        /// </summary>
        [MailgunField("v", "var")]
        public Dictionary<object, object> Vars { get; set; }

        /// <summary>
        /// Initializes an empty instance of the <see cref="MailgunMessage"/> class.
        /// </summary>
        public MailgunMessage(): base()
        {
            Init();   
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailgunMessage"/> class by using
        ///     the specified <see cref="string"/> class objects.
        /// </summary>
        public MailgunMessage(string from, string to): base(from, to)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailgunMessage"/> class by using
        ///     the specified <see cref="MailAddress"/> class objects.
        /// </summary>
        public MailgunMessage(MailAddress from, MailAddress to): base(from, to)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailgunMessage"/> class.
        /// </summary>
        public MailgunMessage(string from, string to, string subject, string body): base(from, to, subject, body)
        {
            Init();
        }

        private void Init()
        {
            Tags = new List<object>();
            CustomHeaders = new Dictionary<object, object>();
            Vars = new Dictionary<object, object>();
        }
    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class MailgunFieldAttribute : Attribute
    {
        public string Prefix { get; set; }
        public string Name { get; set; }

        public MailgunFieldAttribute(): this("o", null)
        {           
        }

        public MailgunFieldAttribute(string prefix): this(prefix, null)
        {           
        }

        public MailgunFieldAttribute(string prefix, string name)
        {
            Prefix = prefix.ToLower();
            Name = name;            
        }
    }

    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class MailgunBoolValuesAttribute : Attribute
    {
        public string True { get; set; }
        public string False { get; set; }

        public MailgunBoolValuesAttribute(string trueValue, string falseValue)
        {
            True = trueValue;
            False = falseValue;
        }
    }
}
