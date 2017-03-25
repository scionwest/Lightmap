using System;
using Lightmap.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lightmap.Modeling
{
    [TestClass]
    public class DataModelTests
    {
        const string _schema = "dbo";

        [TestMethod]
        public void AddTable_FromNullSchemaString_DoesNotThrowException()
        {
            // Arrange
            var dataModel = new DataModel();
            string tableName = "foo";

            // Act
            var table = dataModel.AddTable(schemaName: null, tableName: tableName);

            // Assert
            Assert.IsNull(table.Schema);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTable_FromNullTableString_ThrowsException()
        {
            // Arrange
            var dataModel = new DataModel();

            // Act
            dataModel.AddTable(schemaName: _schema, tableName: null);
        }

        [TestMethod]
		public void AddTable_FromStrings_ReturnsBuilder()
        {
            // Arrange
            var dataModel = new DataModel();
            string tableName = "foo";

            // Act
            ITableBuilder tableBuilder = dataModel.AddTable(_schema, tableName);

            // Assert
            Assert.IsNotNull(tableBuilder);
            Assert.AreEqual(tableName, tableBuilder.TableName);
            Assert.AreEqual(_schema, tableBuilder.Schema);
        }

        [TestMethod]
        public void AddTable_WithSchemaModelNull_DoesNotThrowException()
        {
            // Arrange
            ISchemaModel schemaModel = Mock.Of<ISchemaModel>(mock => mock.Name == _schema);
            var dataModel = new DataModel();
            string tableName = "foo";

            // Act
            var table = dataModel.AddTable(schema: null, tableName: tableName);

            // Assert
            Assert.IsNull(table.Schema);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTable_WithTableNameNull_ThrowsException()
        {
            // Arrange
            ISchemaModel schemaModel = Mock.Of<ISchemaModel>(mock => mock.Name == _schema);
            var dataModel = new DataModel();

            // Act
            dataModel.AddTable(schema: schemaModel, tableName: null);
        }

        [TestMethod]
        public void AddTable_WithSchemaModelAndString_ReturnsBuilder()
        {
            // Arrange
            ISchemaModel schemaModel = Mock.Of<ISchemaModel>(mock => mock.Name == _schema);
            var dataModel = new DataModel();
            string tableName = "foo";

            // Act
            ITableBuilder tableBuilder = dataModel.AddTable(schemaModel, tableName);

            // Assert
            Assert.IsNotNull(tableBuilder);
            Assert.AreEqual(tableName, tableBuilder.TableName);
            Assert.AreEqual(_schema, tableBuilder.Schema);
        }

        [TestMethod]
        public void AddTable_WithSchemaStringNullAndType_DoesNotThrowException()
        {
            // Arrange
            var dataModel = new DataModel();

            // Act
            var table = dataModel.AddTable<AspNetRoles>(schemaName: null);

            // Assert
            Assert.IsNull(table.Schema);
        }

        [TestMethod]
        public void AddTable_WithSchemaStringAndType_ReturnsBuilder()
        {
            // Arrange
            var dataModel = new DataModel();

            // Act
            ITableBuilder tableBuilder = dataModel.AddTable<AspNetRoles>(_schema);

            // Assert
            Assert.IsNotNull(tableBuilder);
            Assert.AreEqual(typeof(AspNetRoles).Name, tableBuilder.TableName);
            Assert.AreEqual(_schema, tableBuilder.Schema);
        }

        [TestMethod]
        public void AddTable_WithSchemaModelNullAndType_DoesNotThrowException()
        {
            // Arrange
            var dataModel = new DataModel();

            // Act
            var table = dataModel.AddTable<AspNetRoles>(schema: null);

            // Assert
            Assert.IsNull(table.Schema);
        }

        [TestMethod]
        public void AddTable_WithSchemaModelAndType_ReturnsBuilder()
        {
            // Arrange
            ISchemaModel schemaModel = Mock.Of<ISchemaModel>(mock => mock.Name == _schema);
            var dataModel = new DataModel();

            // Act
            ITableBuilder tableBuilder = dataModel.AddTable<AspNetRoles>(schemaModel);

            // Assert
            Assert.IsNotNull(tableBuilder);
            Assert.AreEqual(typeof(AspNetRoles).Name, tableBuilder.TableName);
            Assert.AreEqual(_schema, tableBuilder.Schema);
        }

        [TestMethod]
        public void AddTable_WithSchemaStringNullAndAnonymousType_DoesNotThrowException()
        {
            // Arrange
            var dataModel = new DataModel();
            string tableName = "Foo";

            // Act
            var tableBuilder = dataModel.AddTable(null, tableName, 
                () => new { Id = default(int), Name = default(string), });

            // Assert
            Assert.IsNull(tableBuilder.Schema);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTable_WithTableStringNullAndAnonymousType_ThrowsException()
        {
            // Arrange
            var dataModel = new DataModel();

            // Act
            var tableBuilder = dataModel.AddTable(_schema, null, () => new
            {
                Id = default(int),
                Name = default(string),
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTable_WithSchemaStringAndAnonymousTypeNull_ThrowsException()
        {
            // Arrange
            var dataModel = new DataModel();
            string tableName = "Foo";

            // Act
            var tableBuilder = dataModel.AddTable<AspNetRoles>(schemaName: _schema, name: tableName, definition: null);
        }

        [TestMethod]
        public void AddTable_WithSchemaStringAndAnonymousType_ReturnsBuilder()
        {
            // Arrange
            var dataModel = new DataModel();
            string tableName = "Foo";

            // Act
            var tableBuilder = dataModel.AddTable(_schema, tableName, () => new
            {
                Id = default(int),
                Name = default(string),
            });

            // Assert
            IColumnBuilder[] columns = tableBuilder.GetColumns();
            Assert.IsNotNull(tableBuilder);
            Assert.AreEqual(tableName, tableBuilder.TableName);
            Assert.AreEqual(_schema, tableBuilder.Schema);
            Assert.AreEqual("Id", columns[0].ColumnName);
            Assert.AreEqual(typeof(int), columns[0].ColumnDataType);
            Assert.AreEqual("Name", columns[1].ColumnName);
            Assert.AreEqual(typeof(string), columns[1].ColumnDataType);
        }

        [TestMethod]
        public void GetTables_ReturnsTables()
        {
            // Arrange
            string table1Name = "Foo";
            string table2Name = "Bar";
            var dataModel = new DataModel();
            dataModel.AddTable(_schema, table1Name);
            dataModel.AddTable(_schema, table2Name);

            // Act
            ITableBuilder[] builders = dataModel.GetTables();

            // Assert
            Assert.AreEqual(2, builders.Length);
            Assert.AreEqual(table1Name, builders[0].TableName);
            Assert.AreEqual(table2Name, builders[1].TableName);
        }
    }
}
