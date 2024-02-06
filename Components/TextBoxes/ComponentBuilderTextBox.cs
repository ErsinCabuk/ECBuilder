using ECBuilder.ComponentBuilders;
using ECBuilder.FormBuilders;
using ECBuilder.FormBuilders.ComponentBuilderFormBuilders;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using ECBuilder.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.Components.TextBoxes
{
    public class ComponentBuilderTextBox : TextBox, IComponentEntityType
    {
        public ComponentBuilderTextBox()
        {
            #region Designer
            this.ReadOnly = true;
            #endregion
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<bool> BeforeControlClickEvent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<IEntity, bool> AfterControlClickEvent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<Task> SelectedEntityChanged;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, (FilterTypes, object)> Filters { get; set; } = new Dictionary<string, (FilterTypes, object)>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type EntityType { get; set; }
        #endregion

        #region Events
        protected override async void OnClick(EventArgs e)
        {
            #region BeforeControlClickEvent
            if (BeforeControlClickEvent != null)
            {
                bool result = BeforeControlClickEvent();
                if (!result) return;
            }
            #endregion

            DialogResult dialogResult = ComponentBuilderFormBuilder.ShowDialog(this.FindForm());
            if (dialogResult == DialogResult.OK)
            {
                #region AfterControlClickEvent
                if (AfterControlClickEvent != null)
                {
                    bool result = AfterControlClickEvent(ComponentBuilderFormBuilder.SelectedEntity);
                    if (!result) return;
                }
                #endregion

                SetSelectedEntity(ComponentBuilderFormBuilder.SelectedEntity);
            };

            #region SelectedEntityChanged
            if (SelectedEntityChanged != null)
            {
                await SelectedEntityChanged();
            }
            #endregion
        }
        #endregion

        #region Methods
        public Task Import()
        {
            return Task.Run(() =>
            {
                this.Text = DefaultText;

                #region Controls
                if (ComponentBuilderType == null)
                {
                    BuilderDebug.Error("ComponentBuilderTextBox.ComponentBuilderType was null.");
                    return;
                }

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
                ComponentBuilder.Filters = this.Filters;

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
            if (entity != null)
            {
                SelectedEntity = entity;

                this.Text = SelectedEntity.GetType().GetProperty(DisplayProperty).GetValue(SelectedEntity).ToString();
                this.SelectedValue = SelectedEntity.GetType().GetProperty(ValueProperty).GetValue(SelectedEntity);
            }
        }

        public void ResetSelectedEntity()
        {
            SelectedEntity = null;
            SelectedValue = null;
            Text = DefaultText;
        }
        #endregion
    }
}
