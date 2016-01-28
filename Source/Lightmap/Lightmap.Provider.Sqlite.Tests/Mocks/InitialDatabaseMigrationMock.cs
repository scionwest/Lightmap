using System;
using System.Threading.Tasks;

namespace Lightmap.Provider.Sqlite.Tests.Mocks
{
    [MigrationVersion(1)]
    public class InitialDatabaseMigrationMock : IMigration
    {
        private TaskCompletionSource<bool> completionTask;

        private Action onConfiguration;

        public InitialDatabaseMigrationMock(Action onConfiguration, TaskCompletionSource<bool> completionTask)
        {
            this.completionTask = completionTask;
            this.onConfiguration = onConfiguration;
        }

        public Task Apply()
        {
            this.completionTask.SetResult(true);
            return this.completionTask.Task;
        }

        public void Configure()
        {
            this.onConfiguration();
        }

        public Task Rollback()
        {
            this.completionTask.SetResult(true);
            return this.completionTask.Task;
        }
    }
}