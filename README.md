WebApiSecure
============

Secure your **Web API 2** using **JWT (Json Web Token)** or your own implementation. **Basic Authorization** is used for the token request and **Bearer Authorization** using a token for API requests. The project provides a token endpoint against which a user can authorize to get a token. Further requests to the api can then be made with the token. There is a built in authorization handler that validates this token for all requests made - it secures all api endpoints. 

WebApiSecure is available via **Nuget**: `install-package WebApiSecure`

###Dependency Injection

WebApiSecure makes use of dependency injection using a **Unity container** which allows you to specify your own implementation for credential validation, token creation and validation. A default implementation using a **JWT (Json Web Token)** is provided in the form of 3 services:

1. CredentialService
2. TokenService
3. ValidationService

#Getting started

1. Open Package Manager Console and type: `install-package WebApiSecure`
  * After installation you will see **UnityConfig.cs** added below App_Start and a new **Services** folder with the 3 default implmentation services mentioned above:
  * ![alt tag](http://googledrive.com/host/0BwwmUpymTB5WeWhkbU1iYkg3ZGs/solution.jpg)
2. Add reference to `System.IdentityModel` if needed.
3. Modify **CredentialService.cs**
  * Add code to validate credential in the `IsValidCredential` method
  * Return an `IClaim` object in `GetClaim` based on the credentials. Basically, the claims associated with the credential
  * Change the parsing of the authorization header if needed. The default uses Base64 encoding.
4. Modify **TokenService.cs**
  * Change the JWT token settings: issuer,audience and lifetime
  * Set your secret symmetric key to be used for signing the token in `CreateSigningCredentials()`
5. Modify **ValidationService.cs**
  * Change the `TokenValidationParameters` and set your secret symmetric signing key
6. Wire up the services in **UnityConfig.cs** 
  * Set the allowed route to the token endpoint via parameter injection. This route will be ignored by the `AuthHandler`, so that users can make a request to the token endpoint.
  * ![alt tag](http://googledrive.com/host/0BwwmUpymTB5WeWhkbU1iYkg3ZGs/UnityConfig.jpg)
7. Modify WebApiConfig.cs
  * Set the route to the token endpoint. WebApiSecure uses a `TokenController` that exposes to POST endpoints: `PostSecure` and `Post`. `PostSecure` should be used since it requires a secure SSL connection to submit the client credentials. For testing purposes the unsecure `Post` may be used.
  * Secure your api by enabling the `AuthHandler` by adding it as a MessageHandler
  * ![alt tag](http://googledrive.com/host/0BwwmUpymTB5WeWhkbU1iYkg3ZGs/WebApiConfig.jpg)

#Test it
Fire up **Fiddler** to test your secured API:

1. Make a GET request. This should return HTTP status 400 - Bad Request, since no authorization header is present the request fails.
  * ![alt tag](http://googledrive.com/host/0BwwmUpymTB5WeWhkbU1iYkg3ZGs/BadRequest.jpg)
2. Make a POST request to the token endpoint to get a token. Set header: `Authorization: Basic yourBase64Credentials`. Copy the returned token.
  * ![alt tag](http://googledrive.com/host/0BwwmUpymTB5WeWhkbU1iYkg3ZGs/TokenRequest.jpg)
3. Make a valid GET/POST request. Set header: `Authorization: Bearer yourTokenString`
  * ![alt tag](http://googledrive.com/host/0BwwmUpymTB5WeWhkbU1iYkg3ZGs/ApiRequest.jpg)

#Roll your own

###Interfaces to implement
You can roll your own by implementing the following interfaces and then wiring your classes up in `UnityConfig.cs`:

1. IClaim
2. ITokenBuilder
3. IValidateToken
4. IValidateCredential
