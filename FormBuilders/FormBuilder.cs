using ECBuilder.DataAccess;
using ECBuilder.Interfaces;
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
        public Dictionary<Type, List<IEntity>> ImportLists { get; set; } = new Dictionary<Type, List<IEntity>>();

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

            #region Import Lists
            if (ImportListDefinitions != null && ImportListDefinitions.Count > 0)
            {
                foreach (Type type in ImportListDefinitions)
                {
                    var x = await API.GetAll(type, 1);
                    ImportLists.Add(type, await API.GetAll(type, 1));
                }
            }
            #endregion

            #region BeforeLoadEvent
            if (BeforeLoadEvent != null)
            {
                await BeforeLoadEvent();
            }
            #endregion

            #region Load Event
            UsingControls = this.Controls.Cast<Control>().Where(whereControl => (whereControl.Tag != null) && (whereControl.Tag.ToString().Split(',').Select(tag => tag.Trim()).Contains("use"))).ToList();
            UsingControls.Sort((a, b) => a.TabIndex.CompareTo(b.TabIndex));

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
