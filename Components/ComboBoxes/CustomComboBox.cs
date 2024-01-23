using ECBuilder.FormBuilders.EntityFormBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using ECBuilder.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public EntityFormBuilder FormBuilder { get; set; }
        #endregion

        #region Methods
        public Task Import(object selectedValue = null)
        {
            if (!FormBuilder.ImportLists.ContainsKey(EntityType))
            {
                BuilderDebug.Error($"There were no lists of type {EntityType.Name} in ImportLists.");
                return null;
            }

            EntityList = FormBuilder.ImportLists[EntityType];
            this.Items.Clear();

            if (string.IsNullOrEmpty(DisplayMember)) DisplayMember = $"{EntityType.Name}Name";
            if (string.IsNullOrEmpty(ValueMember)) ValueMember = $"{EntityType.Name}ID";

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

            EntityList.ForEach(entity => this.Items.Add(entity));

            if (selectedValue != null)
            {
                int index = EntityList.FindIndex(entity => entity.GetType().GetProperty(ValueMember).GetValue(entity).Equals(selectedValue));
                this.SelectedIndex = index;
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
