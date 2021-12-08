 # Technical test

Here is my implementation for the http server that is protect by authentication and returns a simple response.

## Solution Architecture

I followed the clean architecture template from jason taylor, as recommanded you can find it in `CSharpExercise\src` and `CsharpExerciseTest\`.

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebUI

This layer is a single page application based on Angular 10 and ASP.NET Core 5. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

### Unit Test

Source code is provided with unit Test.

## API Documentation

```API
GET http://localhost:5000/user 
```
This return `Status code 200`  and current authenticated user informations as followed :

```json
    {
        "ID": "3",
        "FirstName": "Emma",
        "LastName": "Taume",
        "Email": "emma@taume.com"
    }
```

or `Status code 401` if not authentified.

### Status code

| Status Code | Description |
| :--- | :--- |
| 200 | `OK` |
| 401 | `UNAUTHORIZED` |
| 500 | `INTERNAL SERVER ERROR` |

## Deployment and Testing

### Makefile

If you are using windows, you may need to download `Chocolatey` to install `make.exe` in order to make it run properly :  [here](https://community.chocolatey.org/packages/make)

I created a makefile in order to deploy the solution in the easiest way as possible.
Open a new terminal while on the root folder of the solution `CSharpExercise` (where the makefile is) and use :

```shell
make [Args] 
```

| Args | Description |
| --- | --- |
| build | Building postgres db and the app |
| start | Starting postgres db and the app in the two separate docker container |
| user_success | Sending a request to the server with an authenticated user |
| user_failed | Sending a request to the server with an unauthenticated user |
| stop | Stopping both docker container |

Use in the above order to test properly the solution.

## Logs

We can find live logs on the server while running the project directly.
You can also find in file logs on the server under `/app/Logs`.
There is one log per day, and all requests are listed with date and time.
