using ECBuilder.DataAccess;
using ECBuilder.Interfaces;
using ECBuilder.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.Components.ComboBoxes
{
    /// <summary>
    /// Imports to ComboBox according to <see cref="EntityType"/>.
    /// </summary>
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

        /// <summary>
        /// Entities in <see cref="EntityList"/> are sorted according to DisplayMember.
        /// </summary>
        public SortTypes SortType { get; set; } = SortTypes.Ascending;
        #endregion

        #region Methods
        /// <summary>
        /// Imports ComboBox's entities.
        /// </summary>
        /// <param name="selectedValue">ComboBox sets the selected value.</param>
        /// <returns>awaitable Task</returns>
        public async Task Import(object selectedValue = null)
        {
            #region Import Confs
            this.Items.Clear();
            #endregion

            #region Controls
            if (string.IsNullOrEmpty(DisplayMember)) DisplayMember = $"{EntityType.Name}Name";
            if (string.IsNullOrEmpty(ValueMember)) ValueMember = $"{EntityType.Name}ID";
            #endregion

            #region Import
            EntityList = await API.GetAll(EntityType);
            #endregion

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

            #region Adding Items
            EntityList.ForEach(entity => this.Items.Add(entity));
            #endregion

            #region SelectedValue
            if (selectedValue != null)
            {
                int index = EntityList.FindIndex(entity => entity.GetType().GetProperty(ValueMember).GetValue(entity).Equals(selectedValue));
                this.SelectedIndex = index;
            }
            else
            {
                this.SelectedIndex = 0;
            }
            #endregion
        }
        #endregion
    }
}
