using ECBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECBuilder.Classes
{
    /// <summary>
    /// Classes that can import EntityLists for
    /// <see cref="FormBuilders.FormBuilder">FormBuilder</see>,
    /// <see cref="ComponentBuilders.DataGridViewBuilders.DataGridViewBuilder">DataGridViewBuilder</see>
    /// and
    /// <see cref="ComponentBuilders.TreeViewBuilders.TreeViewBuilder">TreeViewBuilder</see>
    /// </summary>
    public class ImportLists : Dictionary<Type, List<IEntity>>
    {
        /// <summary>
        /// Gets <see cref="List{IEntity}"/> as <see cref="List{T}"/>.
        /// </summary>
        /// <typeparam name="T">List type</typeparam>
        /// <returns>Returns <see cref="List{T}"/></returns>
        /// <exception cref="ListNotFoundException"></exception>
        public List<T> Get<T>() where T : IEntity
        {
            if (!this.ContainsKey(typeof(T)))
            {
                throw new ListNotFoundException("List type was not found in lists.");
            }

            return this[typeof(T)].Cast<T>().ToList();
        }
    }

    /// <summary>
    /// List Not Found Exception in ImportLists
    /// </summary>
    public class ListNotFoundException : Exception
    {
        /// <summary>
        /// List Not Found Exception in ImportLists
        /// </summary>
        /// <param name="message">Exception message</param>
        public ListNotFoundException(string message) : base(message)
        {

        }
    }
}
