﻿using ECBuilder.Classes;
using ECBuilder.DataAccess;
using ECBuilder.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public event Func<Task> BeforeLoadEvent;

        /// <summary>
        /// Event to run after the main load event. 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Func<Task> AfterLoadEvent;

        /// <summary>
        /// Imported lists. The key gives the type of the list and the value gives the list.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ImportLists ImportLists { get; set; } = new ImportLists();

        /// <summary>
        ///
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Type> ImportListDefinitions { get; set; } = new List<Type>();

        #endregion

        #region Private Properties
        internal event Func<Task> LoadEvent;
        #endregion

        #region Events
        protected override async void OnLoad(EventArgs e)
        {
            #region BeforeLoadEvent
            if (BeforeLoadEvent != null)
            {
                await BeforeLoadEvent();
            }
            #endregion

            #region Import Lists
            if (ImportListDefinitions != null && ImportListDefinitions.Count > 0)
            {
                foreach (Type type in ImportListDefinitions)
                {
                    if (ImportLists.ContainsKey(type))
                    {
                        BuilderDebug.Warn($"{type.Name} already contains in FormBuilder.ImportLists");
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
