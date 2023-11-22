# KNU-2023 Tournament
#### [Soka team solution]()

## Summary
* Authorization/Registration with Google or Apple accounts are not implemented ❌
* You are able to create/remove/update/get/authenticate users (read the doc) 
* Supports [Swagger]() and [SwaggerUI]()

## Dependencies (.NET 6.0)
* Microsoft.EntityFrameworkCore.SqlServer (6.0.20)
* Microsoft.EntityFrameworkCore.Tools (6.0.14)
* AutoMapper.Extensions.Microsoft.DependencyInjection (6.1.1)
* Swashbuckle.AspNetCore (6.5.0)

## Installation
You have to install all the project dependencies. \
Listed on <b>top</b>.

## Preparations
1. You have to update <b>appsettings.json</b> (ConnectionStrings -> DefaultConnection). \
<u><b><i>It's required to connect to the database.</i></b></u>
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "DATABASE_URI"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information", 
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
2. Migrate database (commands are only available in packet manager CLI)
```shell
> Add-Migration "Database init"

> Update-Database
```

## Allowed actions
#### Create user -> api/user/create [HttpPost]()
<i>Request body</i>
```json
{
  "email": "string",
  "password": "string"
}
```

<i>Response</i> 
* 204 - Success
* 400 - Fail
* 500 - Fail
---

#### Delete user -> api/user/delete/{email} [HttpDelete]()
> <i>No request body</i>

<i>Response</i> 
* 204 - Success
* 400 - Fail
* 404 - Fail
* 500 - Fail

---

#### Update user -> api/user/update/{email} [HttpPut]()
* <b><u>changePassword</u></b> property should be set to true if you want to change the password, otherwise the password won't be changed.
* ! Changing <b><u>isActive</u></b> property will freeze the user. Authentication won't be able due to the state update.

<i>Request body</i>
```json
{
  "email": "string",
  "password": "string",
  "isActive": true,
  "changePassword": true
}
```

<i>Response</i> 
* 204 - Success
* 400 - Fail
* 500 - Fail

---

#### Get user list -> api/user/list [HttpGet]()
> <i>No request body</i>

<i>Response</i>

* 200 - Success
```json
[
  {
    "email": "string",
    "isActive": true,
    "creationDate": "string"
  }
]
```
* 400 - Fail
---

#### Get user -> api/user/{email} [HttpGet]()
> <i>No request body</i>

<i>Response</i>
* 200 - Success
```json
{
  "email": "string",
  "isActive": true,
  "creationDate": "string"
}
```
* 404 - Fail

#### Authenticate user -> api/user/auth [HttpPost]()
<i>Request Body</i>
```json
{
  "email": "string",
  "password": "string"
}
```

<i>Response</i> 
* 204 - Success
* 400 - Fail
* 500 - Fail