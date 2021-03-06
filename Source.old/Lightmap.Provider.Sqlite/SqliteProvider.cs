﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Modeling;
using Dapper;

namespace Lightmap.Provider.Sqlite
{
    public class SqliteProvider : IDataProvider
    {
        private static Dictionary<Type, string> typeConversionMapping = new Dictionary<Type, string>();
        private const string _createTable = "CREATE TABLE ";

        private List<string> queryHistory;

        public SqliteProvider(DatabaseManager manager)
        {
            this.DatabaseManager = manager;
            this.queryHistory = new List<string>();
            this.QueryProvider = new Querying.SqliteProvider2();
        }

        public DatabaseManager DatabaseManager { get; }

        public IEnumerable<string> QueryHistory => this.queryHistory;

        public Querying.SqliteProvider2 QueryProvider { get; }

        public Task ExecuteAsync()
        {
            return Task.FromResult(0);
        }

        public async Task ProcessMigration(IMigration migration)
        {
            IDatabaseModelBrowser modeler = this.DatabaseManager.GetDatabaseModelBrowser(migration);
            IEnumerable<ITable> tables = modeler.GetTables();

            string finalSqlStatement = string.Empty;
            using (var connection = await this.DatabaseManager.OpenSqliteConnectionAsync())
            {
                foreach (ITable table in tables)
                {
                    ITableModeler tableModel = table.GetTableModeler();
                    Dictionary<string, string> tableDefinition = tableModel.GetDefinitions();
                    string sql = this.ProcessTable(table, tableModel, tableDefinition);
                    
                    this.queryHistory.Add(sql);
                    await connection.ExecuteAsync(sql: sql, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                }
            }
        }

        private string ProcessTable(ITable table, ITableModeler tableModel, Dictionary<string, string> tableDefinition)
        {
            // We use string concatenation as it is faster than string builder or string interpolation.
            var sql = string.Empty;
            sql += _createTable + table.Name + "\n(\n\t";
            IColumn[] columns = tableModel.GetColumns();
            int columnCount = columns.Length;
            string constraint = null;
            for (int index = 0; index < columnCount; index++)
            {
                IColumn column = columns[index];

                sql += column.Name + " " + this.ConvertTypeToSqlType(column.DataType);

                Dictionary<string, string> columnDefinition = column.GetColumnModeler().GetDefinitions();

                if (columnDefinition.TryGetValue(SqlStatements.Constraints.NotNull, out constraint))
                {
                    sql += " " + SqlStatements.Constraints.NotNull;
                }

                if (columnDefinition.TryGetValue(SqlStatements.Constraints.PrimaryKey, out constraint))
                {
                    sql += " " + SqlStatements.Constraints.PrimaryKey;
                }

                if (columnDefinition.TryGetValue(SqlStatements.Constraints.AutoIncrement, out constraint))
                {
                    sql += " " + SqlStatements.Constraints.AutoIncrement;
                }

                // If we have more, add a comma to separate the columns.
                if (index != columnCount - 1)
                {
                    sql += ", \n\t";
                }
            }

            if (tableDefinition.TryGetValue(SqlStatements.Constraints.ForeignKey, out constraint))
            {
                // We add the missing comma that was skipped by the column building.
                sql += ",\n";
                string referenceTable = tableDefinition[SqlStatements.Constraints.ReferencesTable];
                string referenceColumn = tableDefinition[SqlStatements.Constraints.ReferencesColumn];
                sql += "\tCONSTRAINT \"FK_" + table.Name + "_" + referenceTable + "_" + referenceColumn + "\"";
                sql += " " + SqlStatements.Constraints.ForeignKey + " (\"" + constraint + "\") REFERENCES \"" + referenceTable + "\" (\"" + referenceColumn + "\")";
            }

            return sql += "\n)";
        }

        private string ConvertTypeToSqlType(Type dataType)
        {
            string convertedDataType = null;
            if (typeConversionMapping.TryGetValue(dataType, out convertedDataType))
            {
                return convertedDataType;
            }

            if (dataType == typeof(string))
            {
                convertedDataType = "TEXT";
                typeConversionMapping.Add(dataType, convertedDataType);
                return convertedDataType;
            }
            else if (dataType == typeof(int))
            {
                convertedDataType = "INTEGER";
                typeConversionMapping.Add(dataType, convertedDataType);
                return convertedDataType;
            }

            throw new InvalidOperationException("The data type specified for the column is not supported by the provider.");
        }
    }
}