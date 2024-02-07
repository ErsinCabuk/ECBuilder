using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ECBuilder.ComponentBuilders.DataGridViewBuilders.Columns
{
    /// <summary>
    /// Column type for <see cref="DataGridViewBuilder">DataGridViewBuilder</see> that can get cell content from another <see cref="IEntity">Entity</see>.
    /// </summary>
    public class DataGridViewCustomTextBoxColumn : DataGridViewTextBoxColumn, IComponentEntityType
    {
        public DataGridViewCustomTextBoxColumn()
        {

        }

        #region Properties
        /// <summary>
        /// Entity list type to be get from <see cref="DataGridViewBuilder.ImportLists">DataGridViewBuilder.ImportLists</see>.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Property name of the Entity to be found from Entity list.
        /// </summary>
        public string FindProperty { get; set; }

        /// <summary>
        /// Property name for the cell content of the found Entity.
        /// </summary>
        public string ValueProperty { get; set; }

        /// <summary>
        /// The text that will be displayed if the Entity is not found.
        /// </summary>
        public string NotFoundText { get; set; } = "";
        #endregion

        /// <summary>
        /// Returns the contents of the cell.
        /// </summary>
        /// <param name="value">Value get from Entity based on column name.</param>
        /// <param name="lists"><see cref="DataGridViewBuilder.ImportLists">DataGridViewBuilder.ImportLists</see></param>
        /// <returns>Last content of cell</returns>
        public object Use(object value, List<IEntity> list)
        {
            #region Controls
            if (EntityType == null)
            {
                BuilderDebug.Error("EntityType was null.");
                return null;
            }

            if (string.IsNullOrEmpty(FindProperty))
            {
                BuilderDebug.Error("FindProperty was null.");
                return null;
            }

            if (string.IsNullOrEmpty(ValueProperty))
            {
                BuilderDebug.Error("ValueProperty was null.");
                return null;
            }
            #endregion

            IEntity entity = list
                .Find(findEntity =>
                    findEntity
                        .GetType()
                        .GetProperty(this.FindProperty)
                        .GetValue(findEntity)
                        .Equals(value)
                    );

            if (entity != null)
            {
                return entity.GetType().GetProperty(this.ValueProperty).GetValue(entity);
            }
            else
            {
                return this.NotFoundText;
            }
        }
    }
}
