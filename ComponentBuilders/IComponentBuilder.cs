using ECBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECBuilder.ComponentBuilders
{
    public interface IComponentBuilder
    {
        /// <summary>
        /// Form that will open when <see cref="IEntity">Entity</see> information is displayed or edited in the Component. Must be of type <see cref="EntityFormBuilder">FormBuilder</see>.
        /// </summary>
        Type InfoForm { get; set; }

        /// <summary>
        /// Form that will be opened when creating <see cref="IEntity">Entity</see> in the component. Must be of type <see cref="EntityFormBuilder">FormBuilder</see>.
        /// </summary>
        Type CreateForm { get; set; }

        /// <summary>
        /// Event to be run when <see cref="InfoForm">InfoForm</see> is closed.
        /// </summary>
        Func<DialogResult, Task> InfoFormCloseEvent { get; set; }

        /// <summary>
        /// Event to be run when <see cref="CreateForm">CreateForm</see> is closed.
        /// </summary>
        Func<DialogResult, Task> CreateFormCloseEvent { get; set; }

        /// <summary>
        /// Entity type to be import in Component. Must be of type <see cref="IEntity">IEntity</see>
        /// </summary>
        Type EntityType { get; set; }

        /// <summary>
        /// Entities to be imported to Component.
        /// </summary>
        List<IEntity> EntityList { get; set; }

        /// <summary>
        /// Other Entity types to be used in the component. Must be of type <see cref="IEntity">IEntity</see>
        /// </summary>
        List<Type> ImportListDefinition { get; set; }

        /// <summary>
        /// List of other Entity types to be used in the component when the <see cref="Import">Import</see> method runs.
        /// </summary>
        Dictionary<Type, List<IEntity>> ImportLists { get; set; }

        /// <summary>
        /// Method that imports the types given in the <see cref="ImportListDefinition">ImportListDefinition</see> and <see cref="EntityType">EntityType</see> parameters and adds them to the Component.
        /// </summary>
        /// <returns>awaitable Task</returns>
        Task Import();

        /// <summary>
        /// Method showing <see cref="CreateForm">CreateForm</see>.
        /// </summary>
        void ShowCreateForm();
    }
}
