using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lightmap.Migration
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
            var migrator = Mock.Of<IDatabaseMigrator>();
            var dataModel = new DataModel(migrator);
            ITableBuilder tableBuilder = dataModel.AddTable(_schema, _tableName);

            // Act
            IColumnBuilderUntyped columnBuilder = tableBuilder.AddColumn(null, "Id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UntypedTable_WithNullColumnName_ThrowsException()
        {
            // Arrange
            var migrator = Mock.Of<IDatabaseMigrator>();
            var dataModel = new DataModel(migrator);
            ITableBuilder tableBuilder = dataModel.AddTable(_schema, _tableName);

            // Act
            IColumnBuilderUntyped columnBuilder = tableBuilder.AddColumn(typeof(int), null);
        }

        [TestMethod]
        public void UntypedTable_WithDataTypeAndName_ReturnsUntypedColumnBuilder()
        {
            // Arrange
            var migrator = Mock.Of<IDatabaseMigrator>();
            var dataModel = new DataModel(migrator);
            ITableBuilder tableBuilder = dataModel.AddTable(_schema, _tableName);

            // Act
            IColumnBuilderUntyped columnBuilder = tableBuilder.AddColumn(typeof(int), "Id");

            // Assert
            Assert.IsNotNull(columnBuilder);
            Assert.AreEqual(_schema, tableBuilder.Schema);
            Assert.AreEqual(_tableName, tableBuilder.TableName);
            Assert.AreEqual("Id", columnBuilder.ColumnName);
            Assert.AreEqual(typeof(int), columnBuilder.ColumnDataType);
        }

        [TestMethod]
        public void UntypedTable_ReturnsColumns()
        {
            // Arrange
            var migrator = Mock.Of<IDatabaseMigrator>();
            var dataModel = new DataModel(migrator);
            ITableBuilder tableBuilder = dataModel.AddTable(_schema, _tableName);
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
            var migrator = Mock.Of<IDatabaseMigrator>();
            var dataModel = new DataModel(migrator);
            ITableBuilder tableBuilder = dataModel.AddTable(_schema, _tableName);
            IColumnBuilderUntyped intColumn = tableBuilder.AddColumn(typeof(int), "Id");
            IColumnBuilderUntyped stringColumn = tableBuilder.AddColumn(typeof(string), "Name");

            // Act
            ITableModel model = tableBuilder.GetTableModel();

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(_schema, model.Schema.Name);
            Assert.AreEqual(_tableName, model.Name);
            Assert.AreEqual(2, model.Columns.Length);
            //Assert.AreEqual(model.Columns[0].ColumnName, "Id");
            //Assert.AreEqual(model.Columns[0].ColumnDataType, typeof(int));
            //Assert.AreEqual(model[1].ColumnName, "Name");
            //Assert.AreEqual(model[1].ColumnDataType, typeof(string));
        }
    }
}
