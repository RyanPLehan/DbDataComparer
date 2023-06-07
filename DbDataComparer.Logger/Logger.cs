using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Models;
using DbDataComparer.Logger.Models;

namespace DbDataComparer.Logger
{
    public class Logger : ICustomLogger
    {
        private bool IsRepoInitialized = false;
        private readonly DateTime LogDateTime;
        private readonly LogSettings Settings;
        private readonly Repository Repository;
        private IList<TestDefinitionEntity> TestDefinitionEntities;

        public Logger(LogSettings settings)
        {
            this.Settings = settings ??
                throw new ArgumentNullException(nameof(settings));

            this.Repository = new Repository(settings.Connection);
            this.LogDateTime = DateTime.Now;
            this.TestDefinitionEntities = new List<TestDefinitionEntity>();
        }

        public async Task Log(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            TestDefinitionEntity entity;

            if (!this.IsRepoInitialized)
                await Open();

            // Lookup existing or insert new test definition
            entity = this.Repository.GetTestDefinitionEntity(testDefinition) ?? await this.Repository.Insert(testDefinition);
            if (entity != null)
            {
                this.TestDefinitionEntities.Add(entity);
                foreach (ComparisonResult result in comparisonResults)
                    await this.Repository.Insert(entity.Id, LogDateTime, result);
            }
        }

        public async Task Open()
        {
            await this.Repository.Initialize();
            this.IsRepoInitialized = true;
        }


        public async Task Close()
        {
            await this.Repository.Finalize(this.TestDefinitionEntities);
        }
    }
}
