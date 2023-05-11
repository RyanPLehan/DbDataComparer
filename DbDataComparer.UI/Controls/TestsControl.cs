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
    public class TestsControl : UserControl
    {
        protected enum AddUpdateActionEnum : int
        {
            Add,
            Update,
        }

        protected const int NOT_SELECTED_INDEX = -1;
        protected const int NEW_SELECTED_INDEX = 0;

        protected const int SourceTabPageIndex = 0;
        protected const int TargetTabPageIndex = 1;

        protected bool TestValueChanged = false;
        protected TestDefinition TestDefinition;


        #region General routines
        public virtual void LoadTestDefinition(TestDefinition testDefinition)
        {
            throw new NotImplementedException();
        }

        public virtual TestDefinition SaveTestDefinition()
        {
            throw new NotImplementedException();
        }



        protected virtual void SetAddUpdateButton(AddUpdateActionEnum addUpdateAction, Button button)
        {
            button.Tag = addUpdateAction;

            switch (addUpdateAction)
            {
                case AddUpdateActionEnum.Add:
                    button.Text = "Add";
                    break;

                case AddUpdateActionEnum.Update:
                    button.Text = "Update";
                    break;
            }
        }


        protected void FindInComboBox(ComboBox comboBox, string value)
        {
            int selectedIndex = NOT_SELECTED_INDEX;

            for (int i = 1; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString().Equals(value))
                {
                    selectedIndex = i;
                    break;
                }
            }

            comboBox.SelectedIndex = selectedIndex;
        }


        protected void LoadComboBox<T>(ComboBox comboBox, IEnumerable<T> tests)
            where T: Test
        {
            comboBox.Items.Clear();

            comboBox.Items.Add("<< New Test >>");
            foreach (T test in tests.OrderBy(x => x.Name))
                comboBox.Items.Add(test);

            // Set index
            comboBox.SelectedIndex = NOT_SELECTED_INDEX;
        }

        #endregion
    }
}
