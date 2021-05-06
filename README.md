# TeamProject-Manager-Api
Project Idea is to create web Api to manage project within a comapny, so team's can handle and manage their projects more easily.
Project is created in C# and .Net Framework. Testing in this project is via Postman and Swagger UI.


## Technologies and Tools

##### Technologies
* .Net Core
* Entity Framework Core
* AutoMapper
* NLog
* FluentValidation
* SQL Server
* Swagger

##### Tools
* Visual Studio Code
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

## SQL Server Diagram
![Alt text](https://github.com/MatthewKsc/TeamProject-Manager-Api/tree/master/TeamProject-Manager-Api/markdown/images/MSSQL.png "sqlServer")
<br>
<br>
## Swagger UI
![Alt text](https://github.com/MatthewKsc/TeamProject-Manager-Api/tree/master/TeamProject-Manager-Api/markdown/images/SwaggerUI.png "sqlServer")

