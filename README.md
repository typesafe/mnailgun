= About mnailgun

mnailgun is a wrapper library for the Mailgun REST API. It provides a MailgunClient class
that allows you to send messages, register domains, create mailboxes, manage routes, etc.

= How to use?

== Install

No nuget package yet. Shouldn't take to long.

== General

The mailgun API is exposed through a MilgunClient class 

var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");

== Sending a mail message

 var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");
 client.SendMail(new System.Net.MailMessage("gino@samples.mailgun.org", "gino.heyman@gmail.com") 
 {
    Subject = "Hello from mailgun",
    Body = "this is a test message from mailgun."
 });

== Sending a mail message with attachments

 var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");
 client.SendMail(new System.Net.MailMessage("gino@samples.mailgun.org", "gino.heyman@gmail.com") 
 {
    Subject = "Hello from mailgun",
    Body = "this is a test message from mailgun."
 });

== What else

Coming soon :-)

= License

Copyright (c) 2011 Gino Heyman.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
