# Toolkit Angular JS WOA - .Net RestAPI  #

This is a default Asp.Net WebApi .Net 4.6 Template with several modifications:

## Authenticaion ##
* Db repo authentication and Bearer tokens are enabled (see Startup.Auth.cs)
* SSO or Oauth2 external logins (Facebook, Google, Microsoft, Twitter) can be enable very easily (see Startup.Auth.cs)

## Api Version ##
* API Version is handle in BaseController.cs

## Global Error handlers ##
* GlobalExceptionFilter.cs catch all errors raised in a Controller call
* GlobalExceptionHandler.cs catch all errors outside a controller call

## Testing ##
* OWIN tests has been setup to make integration tests (see examples in WebAPIToolkit.Tests.Controllers.*)

## Generated Documentation ##
* Swagger is enabled and is accessible at http://WEBAPIURL/swagger

## Database ##
* EntityFramework is used as ORM.
* The database specified in web.config in connectionstring "ModelContextDatabase" is used.
* If the database is empty, EntityFramework will initialize it on first API call accessing database (it can be disabled using disableDatabaseInitialization)
* If you update the model, see [Code First Migrations](https://msdn.microsoft.com/en-us/data/jj591621). Basically, in the Package Manager Console run :
```

PM> Enable-Migrations
PM> Add-Migration
PM> Update-Database
```

## Misc. ##
* Convention based routing is disable (see WebApiConfig.cs to enable it)
* CORS has been enabled for all origins (see BaseController.cs)
* CamelCase is use in Json (see Startup.cs)
