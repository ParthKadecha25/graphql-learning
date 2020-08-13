# GraphQL API - Blogs Management
> Created By : Parth Kadecha on 29-Apr-2020
 
This solution contains 3 solutions
- BlogManagement.Core
- BlogManagement.Data
- BlogManagement.GraphQLAPI

# BlogManagement.Core

This solution contains the models and interface repository for each entities.
There are main 3 models/entities
- Author
- Category
- Post

Interface repository contains all the operations which can be performed on the entity

# BlogManagement.Data

Here, Code-First architecture is used to create the database. If you are using the solution for the first time than update the connection string as per your requirement in BlogContext.cs file

### In BlogContext.cs
```
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer(@"data source=CIPL-8PC164\SQLEXPRESS;initial catalog=Blogs;integrated security=True;MultipleActiveResultSets=True;");
}
```
Once the connection string is setup, navigate to NUGet Package Manager Console, and run the below commands. Also make suew that in Package Manager Console, default selected project is BlogManagement.Data

- add-migration
  - When asks for "name", just provide the unique migration name
  - This command will create the migration file which will be later used for creating the database
- update-database
    - this will create the database if doesn't exist or create one - if does not exists

Now you can check whether the database is created or not.

This solution also contains the implemention of all repository interfaces.

# BlogManagement.GraphQLAPI

This is the .Net Core Web API project. And in this we will create required queries and mutation methods to interact with Blogs database

Here are some of the important folders and it's details

| Folder/File | Description |
| ------ | ------- |
| Queries | It contains "Types" for each entity and the members which are added in paricular type can only be accessed using GraphQL. "Model" contains generic GraphQL query model. |
| Queries -> BlogsQuery.cs | Contains all the possible GET actions which can be used to query using GraphQL |
|||
| Mutations | Mutations allow to perform Add, Edit and Delete operation on the entity using GraphQL. It contains "InputTypes" classes which will hold the data required at the time of creation or updation. |
| Mutations -> BlogsMutation.cs" | Contains all functions for add, edit and delete for all entities. |
|BlogsSchema.cs | This contains the schema which need to be used for GraphQL and it will have mapping of query and mutation files to be used |
| Controllers -> GraphQLController.cs | This is the generic controller class which has to be used for performing any GraphQL operations. |
| Startup.cs | This file does all the required configuraion in "ConfigureServices" method. When any new "Type" or "InputType" has been added it needs to be added in this function too, for configuring the API |

# How to perform testing?

Open the GraphiQL page, after running the API solution.
> http://<siteURL>/GraphiQL

It will open the tool to test the query. It will also contain the details of all the types, queries and mutations available in the solution. This tool is useful for the front-end developer who is going to generate the queries.






