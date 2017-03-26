using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lightmap.Modeling
{
    [TestClass]
    public class TableBuilderTests
    {
        private const string _tableName = "Foo";
        private const string _schema = "dbo";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UntypedTable_WithNullDataType_ThrowsException()
        {
            // Arrange
            var dataModel = new DataModel();
            ISchemaModel schemaModel = dataModel.AddSchema(_schema).GetSchemaModel();
            ITableBuilder tableBuilder = dataModel.AddTable(_tableName, schemaModel);

            // Act
            IColumnBuilderUntyped columnBuilder = tableBuilder.AddColumn(null, "Id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UntypedTable_WithNullColumnName_ThrowsException()
        {
            // Arrange
            var dataModel = new DataModel();
            ISchemaModel schemaModel = dataModel.AddSchema(_schema).GetSchemaModel();
            ITableBuilder tableBuilder = dataModel.AddTable(_tableName, schemaModel);

            // Act
            IColumnBuilderUntyped columnBuilder = tableBuilder.AddColumn(typeof(int), null);
        }

        [TestMethod]
        public void UntypedTable_WithDataTypeAndName_ReturnsUntypedColumnBuilder()
        {
            // Arrange
            var dataModel = new DataModel();
            ISchemaModel schemaModel = dataModel.AddSchema(_schema).GetSchemaModel();
            ITableBuilder tableBuilder = dataModel.AddTable(_tableName, schemaModel);

            // Act
            IColumnBuilderUntyped columnBuilder = tableBuilder.AddColumn(typeof(int), "Id");

            // Assert
            Assert.IsNotNull(columnBuilder);
            Assert.AreEqual(schemaModel, tableBuilder.Schema);
            Assert.AreEqual(_tableName, tableBuilder.TableName);
            Assert.AreEqual("Id", columnBuilder.ColumnName);
            Assert.AreEqual(typeof(int), columnBuilder.ColumnDataType);
        }

        [TestMethod]
        public void UntypedTable_ReturnsColumns()
        {
            // Arrange
            var dataModel = new DataModel();
            ISchemaModel schemaModel = dataModel.AddSchema(_schema).GetSchemaModel();
            ITableBuilder tableBuilder = dataModel.AddTable(_tableName, schemaModel);
            IColumnBuilderUntyped intColumn = tableBuilder.AddColumn(typeof(int), "Id");
            IColumnBuilderUntyped stringColumn = tableBuilder.AddColumn(typeof(string), "Name");

            // Act
            IColumnBuilder[] columnBuilders = tableBuilder.GetColumns();

            // Assert
            Assert.IsNotNull(columnBuilders);
            Assert.AreEqual(2, columnBuilders.Length);
            Assert.AreEqual(columnBuilders[0].ColumnName, "Id");
            Assert.AreEqual(columnBuilders[0].ColumnDataType, typeof(int));
            Assert.AreEqual(columnBuilders[1].ColumnName, "Name");
            Assert.AreEqual(columnBuilders[1].ColumnDataType, typeof(string));
        }

        [TestMethod]
        public void UntypedTable_ReturnsTableModel()
        {
            // Arrange
            var dataModel = new DataModel();
            ISchemaModel schemaModel = dataModel.AddSchema(_schema).GetSchemaModel();
            ITableBuilder tableBuilder = dataModel.AddTable(_tableName, schemaModel);
            IColumnBuilderUntyped intColumn = tableBuilder.AddColumn(typeof(int), "Id");
            IColumnBuilderUntyped stringColumn = tableBuilder.AddColumn(typeof(string), "Name");

            // Act
            ITableModel model = tableBuilder.GetTableModel();
            IColumnModel[] columns = model.GetColumns();

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(_schema, model.Schema.Name);
            Assert.AreEqual(_tableName, model.Name);
            Assert.AreEqual(2, columns.Length);
            //Assert.AreEqual(model.Columns[0].ColumnName, "Id");
            //Assert.AreEqual(model.Columns[0].ColumnDataType, typeof(int));
            //Assert.AreEqual(model[1].ColumnName, "Name");
            //Assert.AreEqual(model[1].ColumnDataType, typeof(string));
        }
    }
}
