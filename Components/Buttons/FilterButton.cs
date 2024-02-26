using ECBuilder.FormBuilders.FilterFormBuilders;
using ECBuilder.Test;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.Components.Buttons
{
    public class FilterButton : Button
    {
        #region Properties
        /// <summary>
        /// Form containing the button.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FilterFormBuilder ParentForm { get; set; }

        /// <summary>
        /// Method that will work before the button does its main method. If the method returns <see langword="true"/>, the button starts main method. If it returns <see langword="false"/>, not starts main method.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<bool> ControlClickEvent;

        /// <summary>
        /// Method to be run before the main method of the button runs.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<Task> BeforeRunClickEvent;

        /// <summary>
        /// Method to be run after the main method of the button runs.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<Task> AfterRunClickEvent;
        #endregion

        #region Events
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (!this.FindForm().GetType().IsSubclassOf(typeof(FilterFormBuilder)) && !this.FindForm().GetType().IsEquivalentTo(typeof(FilterFormBuilder)))
            {
                BuilderDebug.Error(this.DesignMode, $"{this.Name} FilterButton can only be used in FilterFormBuilder.");
                return;
            }

            ParentForm = (FilterFormBuilder)this.FindForm();
        }

        protected override async void OnClick(EventArgs e)
        {
            #region Controls
            if (ParentForm == null)
            {
                BuilderDebug.Error($"{this.Name} FilterButton can only be used in FilterFormBuilder.");
                return;
            }
            #endregion

            #region ControlClickEvent
            if (ControlClickEvent != null)
            {
                bool controlResult = ControlClickEvent();
                if (!controlResult) return;
            }
            #endregion

            #region BeforeRunClickEvent
            if (BeforeRunClickEvent != null)
            {
                await BeforeRunClickEvent();
            }
            #endregion



            #region AfterRunClickEvent
            if (AfterRunClickEvent != null)
            {
                await AfterRunClickEvent();
            }
            #endregion
        }
        #endregion
    }
}