About mnailgun
==============

mnailgun is a wrapper library for the Mailgun REST API. It provides a MailgunClient class
that allows you to send messages, register domains, create mailboxes, manage routes, etc.

Getting started
---------------

The mailgun API is exposed through the `Typesafe.MailgunClient`. All supported operations are 
exposed through this client.

	var client = new MailgunClient("<domain>", "<API key>");
	
	// use client to send mail, create routes, etc.

Sending a mail message
----------------------

The `SendMail` method accepts a regular .Net `System.Net.MailMessage` instance. From, To, Cc, Bcc, 
Body text, attachments are all translated to a mailgun message.

	client.SendMail(new System.Net.Mail.MailMessage("gino@samples.mailgun.org", "gino.heyman@gmail.com") 
	{
		Subject = "Hello from mailgun",
		Body = "this is a test message from mailgun."
	});

Managing Routes and Mailboxes
-----------------------------

MailgunClient provides the following Route-related operations:

	IEnumerable<Route> GetRoutes(int skip, int take, out int count)
	Route CreateRoute(int priority, string description, RouteFilter expression, params RouteAction[] actions)
	void DeleteRoute(string routeId)

Mailboxes can be managed just the same:

	Mailbox CreateMailbox(string name, string password)
	void DeleteMailbox(string name)
	IEnumerable<Mailbox> GetMailboxes(int skip, int take, out int count)

Getting statistics
------------------

Stats can be retrieved with `GetStats` method of the MailgunClient.

Running Tests
-------------

1. Make sure you replace the placeholders with you domain and API key in `src/Typesafe.Mailgun.Tests/MailgunClientBuilder.cs`.
2. run the XUnit tests, with your preferred IDE/runner.

License
=======

Copyright (c) 2011-2018 Gino Heyman.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
