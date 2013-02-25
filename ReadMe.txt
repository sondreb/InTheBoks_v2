
InTheBoks - v2.0 - www.intheboks.com

Organize Your Stuff!

Music, Movies, Books, Games, Software and more.

* Register everything you own
* Remember what you borrow or rent
* Share your list items with friends and family
* Keep a wish-list of items
	
Register everything you own of the above items and everything you'd want to own.
InTheBoks helps you remember and you can search your catalogs from anywhere, 
making sure you don't purchace multiple copies. 
	
You can register which items you are borrowing or renting from your friends, 
making it easier to remember to return them.


Requirements:

	- AppId from Facebook Developer Platform (https://developers.facebook.com)
	- Visual Studio 2012
	- Azure June SDK SP1
	- ASP.NET MVC 4
	- SQL Express (or LocalDb)

Add the appId to the Scripts/app/fb.js file in the FB.init function call.
The InTheBoks.Web is configured to run with port 7474, make sure you
set that URL as the app URL within the Facebook app settings: http://localhost:7474/

To enable search on Amazon, you need to modify the web.config and add your own
secret and keys from the Amazon Web Services. Alternatively you can add a file 
to your disk. The file should be located in the C:\InTheBoks\ folder and be named
InTheBoks.ini. This file is stored outside the source repository, since it 
contains private keys. See example file in the Solution Items.

AWS Access Keys: https://portal.aws.amazon.com/gp/aws/securityCredentials


Get Started:

If you are a developer and want to run InTheBoks on your own local machine,
all you need to do is get the source and run the application.

Should you have problems with the automatic database creation, try to create
a database named "InTheBoks" on your local SQL Express instance manually.

If you want to run the sample on a server, you need to ensure that NETWORK SERVICE
have permissions to create the database, and/or the tables.


Libraries:

	- jQuery (MIT license)
	- Knockout (MIT license)
	- Facebook C# (Apache License, 2.0)
	- Modernizr (MIT license)
	- JSON.NET (?)
	- idle.timer (https://github.com/mikesherov/jquery-idletimer)
	- Microsoft Web Platform (?)
	- html5slider
	- Autofac
	- NLog
	- Windows Phone 7 inspired icons (http://metro.windowswiki.info/mi/)
	- Windows Touch Icons (http://windows7tablet.blogspot.no/2011/06/metro-icons-and-requests-for-keyboard.html)
	- Bootstrap (http://twitter.github.com/bootstrap/) (Apache License, 2.0)

Thanks to all the developers of these great libaries, this project could not be
realized without them.


License:

All source, except specified otherwise, are licensed under the MIT license. See License.txt
All libraries, are licensed as their respective licenses.


User Feedback and Support:

Email: tickets@intheboks.uservoice.com
Web: http://intheboks.uservoice.com/