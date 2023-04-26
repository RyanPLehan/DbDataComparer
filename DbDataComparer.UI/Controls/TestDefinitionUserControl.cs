using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbDataComparer.Domain.Models;
using DbDataComparer.UI.Models;

namespace DbDataComparer.UI
{
    public class TestDefinitionUserControl : UserControl
    {
        public event EventHandler<TestDefinitionLoadRequestedEventArgs> TestDefinitionLoadRequested;
        public event EventHandler<TestDefinitionQueryRequestedEventArgs> TestDefinitionQueryRequested;
        public event EventHandler<TestDefinitionSaveRequestedEventArgs> TestDefinitionSaveRequested;
        public event EventHandler<TestDefinitionSetRequestedEventArgs> TestDefinitionSetRequested;
        public event EventHandler<TestDefinitionStatusUpdatedEventArgs> TestDefinitionStatusUpdated;

        protected string PathName { get; set; }
        protected TestDefinition TestDefinition { get; set; }


        #region General routines
        protected virtual void QueryTestDefinition()
        {
            var eventArgs = new TestDefinitionQueryRequestedEventArgs();
            OnTestDefinitionQueryRequested(eventArgs);

            this.PathName = eventArgs.PathName;
            this.TestDefinition = eventArgs.TestDefinition;
        }
        #endregion

        #region Event Raising Methods
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
