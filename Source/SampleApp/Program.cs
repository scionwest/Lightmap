using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap;
using Lightmap.Modeling;

namespace SampleApp
{
    public class InitialDb : IMigration
    {
        public Task Apply()
        {
            throw new NotImplementedException();
        }

        public void Configure()
        {
            var database = new DatabaseModeler();
            ITableModeler userTable = database.Create().Table("User");
            userTable
                .WithColumn<int>("UserId")
                    .AsPrimaryKey()
                    .IsUnique()
                    .WithIndex()
                .WithColumn<string>("FirstName")
                    .NotNull()
                .WithColumn<string>("LastName")
                    .NotNull()
               .WithColumn<char>("MiddleInitial");

            var accountTable = database.Create().Table("Account");
            accountTable
                .WithColumn<int>("AcountId")
                    .AsPrimaryKey()
                    .IsUnique()
                    .WithIndex()
                .WithColumn<string>("AccountNumber")
                    .NotNull()
                .AsForeignKey(userTable, "User_Account_Id_FK");
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}
