# bcs-focus-backend
api for bcs learning app for preparing for the bcs exam. The project contains the API project(BcsFocus.API) and the unit test project(BcsFocus.API.Test)

# To Run

## Requirements
* .Net 6.0
* MongoDb

## Steps
* Create the Application database in MongoDb
* Add the following configuration to the appsettings.Development.json file

```
"BcsStoreDbSettings":{
    "ConnectionString":"<your-connection-string>",
    "DatabaseName":"<your-database-name>"
  }
```

* Include all the dependencies with the command `dotnet restore`
* Build the project with the command `dotnet build`
* Start the api application with the command `dotnet run --project BcsFocus.API`
* Start the test application with the command `dotnet run --project BcsFocus.API.Test`