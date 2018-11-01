= mnailgun Release Notes

== 3.0.0

- Migrate to netstandard2.0 (thanks to Mongkon Eiadon)

== 2.0.0

- remove obsolete mailbox support
- Adds the ability to apply custom variables to your email messages (thanks to ProNotion)
- Add support for custom data (thanks to Brandon Chothia)


== 1.0.0

- add overload to specify version of API
- Fixed ReadJson on MailgunHttpResponse (thanks to Raphael ATALLAH)
- Added the ability to add recipient variables (thanks to Zac Marcus)
- fix typo in README (thanks to ITmeze Michal Zygula)

== 0.7.0

- Add support for ReplyTo (thanks to Grímur Daníelsson)

== Version 0.6.0

- Include mailgun mesasge id in `SendMail` response (thanks to Byron Sommardahl)

== Version 0.5.0

- Add all members to IMailgunClient

== Version 0.4.0

- Add support for alterante views (thanks to Rob Cooper)

== Version 0.3.0

- Update JSON package dependency

== Version 0.2.2

- Fixed issue 2 (display names are ignored)
- updated README to include nuget info

== Version 0.2.1

- Build script improvements (mainly versioning)
- Fixed test for bad domaind which broke after introducing MailgunClientBuilder
- Added support for SymbolSource
- Added license (Apache 2.0)

== Version 0.2.0

Added support for Stats, Routes & Mailboxes.

== Version 0.1.0

The initial version that allows you to send (mail) messages using your Mailgun domaing and api key.
