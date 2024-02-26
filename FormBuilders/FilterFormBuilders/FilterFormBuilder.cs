using ECBuilder.ComponentBuilders;
using ECBuilder.Components.ComboBoxes;
using ECBuilder.Components.TextBoxes;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders.FilterFormBuilders
{
    /// <summary>
    /// It performs operations related to an Entity.
    /// </summary>
    [Serializable]
    public class FilterFormBuilder : FormBuilder
    {
        public FilterFormBuilder()
        {
            this.BeforeLoadEvent += FilterFormBuilder_BeforeLoadEvent;
            this.LoadEvent += FilterFormBuilder_LoadEvent;
        }

        #region Properties
        /// <summary>
        /// Owner ComponentBuilder
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComponentBuilder ComponentBuilder { get; set; }

        /// <summary>
        /// Controls to be used in the form and included in the process.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Control> UsingControls { get; set; } = new List<Control>();
        #endregion

        private Task FilterFormBuilder_BeforeLoadEvent()
        {
            return Task.Run(() =>
            {
                GetControls(this);
                UsingControls.Sort((a, b) => a.TabIndex.CompareTo(b.TabIndex));

                foreach (IComponentEntityType componentEntityType in UsingControls.OfType<IComponentEntityType>())
                {
                    if (!ImportListDefinitions.Contains(componentEntityType.EntityType) && !ImportLists.ContainsKey(componentEntityType.EntityType))
                    {
                        ImportListDefinitions.Add(componentEntityType.EntityType);
                    }
                    else
                    {
                        BuilderDebug.Warn($"{componentEntityType.EntityType.Name} already contains in {this.Name}.ImportListDefinitions or {this.Name}.ImportLists");
                    }
                }
            });
        }

        private async Task FilterFormBuilder_LoadEvent()
        {
            await this.InitializeControls();
        }

        #region Methods
        /// <summary>
        /// Initialize controls.
        /// </summary>
        /// <returns>awaitable Task</returns>
        public async Task InitializeControls()
        {
            foreach (Control control in UsingControls)
            {
                if (control is ComponentBuilderTextBox componentBuilderTextBox)
                {
                    await componentBuilderTextBox.Import();
                }
                else if (control is CustomComboBox customComboBox)
                {
                    await customComboBox.Import();
                }
                else if (control is IComponentBuilder componentBuilder)
                {
                    await componentBuilder.Initialize(this.ImportLists[componentBuilder.EntityType]);
                }
            }
        }
        #endregion

        #region Private Methods
        private void GetControls(Control control)
        {
            foreach (Control forControl in control.Controls)
            {
                if ((forControl.Tag != null) && forControl.Tag.ToString().Contains("use"))
                {
                    UsingControls.Add(forControl);
                }

                if (forControl.HasChildren)
                {
                    GetControls(forControl);
                }
            }
        }
        #endregion
    }
}
