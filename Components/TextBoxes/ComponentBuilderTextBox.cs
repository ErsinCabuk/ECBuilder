using ECBuilder.ComponentBuilders;
using ECBuilder.FormBuilders;
using ECBuilder.FormBuilders.ComponentBuilderFormBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.Components.TextBoxes
{
    public class ComponentBuilderTextBox : TextBox
    {
        public ComponentBuilderTextBox()
        {

        }

        #region Properties
        public string DefaultText { get; set; } = "Seç...";

        public string DisplayProperty { get; set; }

        public string ValueProperty { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type ComponentBuilderType { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComponentBuilder ComponentBuilder { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ComponentBuilderFormBuilder ComponentBuilderFormBuilder { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEntity SelectedEntity { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue { get; set; }
        #endregion

        #region Events
        protected override void OnClick(EventArgs e)
        {
            DialogResult dialogResult = ComponentBuilderFormBuilder.ShowDialog(this.FindForm());
            if (dialogResult == DialogResult.OK)
            {
                SetSelectedEntity(ComponentBuilderFormBuilder.SelectedEntity);
            };
        }
        #endregion

        #region Methods
        public Task Import()
        {
            return Task.Run(() =>
            {
                this.Text = DefaultText;

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

                if (!ComponentBuilderType.GetInterfaces().Contains(typeof(IComponentBuilder)))
                {
                    BuilderDebug.Error("ComponentBuilderTextBox.ComponentBuilderType was not IComponentBuilder.");
                    return;
                }
                #endregion


                ComponentBuilderFormBuilder = (ComponentBuilderFormBuilder)Activator.CreateInstance(ECBuilderSettings.ComponentBuilderTextBoxForm);
                ComponentBuilder = (IComponentBuilder)Activator.CreateInstance(ComponentBuilderType);

                ComponentBuilderFormBuilder.ComponentBuilder = ComponentBuilder;

                if (string.IsNullOrEmpty(DisplayProperty)) DisplayProperty = $"{ComponentBuilder.EntityType.Name}Name";
                if (string.IsNullOrEmpty(ValueProperty)) ValueProperty = $"{ComponentBuilder.EntityType.Name}ID";
            });
        }

        public void SetSelectedEntity(object value)
        {
            FormBuilder formBuilder = (FormBuilder)this.FindForm();

            #region Controls
            if (!formBuilder.ImportLists.ContainsKey(ComponentBuilder.EntityType))
            {
                BuilderDebug.Error("ComponentBuilderFormBuilder.ComponentBuilder.EntityType was not found in FormBuilder.ImportLists");
                return;
            }
            #endregion

            SelectedEntity = formBuilder.ImportLists[ComponentBuilder.EntityType].Find(findEntity => findEntity.GetType().GetProperty(ValueProperty).GetValue(findEntity).Equals(value));

            if (SelectedEntity == null)
            {

                SelectedEntity = ComponentBuilder.AddList[0];
            }

            this.Text = SelectedEntity.GetType().GetProperty(DisplayProperty).GetValue(SelectedEntity).ToString();
            this.SelectedValue = SelectedEntity.GetType().GetProperty(ValueProperty).GetValue(SelectedEntity);
        }

        public void SetSelectedEntity(IEntity entity)
        {
            SelectedEntity = entity;

            this.Text = SelectedEntity.GetType().GetProperty(DisplayProperty).GetValue(SelectedEntity).ToString();
            this.SelectedValue = SelectedEntity.GetType().GetProperty(ValueProperty).GetValue(SelectedEntity);
        }
        #endregion
    }
}
