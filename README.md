# TeamProject-Manager-Api
Project Idea is to create web Api to manage project within a comapny, so team's can handle and manage their projects more easily.
Project is created in C# and .Net Framework. Project was tested with Unit Test's (NUnit and Moq) and Integration Test's (Postman and Swagger).


## Technologies and Tools

##### Technologies
* .Net Core
* Entity Framework Core
* AutoMapper
* NLog
* FluentValidation
* SQL Server
* Swagger
* Moq
* NUnit

##### Tools
* Visual Studio
* MS for SQL Server
* Postman
* Swagger UI

## Folders and explanation

* `\TeamProject-Manager-Api\dao\` -> Data access layout with db context, entity's, configurations of entity's
* `\TeamProject-Manager-Api\Controllers` -> Controller layout to map specific endpoints to proper action
* `\TeamProject-Manager-Api\Exceptions` -> Error Handling layout to catch exception within API
* `\TeamProject-Manager-Api\Installer` -> Folder in witch we register some of services in API container for example ComapnyService.cs
* `\TeamProject-Manager-Api\Migrations` -> Migrations created with Entity Framework Core
* `\TeamProject-Manager-Api\Services` -> Buissnes logic layout to perform action on database
* `\TeamProject-Manager.Test\Services\` -> Unit test's of Service Layout

## SQL Server Diagram
![Alt text](TeamProject-Manager-Api/markdown/images/MSSQL.png "sqlServer")
<br>
<br>
## Swagger UI
![Alt text](TeamProject-Manager-Api/markdown/images/SwaggerUI.png "sqlServer")

