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

## Misc. ##
* Convention based routing is disable (see WebApiConfig.cs to enable it)
* CORS has been enabled for all origins (see BaseController.cs)
* CamelCase is use in Json (see Startup.cs)
