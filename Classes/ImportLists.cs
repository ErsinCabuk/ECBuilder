using ECBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECBuilder.Classes
{
    public class ImportLists : Dictionary<Type, List<IEntity>>
    {
        public List<T> Get<T>() where T : IEntity
        {
            if (!this.ContainsKey(typeof(T)))
            {
                throw new Exception("List was not found.");
            }

            return this[typeof(T)].Cast<T>().ToList();
        }
    }
}
