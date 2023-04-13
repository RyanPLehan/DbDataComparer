using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.Domain.Configuration
{
    public class LocationSettings
    {
        private const string DEFAULT_TD_PATH = @".\TestDefinitions";
        private const string DEFAULT_CR_PATH = @".\ComparisonResults";
        private const string DEFAULT_CR_ERROR_PATH = @".\ComparisonResults\Errors";

        public string TestDefinitionsPath { get; set; } = DEFAULT_TD_PATH;
        public string ComparisonResultsPath { get; set; } = DEFAULT_CR_PATH;
        public string ComparisonErrorsPath { get; set; } = DEFAULT_CR_ERROR_PATH;
    }
}
