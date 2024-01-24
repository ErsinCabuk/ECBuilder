using ECBuilder.ComponentBuilders;
using ECBuilder.FormBuilders;
using ECBuilder.FormBuilders.ComponentBuilderFormBuilders;
using ECBuilder.FormBuilders.EntityFormBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ComponentBuilderFormBuilder ComponentBuilderFormBuilder { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type EntityType { get; set; }
        #endregion

        #region Events
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

        public Task Import()
        {
            return Task.Run(() => {
                ComponentBuilderFormBuilder = (ComponentBuilderFormBuilder)Activator.CreateInstance(ECBuilderSettings.ComponentBuilderTextBoxForm);
                ComponentBuilderFormBuilder.ComponentBuilderType = ComponentBuilderType;
            });
        }

        public void SetSelectedEntity(object entityID)
        {
            if(((FormBuilder)this.FindForm()).ImportLists.TryGetValue(EntityType, out List<IEntity> list))
            {
                IEntity entity = list.Find(findEntity => findEntity.GetType().GetProperty(
                string.IsNullOrEmpty(this.ValueProperty)
                    ? $"{findEntity.GetType().Name}ID"
                    : this.ValueProperty
                ).GetValue(findEntity).Equals(entityID));

                this.Text = entity.GetType().GetProperty(
                    string.IsNullOrEmpty(this.DisplayProperty)
                        ? $"{entity.GetType().Name}Name"
                        : this.DisplayProperty
                ).GetValue(entity).ToString();

                this.Value = entity.GetType().GetProperty(
                     string.IsNullOrEmpty(this.ValueProperty)
                        ? $"{entity.GetType().Name}ID"
                        : this.ValueProperty
                ).GetValue(entity).ToString();
            }
            else
            {
                BuilderDebug.Error("List of type ComponentBuildTextBox.EntityType was not found in ImportLists");
            }
        }
        #endregion
    }
}
