using System.Threading.Tasks;

namespace Lightmap.Migration
{
    public interface IDatabaseMigrator
    {
        IMigration[] Migrations { get; }

        bool IsMigrationNeeded();

        void Apply(IDatabaseManager databaseManager);

        Task ApplyAsync(IDatabaseManager databaseManager);
    }
}
