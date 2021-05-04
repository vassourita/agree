# Allow :lock:

Allow is the auth server for Agree. It uses Asp.Net IdentityServer to handle auth and profiles.

I also serve the [Concord](./src/Agree.Allow.Presentation/ClientApp) dashboard statically from the same server for security and integration reasons. It was created with the ```dotnet new react``` template, so there is a lot of integration codebase to be adapted yet.