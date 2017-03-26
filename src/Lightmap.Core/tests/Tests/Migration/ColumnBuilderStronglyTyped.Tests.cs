using System;
using System.Linq;
using Lightmap.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lightmap.Modeling
{
    [TestClass]
    public class ColumnBuilderStronglyTypedTests
    {
        const string _schema = "dbo";

        [TestMethod]
        public void WithForeignKey_AddsDefinitonToTableBuilder()
        {
            // Arrange
            var dataModel = new DataModel();
            var table = dataModel.AddTable<AspNetUsers>();

            // Act
            IColumnBuilderStronglyTyped<AspNetUsers> column = table.AlterColumn(model => model.Id)
                .WithForeignKey<AspNetUserRoles>((users, roles) => roles.UserId == users.Id);

            // Assert
            Assert.AreEqual("Id", column.GetColumnDefinition()[ColumnDefinitions.ForeignKey]);
        }
    }
}
