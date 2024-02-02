using ECBuilder.Classes;
using ECBuilder.DataAccess;
using ECBuilder.Interfaces;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.FormBuilders
{
    /// <summary>
    /// It covers all transactions related to the form.
    /// </summary>
    public class FormBuilder : Form
    {
        #region Properties
        /// <summary>
        /// Event to run before the main load event. 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task> BeforeLoadEvent { get; set; }

        /// <summary>
        /// Event to run after the main load event. 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<Task> AfterLoadEvent { get; set; }

        /// <summary>
        /// Controls to be used in the form and included in the process.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Control> UsingControls { get; set; }

        /// <summary>
        /// Imported lists. The key gives the type of the list and the value gives the list.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ImportLists ImportLists { get; set; } = new ImportLists();

        /// <summary>
        ///
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Type> ImportListDefinitions { get; set; }

        #endregion

        #region Private Properties
        internal Func<Task> LoadEvent { get; set; }
        #endregion

        #region Events
        protected override async void OnLoad(EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            #region BeforeLoadEvent
            if (BeforeLoadEvent != null)
            {
                await BeforeLoadEvent();
            }
            #endregion

            UsingControls = this.Controls.Cast<Control>().Where(whereControl => (whereControl.Tag != null) && (whereControl.Tag.ToString().Split(',').Select(tag => tag.Trim()).Contains("use"))).ToList();
            UsingControls.Sort((a, b) => a.TabIndex.CompareTo(b.TabIndex));

            #region Import Lists
            foreach (Control control in UsingControls)
            {
                if(control is IComponentEntityType entityTypeControl && !ImportLists.ContainsKey(entityTypeControl.EntityType))
                {
                    ImportLists.Add(entityTypeControl.EntityType, await API.GetAll(entityTypeControl.EntityType, 1));
                }
            }

            if (ImportListDefinitions != null && ImportListDefinitions.Count > 0)
            {
                foreach (Type type in ImportListDefinitions)
                {
                    if (ImportLists.ContainsKey(type))
                    {
                        BuilderDebug.Warn("ImportLists contains");
                        continue;
                    }

                    ImportLists.Add(type, await API.GetAll(type, 1));
                }
            }
            #endregion
            
            #region Load Event
            if (LoadEvent != null)
            {
                await LoadEvent();
            }
            #endregion

            #region AfterLoadEvent
            if (AfterLoadEvent != null)
            {
                await AfterLoadEvent();
            }
            #endregion
        }
        #endregion
    }
}
