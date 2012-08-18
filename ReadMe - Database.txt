Database ReadMe

The project relies on Entity Framework 5.0 Code First.

This means any changes to the Domain Model, will require a rerun of the command Update-Database.


* First time setup

1. Open the Package Manager Console
2. Selected "InTheBoks" as the default project
3. Run the command: "Enable-Migrations -EnableAutomaticMigrations"
4. Run the command: "Update-Database"
5. This should create a new LocalDb instance OR SQLExpress database, depending on your settings in App.config.
6. Make sure the connection string is the same in App.config (InTheBoks) and Web.config (InTheBoks.Web).

* After changes to Domain Model

1. Open the Package Manager Console
2. Run the command: "Update-Database -Force"

* View The Database

You can view the database using the new SQL Server Object Explorer. Open this view from the View menu.
From here, navigate to the (localdb) instance and Databases, where "InTheBoks" should be visible.

