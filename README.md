**Lightmap** is a simple, fast, database modeling API for .Net projects.

_Note: this project is still under heavy development. NuGet packages haven't been shipped and breaking API changes should be expected._

# Summary
- Define data model independently of data store.
- Use different databases across different environments with the same data model and code.
- Supported runtimes: .NET Core, Full .NET Framework and Mono.
- Supported Languages: C#, Visual Basic.
  - F# should work as well, but has not been tested.

# Features

- Table constraints
  - Foreign Key
  - Unique
  - Not Null
- Migration support
  - Migrate a database forward
  - Migrate a database backward

# Examples

### Untyped Data Modeling
You can model database tables with just strings and Types.

```
var dataModel = new DataModel();

dataModel.AddTable(schemaName: "dbo", tableName: "Foo")
  .AddColumn(typeof(int), "Id")
    IsPrimaryKey()
  .AddColumn(typeof(string), "Name")
  .AddColumn(typeof(DateTime), "CreatedOn")
  .AddColumn(typeof(int), "LastState")
    .IsNullable();
```

### Strongly typed Modeling
Strongly typing your database tables provides a little more safety, ensuring that your constraints and references all are correctly spelled.

When a Type is provided as the table generic argument, all of the properties on the model are turned into columns. You may then alter individual properties as needed.

```
var dataModel = new DataModel();

dataModel.AddTable<AspNetRoles>("dbo")
  .AlterColumn(model => model.Id)
    .IsPrimaryKey()
    .GetOwner()
  .AlterColumn(model => model.Name)
    .Unique()
```

### Strongly typed with Anonymous objects
Lightmap allows you to create your tables with anonymous objects, so you can still have a strongly typed data model, without having to create a bunch of classes yourself.

```
var dataModel = new DataModel();

dataModel.AddTable("dbo", "Foo", () => new
  {
      Id = default(Guid),
      Name = default(string),
      CreatedOn = default(DateTime)
  })
  .AlterColumn(model => model.Id)
    .IsPrimaryKey()
  .AlterColumn(model => model.Name)
    .Unique();
```

### Performing a migration

You can migrate your database to new datamodels by defining the new model in a class that implements the `IMigration` interface.

```
public class InitialDataModel : IMigration
{
    public InitialDataModel(IDataModel currentModel) => this.DataModel = currentModel;

    public IDataModel DataModel { get; }

    public void Apply()
    {
        DataModel.AddTable("dbo", "User")
            .AddColumn(typeof(int), "Id").GetOwner()
            .AddColumn(typeof(string), "Name");
    }

    public void Revert()
    {
        // Code to revert:
    }
}
```

Once you have one or more migrations defined, you can process them with the datastore of your choice, providing it has an implementation of `IDatabaseManager` and `IDatabaseMigrator`.

```
public class Program
{
    public static void Main()
    {
        IDatabaseManager databaseManager = new SqliteDatabaseManager("MyDatabase", "DATA SOURCE=MyDatabase");
        IDataModel dataModel = new DataModel();
        IMigration initialMigration = new InitialDataModel(dataModel);
        IDatabaseMigrator migrator = new SqliteMigrator(initialMigration);

        // Apply migration to database.
        migrator.Apply(databaseManager);
    }
}
```