# FUNDS API

## CRUD API developed in dot net core 2.2 using C# 
## Using design patterns like Unit of work, repositories, database migrations, Dependency Injection, Unit testing using XUnit
## SQL Database needed for use the api

### Developed by Juan Carlos Del Saz 

# How To use 

## Configuration of the DB 

1. The first step is change the connection string for sql server database. The steps for doing it are the following:  
	1. Open the solution project with your favorite editor(I use for perform this API Visual Studio 2019 Community). 
	2. You can find two projects inside => fundsAPI and XunitFundsAPI. Display fundsAPI and in the root you can finde the file called "appsettings.json". Deploy this file and open "appsettings.development.json".
	3. In this configuration file you need to change the property default of the connectionStrings with your own sql sever connection string. 

2. If your are using Visual Studio
	1. Go to Tools -> Nuget administrator packages -> Package Manager Console and execute "Update-Database" without quotes. With this your setting up the table using Microsoft migrations.
	2. In case your are not able to do it or something fails. In the solution fundsAPI in the folder Migrations -> SqlScripts you can find the createDatabase.sql script in order to create the database.

3. Seed the Database
	1. In folder Migrations -> SqlScripts you can find two scripts AddFundData.sql and AddFundsValueData.sql please after the creation of the database, execute this scripts in order, first addfundsData and after finish addfundsValueData. 
	2. This scripts has the funds data 10000 register every script and are in separated files for avoid problems with time execution.
	
4. Execute the Application
	1. For execute the app in a development environment please being sure you put fundsAPI as run project and not use IISExpress.
	2. For configure that in visual studio in the toolbar you can change to fundsAPI and click it. 
5. Testing the app 
	1. The app will start at the next route "https://localhost:5001" you can see the url in the console display after start the app. 
	2. To help with the documentation the project has added swagger documentation. You can find it in the following route: 
		1. https://localhost:5001/swagger/index.html
	3. If you prefer use postman you can find a exported postman collection in the root of the repository. 
	4. You can launch the unit test for the app clickin with right mouse in the XunitFundsAPI and select Run tests.

