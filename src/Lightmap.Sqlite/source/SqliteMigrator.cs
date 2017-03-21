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
        private List<string> migrationHistory;

        public SqliteMigrator(params IMigration[] migrations)
        {
            this.Migrations = migrations;
            this.migrationHistory = new List<string>();
        }

        public IMigration[] Migrations { get; }

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
                    // throw exception here.
                }
            }

            // Return all migrations that have a version attribute assigned to them, that are greater than our current version.
            IEnumerable<IMigration> migrationsRemainingToUpgrade = this.Migrations.Where(migration =>
            {
                //MigrationVersionAttribute migrationVersion = AttributeCache.GetAttribute<MigrationVersionAttribute>(migration.GetType());
                //return migrationVersion != null;
                return true;
            });

            using (var connection = databaseManager.OpenConnection())
            {
                foreach (IMigration migration in this.Migrations)
                {
                    foreach(string sqlStatement in this.GetSqlStatements(migration))
                    {
                        connection.Execute(sqlStatement);
                        this.migrationHistory.Add(sqlStatement);
                    }
                }
            }
        }

        public Task ApplyAsync(IDatabaseManager databaseManager)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> GetSqlStatements(IMigration migration)
        {
            migration.Apply();
            ITableBuilder[] tables = migration.DataModel.GetTables();
            string sqlStatement = string.Empty;

            foreach(ITableBuilder tableBuilder in tables)
            {
                sqlStatement += $"{_createTable} {tableBuilder.Schema}.{tableBuilder.TableName} (\n\t";
                
                IColumnBuilder[] columns = tableBuilder.GetColumns();
                string constraint = string.Empty;

                for (int index = 0; index < columns.Length; index++)
                {
                    IColumnBuilder currentColumn = columns[index];
                    sqlStatement += $"\t{currentColumn.ColumnName} {this.ConvertTypeToSqlType(currentColumn.ColumnDataType)}";
                    Dictionary<string, string> columnDefinition = currentColumn.GetColumnDefinition();

                    if (columnDefinition.TryGetValue(ColumnDefinitions.NotNull, out constraint))
                    {
                        sqlStatement += $" {ColumnDefinitions.NotNull}";
                    }

                    if (columnDefinition.TryGetValue(ColumnDefinitions.Unique, out constraint))
                    {
                        sqlStatement += " UNIQYE";
                    }

                    if (columnDefinition.TryGetValue(ColumnDefinitions.PrimaryKey, out constraint))
                    {
                        sqlStatement += " PRIMARY KEY";
                    }

                    if (columnDefinition.TryGetValue(ColumnDefinitions.AutoIncrement, out constraint))
                    {
                        sqlStatement += $" {ColumnDefinitions.AutoIncrement}";
                    }

                    if (index != columns.Length - 1)
                    {
                        sqlStatement += ", \n\t";
                    }
                }

                Dictionary<string, string> tableDefinition = tableBuilder.GetTableDefinition();
                if (tableDefinition.TryGetValue(ColumnDefinitions.ForeignKey, out constraint))
                {
                    sqlStatement += ", \n";
                    string referenceTable = tableDefinition[ColumnDefinitions.ReferencesTable];
                    string referenceColumn = tableDefinition[ColumnDefinitions.ReferencesColumn];
                    sqlStatement += "\tCONSTRAINT \"FK_" + tableBuilder.TableName + "_" + referenceTable + "_" + referenceColumn + "\"";
                    sqlStatement += " " + ColumnDefinitions.ForeignKey + " (\"" + constraint + "\") REFERENCES \"" + referenceTable + "\" (\"" + referenceColumn + "\")";
                }

                sqlStatement += "\n)";
                yield return sqlStatement;
            }
        }

        private string ConvertTypeToSqlType(Type dataType)
        {
            if (dataType == typeof(string) || dataType == typeof(DateTime))
            {
                return "TEXT";
            }
            else if (dataType == typeof(int))
            {
                return "INTEGER";
            }

            throw new InvalidOperationException("The data type specified for the column is not supported by the provider.");
        }
    }
}
