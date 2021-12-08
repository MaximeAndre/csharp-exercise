 # Technical test

Here is my implementation for the http server that is protect by authentication and returns a simple response.

## Solution Architecture

I followed the clean architecture template from jason taylor, as recommanded you can find it in `CSharpExercise\src`.

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebUI

This layer is a single page application based on Angular 10 and ASP.NET Core 5. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.


## API Documentation

```http
GET http://localhost:5000/user 
```
This return current authenticated user informations as followed :

```json
    {
        "ID": "3",
        "FirstName": "Emma",
        "LastName": "Taume",
        "Email": "emma@taume.com"
    }
```

## Deployment and Testing

### Makefile

I created a makefile in order to deploy the solution in the easiest way as possible.
Open a new terminal while on the root folder of the solution and use :
```shell
make [Args] 
```

| Args | Description |
| --- | --- |
| start | Starting postgres db and the app in the two separate docker container |
| user_success | Sending a request to the server with an authenticated user |
| user_failed | Sending a request to the server with an unauthenticated user |
| stop | Stopping both docker container |

### Status code

| Status Code | Description |
| :--- | :--- |
| 200 | `OK` |
| 401 | `UNAUTHORIZED` |
| 500 | `INTERNAL SERVER ERROR` |

## Logs

We can find logs while running the project directly on the serveur while sending requests.
You can also find in file log on the serveur under `/app/Logs`.
There is one log per day, and all request are listed and dated.