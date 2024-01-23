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

        public Func<bool> ControlEvent { get; set; }

        public Func<Task> BeforeLoadingFormEvent { get; set; }

        public Func<ComponentBuilderFormBuilder, Task> LoadingFormEvent { get; set; }

        public Func<DialogResult, Task> ClosedFormEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type ComponentBuilderType { get; set; }
        #endregion

        protected override async void OnClick(EventArgs e)
        {
            if (ECBuilderSettings.ComponentBuilderTextBoxForm == null)
            {
                BuilderDebug.Error("ECBuilderSettings.ComponentBuilderTextBoxForm was null.");
                return;
            }

            object formType = Activator.CreateInstance(ECBuilderSettings.ComponentBuilderTextBoxForm);
            if (!formType.GetType().IsSubclassOf(typeof(ComponentBuilderFormBuilder)))
            {
                BuilderDebug.Error("ECBuilderSettings.ComponentBuilderTextBoxForm was not ComponentBuilderFormBuilder.");
                return;
            }

            if (ControlEvent != null)
            {
                bool control = ControlEvent();
                if (!control) return;
            }

            if (BeforeLoadingFormEvent != null)
            {
                await BeforeLoadingFormEvent();
            }

            ComponentBuilderFormBuilder componentBuilderFormBuilder = (ComponentBuilderFormBuilder)formType;
            componentBuilderFormBuilder.ComponentBuilderType = ComponentBuilderType;

            if (LoadingFormEvent != null)
            {
                await LoadingFormEvent(componentBuilderFormBuilder);
            }

            DialogResult dialogResult = componentBuilderFormBuilder.ShowDialog();

            if (ClosedFormEvent != null)
            {
                await ClosedFormEvent(dialogResult);
            }
        }
    }
}
