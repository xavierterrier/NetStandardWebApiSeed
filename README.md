# Toolkit Angular JS WOA - .Net RestAPI  #

This is a default Asp.Net WebApi Template with several modifications:
* Convention based routing is disable (see WebApiConfig.cs to enable it)
* Db repo authentication and Bearer tokens are enabled (see Startup.Auth.cs)
* SSO or Oauth2 external logins (Facebook, Google, Microsoft, Twitter) can be enable very easily (see Startup.Auth.cs)
* API Version is handle in BaseController.cs
* CORS has been enabled for all origins (see BaseController.cs)
* CamelCase is use in Json (see Startup.cs)