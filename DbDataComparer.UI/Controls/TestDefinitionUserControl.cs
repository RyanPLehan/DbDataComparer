using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Models;
using DbDataComparer.UI.Models;

namespace DbDataComparer.UI
{
    public class TestDefinitionUserControl : UserControl
    {
        public event EventHandler<ConfigurationQueryRequestedEventArgs> ConfigurationQueryRequested;
        public event EventHandler<TestDefinitionLoadRequestedEventArgs> TestDefinitionLoadRequested;
        public event EventHandler<TestDefinitionQueryRequestedEventArgs> TestDefinitionQueryRequested;
        public event EventHandler<TestDefinitionSaveRequestedEventArgs> TestDefinitionSaveRequested;
        public event EventHandler<TestDefinitionSetRequestedEventArgs> TestDefinitionSetRequested;
        public event EventHandler<TestDefinitionStatusUpdatedEventArgs> TestDefinitionStatusUpdated;

        protected string PathName { get; set; }
        protected TestDefinition TestDefinition { get; set; }
        protected ConfigurationSettings ConfigurationSettings { get; set; }


        #region General routines
        public virtual void Activate()
        {
            this.QueryConfiguration();
            this.QueryTestDefinition();
        }

        protected virtual void QueryConfiguration()
        {
            var eventArgs = new ConfigurationQueryRequestedEventArgs();
            OnConfigurationQueryRequested(eventArgs);

            this.ConfigurationSettings = eventArgs.ConfigurationSettings;
        }

        protected virtual void QueryTestDefinition()
        {
            var eventArgs = new TestDefinitionQueryRequestedEventArgs();
            OnTestDefinitionQueryRequested(eventArgs);

            this.PathName = eventArgs.PathName;
            this.TestDefinition = eventArgs.TestDefinition;
        }

        protected virtual void SetTestDefinition(TestDefinition testDefinition)
        {
            var eventArgs = new TestDefinitionSetRequestedEventArgs() { TestDefinition = testDefinition };
            OnTestDefinitionSetRequested(eventArgs);
        }
        #endregion

        #region Event Raising Methods
        protected virtual void OnConfigurationQueryRequested(ConfigurationQueryRequestedEventArgs e)
        {
            EventHandler<ConfigurationQueryRequestedEventArgs> handler = ConfigurationQueryRequested;
            if (handler != null)
                handler(this, e);
        }


        protected virtual void OnTestDefinitionLoadRequested(TestDefinitionLoadRequestedEventArgs e)
        {
            EventHandler<TestDefinitionLoadRequestedEventArgs> handler = TestDefinitionLoadRequested;
            if (handler != null)
                handler(this, e);
        }


        protected virtual void OnTestDefinitionQueryRequested(TestDefinitionQueryRequestedEventArgs e)
        {
            EventHandler<TestDefinitionQueryRequestedEventArgs> handler = TestDefinitionQueryRequested;
            if (handler != null)
                handler(this, e);
        }


        protected virtual void OnTestDefinitionSaveRequested(TestDefinitionSaveRequestedEventArgs e)
        {
            EventHandler<TestDefinitionSaveRequestedEventArgs> handler = TestDefinitionSaveRequested;
            if (handler != null)
                handler(this, e);
        }


        protected virtual void OnTestDefinitionSetRequested(TestDefinitionSetRequestedEventArgs e)
        {
            EventHandler<TestDefinitionSetRequestedEventArgs> handler = TestDefinitionSetRequested;
            if (handler != null)
                handler(this, e);
        }


        protected virtual void OnTestDefinitionStatusUpdated(TestDefinitionStatusUpdatedEventArgs e)
        {
            EventHandler<TestDefinitionStatusUpdatedEventArgs> handler = TestDefinitionStatusUpdated;
            if (handler != null)
                handler(this, e);
        }
        #endregion
    }
}
