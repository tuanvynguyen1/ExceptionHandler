# ExceptionHandler

### Prerequisites 

- .NET Core 8.0+ SDK
- VisualStudio
- SqlServer(2012+)
  
### Setup

1.  Clone this project
2.  Open ExceptionHandler.sln
![appsetting](https://github.com/tuanvynguyen1/ExceptionHandler/tree/master/images/appsettings.png) 
```
Data Source=<Name_Server>;Initial Catalog=<Name_DB>;User ID=<User>;Password=<Password>;Multiple Active Result Sets=True;Trust Server Certificate=True
```
3. Tools -> Nugget Package Manager -> Package Manager Console 
4. Run command
```
nuget restore
update-Database
```
5. Run Project 
![runproject](https://github.com/tuanvynguyen1/ExceptionHandler/tree/master/images/run.png) 