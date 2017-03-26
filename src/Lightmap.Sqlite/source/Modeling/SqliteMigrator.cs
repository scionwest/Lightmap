using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Lightmap.Modeling
{
    public class SqliteMigrator : IDatabaseMigrator
    {
        private const string _createTable = "CREATE TABLE";
        private readonly List<string> migrationHistory;

        public SqliteMigrator(params IMigration[] migrations)
        {
            this.Migrations = migrations;
            this.migrationHistory = new List<string>();
        }

        public SqliteMigrator(string defaultSchema, params IMigration[] migrations) : this(migrations)
        {
            if (string.IsNullOrWhiteSpace(defaultSchema))
            {
                throw new ArgumentNullException(nameof(defaultSchema), "When using the constructor overload that takes a default schema name, you must provide a valid schema name.");
            }

            this.DefaultSchema = defaultSchema;
        }

        public IMigration[] Migrations { get; }

        public string DefaultSchema { get; } = "main";

        public bool IsMigrationNeeded()
        {
            throw new NotImplementedException();
        }

        public void Apply(IDatabaseManager databaseManager)
        {
            int dbVersion = 0;
            using (IDbConnection connection = databaseManager.OpenConnection())
            {
                var schemaVersion = connection.ExecuteScalar("Pragma schema_version");
                if (!int.TryParse(schemaVersion.ToString(), out dbVersion))
                {
                    // TODO: throw exception here.
                }
            }

            // Return all migrations that have a version attribute assigned to them, that are greater than our current version.
            IEnumerable<IMigration> migrationsRemainingToUpgrade = this.Migrations.Where(
                migration => AttributeCache.GetAttribute<MigrationVersionAttribute>(migration.GetType()) != null);

            using (var connection = databaseManager.OpenConnection())
            {
                foreach (IMigration migration in this.Migrations)
                {
                    foreach (string sqlStatement in this.GenerateStatements(migration))
                    {
                        connection.Execute(sqlStatement);
                        this.migrationHistory.Add(sqlStatement);
                    }
                }
            }
        }

        public async Task ApplyAsync(IDatabaseManager databaseManager)
        {
            int dbVersion = 0;
            using (IDbConnection connection = await databaseManager.OpenConnectionAsync())
            {
                var schemaVersion = connection.ExecuteScalar("Pragma schema_version");
                if (!int.TryParse(schemaVersion.ToString(), out dbVersion))
                {
                    // TODO: throw exception here.
                }
            }

            // Return all migrations that have a version attribute assigned to them, that are greater than our current version.
            IEnumerable<IMigration> migrationsRemainingToUpgrade = this.Migrations.Where(
                migration => AttributeCache.GetAttribute<MigrationVersionAttribute>(migration.GetType()) != null);

            using (var connection = await databaseManager.OpenConnectionAsync())
            {
                foreach (IMigration migration in this.Migrations)
                {
                    foreach (string sqlStatement in this.GenerateStatements(migration))
                    {
                        await connection.ExecuteAsync(sqlStatement);
                        this.migrationHistory.Add(sqlStatement);
                    }
                }
            }
        }

        public IEnumerable<string> GenerateStatements(IMigration migration)
        {
            migration.Apply();
            ITableBuilder[] tables = migration.DataModel.GetTables();
            string sqlStatement = string.Empty;

            foreach (ITableBuilder tableBuilder in tables)
            {
                string tableSchema = tableBuilder.Schema == null
                    ? this.DefaultSchema
                    : tableBuilder.Schema.Name;
                sqlStatement += $"{_createTable} {tableSchema}.{tableBuilder.TableName} (\n\t";

                IColumnBuilder[] columns = tableBuilder.GetColumns();
                string constraint = string.Empty;

                for (int index = 0; index < columns.Length; index++)
                {
                    IColumnBuilder currentColumn = columns[index];
                    sqlStatement += this.GenerateColumn(currentColumn);

                    if (index != columns.Length - 1)
                    {
                        sqlStatement += ", \n\t";
                    }
                }

                sqlStatement += SqliteMigrator.GenerateTableConstraints(tableBuilder);
                sqlStatement += "\n)";

                yield return sqlStatement;
                sqlStatement = string.Empty;
            }
        }

        private static string GenerateTableConstraints(ITableBuilder tableBuilder)
        {
            Dictionary<string, string> tableDefinition = tableBuilder.GetTableDefinition();
            string constraint = string.Empty;
            string sqlStatement = string.Empty;

            if (tableDefinition.TryGetValue(ColumnDefinitions.ForeignKey, out constraint))
            {
                sqlStatement += ", \n";
                string referenceSchema = tableDefinition[ColumnDefinitions.ReferencesSchema];
                string referenceTable = tableDefinition[ColumnDefinitions.ReferencesTable];
                string referenceColumn = tableDefinition[ColumnDefinitions.ReferencesColumn];
                sqlStatement += "\tCONSTRAINT \"FK_" + tableBuilder.TableName + "_" + referenceTable + "_" + referenceColumn + "\"";
                sqlStatement += " " + ColumnDefinitions.ForeignKey + " (\"" + constraint + "\") REFERENCES \"" + referenceSchema + "." + referenceTable + "\" (\"" + referenceColumn + "\")";
            }

            return sqlStatement;
        }

        private string GenerateColumn(IColumnBuilder columnBuilder)
        {
            string sqlStatement = $"\t{columnBuilder.ColumnName} {this.ConvertTypeToSqlType(columnBuilder.ColumnDataType)}";
            string constraint = string.Empty;

            Dictionary<string, string> columnDefinition = columnBuilder.GetColumnDefinition();

            if (columnDefinition.TryGetValue(ColumnDefinitions.NotNull, out constraint))
            {
                sqlStatement += $" {ColumnDefinitions.NotNull}";
            }

            if (columnDefinition.TryGetValue(ColumnDefinitions.Unique, out constraint))
            {
                sqlStatement += " UNIQUE";
            }

            if (columnDefinition.TryGetValue(ColumnDefinitions.PrimaryKey, out constraint))
            {
                sqlStatement += " PRIMARY KEY";
            }

            if (columnDefinition.TryGetValue(ColumnDefinitions.AutoIncrement, out constraint))
            {
                sqlStatement += $" {ColumnDefinitions.AutoIncrement}";
            }

            return sqlStatement;
        }

        protected virtual string ConvertTypeToSqlType(Type dataType)
        {
            if (dataType == typeof(string) || dataType == typeof(DateTime))
            {
                return "TEXT";
            }
            else if (dataType == typeof(int) || dataType == typeof(short) || dataType == typeof(long) || dataType == typeof(bool))
            {
                return "INTEGER";
            }
            else if (dataType == typeof(float) || dataType == typeof(decimal) || dataType == typeof(double))
            {
                return "REAL";
            }

            throw new InvalidOperationException("The data type specified for the column is not supported by the provider.");
        }
    }
}
