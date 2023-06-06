﻿using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Models;
using DbDataComparer.Logger.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbDataComparer.Logger
{
    public class Logger
    {
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

            // Lookup existing or insert new test definition
            entity = this.Repository.GetTestDefinitionEntity(testDefinition) ?? await this.Repository.Insert(testDefinition);
            if (entity != null)
            {
                this.TestDefinitionEntities.Add(entity);
                foreach (ComparisonResult result in comparisonResults)
                    await this.Repository.Insert(entity.Id, LogDateTime, result);
            }
        }
    }
}
