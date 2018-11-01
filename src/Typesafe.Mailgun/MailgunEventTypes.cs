using System;

namespace Typesafe.Mailgun
{
	[Flags]
	public enum MailgunEventTypes
	{
		Sent = 0x1,
		Bounces = 0x2,
		Complaints = 0x4,
		Unsubscribes = 0x8,
		Opened = 0x10,
		LinkClicks = 0x20
	}
}
