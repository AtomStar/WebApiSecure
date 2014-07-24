WebApiSecure
============

Secure your Web API 2 using JWT (JsonWebToken) or your own implementation. The project provides a token endpoint against which a user can authorize to get a token. Further requests to the api can then be made using the token. There is a built in authorization handler that validates this token for all requests made - it secures all api endpoints. 

**Dependency Injection**

WebApiSecure makes use of dependency injection using a Unity container which allows you to specify your own implementation for credential validation, token creation and validation. A default implemtation using a JWT (Json Web Token) is provided in the form of 3 services:

1. CredentialService
2. TokenService
3. ValidationService

Interfaces to implement
