using ECBuilder.ComponentBuilders;
using ECBuilder.FormBuilders.ComponentBuilderFormBuilders;
using ECBuilder.Test;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.Components.TextBoxes
{
    public class ComponentBuilderTextBox : TextBox
    {
        public ComponentBuilderTextBox()
        {
            #region Designer
            this.ReadOnly = true;
            this.Margin = new Padding(0, 0, 0, 0);
            this.Size = new System.Drawing.Size(300, 29);
            this.Text = DefaultText;
            #endregion
        }

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string DefaultText { get; set; } = "Seç...";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<bool> ControlClickEvent { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task> BeforeLoadingFormEvent { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<ComponentBuilderFormBuilder, Task> LoadingFormEvent { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<DialogResult, Task> ClosedFormEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type ComponentBuilderType { get; set; }

        public string DisplayProperty { get; set; }

        public string ValueProperty { get; set; }

        public object Value { get; set; }

        public ComponentBuilderFormBuilder ComponentBuilderFormBuilder { get; set; }
        #endregion

        #region Events
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }

        protected override async void OnClick(EventArgs e)
        {
            #region Controls
            if (ECBuilderSettings.ComponentBuilderTextBoxForm == null)
            {
                BuilderDebug.Error("ECBuilderSettings.ComponentBuilderTextBoxForm was null.");
                return;
            }

            if (!ECBuilderSettings.ComponentBuilderTextBoxForm.IsSubclassOf(typeof(ComponentBuilderFormBuilder)))
            {
                BuilderDebug.Error("ECBuilderSettings.ComponentBuilderTextBoxForm was not ComponentBuilderFormBuilder.");
                return;
            }
            #endregion

            #region Control Click Event
            if (ControlClickEvent != null)
            {
                bool control = ControlClickEvent();
                if (!control) return;
            }
            #endregion

            #region BeforeLoadingFormEvent
            if (BeforeLoadingFormEvent != null)
            {
                await BeforeLoadingFormEvent();
            }
            #endregion

            #region Load Event
            ComponentBuilderFormBuilder = (ComponentBuilderFormBuilder)Activator.CreateInstance(ECBuilderSettings.ComponentBuilderTextBoxForm);
            this.ComponentBuilderFormBuilder.ComponentBuilderType = ComponentBuilderType;

            #region LoadingFormEvent
            if (LoadingFormEvent != null)
            {
                await LoadingFormEvent(ComponentBuilderFormBuilder);
            }
            #endregion

            DialogResult dialogResult = this.ComponentBuilderFormBuilder.ShowDialog(this.FindForm());
            if (dialogResult == DialogResult.OK)
            {
                this.Text = string.IsNullOrEmpty(DisplayProperty)
                    ? ComponentBuilderFormBuilder.SelectedEntity.GetType().GetProperty($"{ComponentBuilderFormBuilder.SelectedEntity.GetType().Name}Name").GetValue(ComponentBuilderFormBuilder.SelectedEntity).ToString()
                    : ComponentBuilderFormBuilder.SelectedEntity.GetType().GetProperty(DisplayProperty).GetValue(ComponentBuilderFormBuilder.SelectedEntity).ToString();

                this.Value = string.IsNullOrEmpty(ValueProperty)
                    ? ComponentBuilderFormBuilder.SelectedEntity.GetType().GetProperty($"{ComponentBuilderFormBuilder.SelectedEntity.GetType().Name}ID").GetValue(ComponentBuilderFormBuilder.SelectedEntity)
                    : ComponentBuilderFormBuilder.SelectedEntity.GetType().GetProperty(ValueProperty).GetValue(ComponentBuilderFormBuilder.SelectedEntity);
            }
            #endregion

            #region ClosedFormEvent
            if (ClosedFormEvent != null)
            {
                await ClosedFormEvent(dialogResult);
            }
            #endregion
        }
        #endregion
    }
}
