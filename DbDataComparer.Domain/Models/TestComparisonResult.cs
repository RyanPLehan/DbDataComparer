using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Enums;

namespace DbDataComparer.Domain.Models
{
    public class TestComparisonResult
    {
        public ComparisonResultTypeEnum Result { get; set; } = ComparisonResultTypeEnum.NotTested;
        public string ResultDescription { get; set; }
    }
}
