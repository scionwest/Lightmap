using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public interface IDatabaseMigrator
    {
        IMigration[] Migrations { get; }

        string DefaultSchema { get; }

        bool IsMigrationNeeded();

        void Apply(IDatabaseManager databaseManager);

        Task ApplyAsync(IDatabaseManager databaseManager);
    }
}
