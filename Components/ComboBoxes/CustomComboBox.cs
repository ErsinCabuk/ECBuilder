using ECBuilder.FormBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using ECBuilder.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.Components.ComboBoxes
{
    public class CustomComboBox : ComboBox
    {
        public CustomComboBox()
        {

        }

        #region Properties
        /// <summary>
        /// ComboBox entity type
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type EntityType { get; set; }

        /// <summary>
        /// ComboBox Items List
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<IEntity> EntityList { get; set; } = new List<IEntity>();

        public SortTypes SortType { get; set; } = SortTypes.Ascending;

        public FormBuilder FormBuilder { get; set; }
        #endregion

        #region Methods
        public Task Import()
        {
            if (string.IsNullOrEmpty(DisplayMember))
            {
                DisplayMember = $"{EntityType.Name}Name";
                ValueMember = $"{EntityType.Name}ID";
            }

            if (!FormBuilder.ImportLists.ContainsKey(EntityType))
            {
                BuilderDebug.Error($"There were no lists of type {EntityType.Name} in ImportLists.");
                return null;
            }

           EntityList = FormBuilder.ImportLists[EntityType];
            this.Items.Clear();

            #region Sort
            if (SortType is SortTypes.Ascending)
            {
                EntityList.Sort((x, y) =>
                    string.Compare(
                        x.GetType().GetProperty(DisplayMember).GetValue(x).ToString(),
                        y.GetType().GetProperty(DisplayMember).GetValue(y).ToString(),
                        StringComparison.Ordinal
                    )
                );
            }
            else if (SortType is SortTypes.Descending)
            {
                EntityList.Sort((x, y) =>
                        string.Compare(
                            y.GetType().GetProperty(DisplayMember).GetValue(y).ToString(),
                            x.GetType().GetProperty(DisplayMember).GetValue(x).ToString(),
                            StringComparison.Ordinal
                        )
                    );
            }
            #endregion

            EntityList.ForEach(entity =>
            {
                this.Items.Add(entity);
            });

            if (this.SelectedValue != null)
            {
                this.SelectedValue = this.SelectedValue;
            }
            else
            {
                this.SelectedIndex = 0;
            }

            return Task.CompletedTask;
        }
        #endregion
    }
}
