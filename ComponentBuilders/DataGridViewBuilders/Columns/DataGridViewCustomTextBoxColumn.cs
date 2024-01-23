using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ECBuilder.ComponentBuilders.DataGridViewBuilders.Columns
{
    public class DataGridViewCustomTextBoxColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewCustomTextBoxColumn()
        {

        }

        #region Properties
        public Type ListType { get; set; }

        public string FindProperty { get; set; }

        public string ValueProperty { get; set; }

        public string NotFoundText { get; set; } = "";
        #endregion

        public object Use(object preValue, Dictionary<Type, List<IEntity>> lists)
        {
            if (ListType == null)
            {
                BuilderDebug.Error("ListType was null.");
                return null;
            }

            if (!lists.ContainsKey(ListType))
            {
                BuilderDebug.Error("ListType not found in DataGridView.ImportLists");
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

            IEntity entity = lists[ListType].Find(findEntity => findEntity.GetType().GetProperty(this.FindProperty).GetValue(findEntity).Equals(preValue));

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
