using ECBuilder.Interfaces;
using ECBuilder.Types;
using System.Collections.Generic;
using System.Linq;

namespace ECBuilder.Helpers
{
    public class ListHelper
    {
        public static List<IEntity> Filter(List<IEntity> list, Dictionary<string, (FilterTypes, object)> filters)
        {
            Comparer<object> comparer = Comparer<object>.Default;
            /*
             * Less than zero        =>    x is less than y.
             * Zero	                 =>    x equals y
             * Greater than zero     =>    x is greater than y.
             */
            foreach (KeyValuePair<string, (FilterTypes, object)> filter in filters)
            {
                FilterTypes filterType = filter.Value.Item1;
                object value = filter.Value.Item2;

                if (filterType == FilterTypes.Equal)
                {
                    list = list.Where(entity => entity.GetType().GetProperty(filter.Key).GetValue(entity).Equals(value)).ToList();
                }
                else if (filterType == FilterTypes.GreaterThan)
                {
                    list = list.Where(entity =>
                    {
                        int result = comparer.Compare(value, entity.GetType().GetProperty(filter.Key).GetValue(entity));
                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }).ToList();
                }
                else if (filterType == FilterTypes.GreaterThanOrEqual)
                {
                    list = list.Where(entity =>
                    {
                        int result = comparer.Compare(value, entity.GetType().GetProperty(filter.Key).GetValue(entity));
                        if (result > 0 || result == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }).ToList();
                }
                else if (filterType == FilterTypes.LessThan)
                {
                    list = list.Where(entity =>
                    {
                        int result = comparer.Compare(value, entity.GetType().GetProperty(filter.Key).GetValue(entity));
                        if (result < 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }).ToList();
                }
                else if (filterType == FilterTypes.LessThanOrEqual)
                {
                    list = list.Where(entity =>
                    {
                        int result = comparer.Compare(value, entity.GetType().GetProperty(filter.Key).GetValue(entity));
                        if (result < 0 || result == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }).ToList();
                }
                else if (filterType == FilterTypes.EqualArray)
                {
                    if (value is List<object> valueList)
                    {
                        list = list.Where(entity =>
                            valueList.Any(itemValue =>
                                itemValue.Equals(
                                    entity.GetType().GetProperty(filter.Key).GetValue(entity)
                                )
                            )
                        ).ToList();
                    }
                }
                else if (filterType == FilterTypes.NotEqualArray)
                {
                    if (value is List<object> valueList)
                    {
                        list = list.Where(entity =>
                            !valueList.Any(itemValue =>
                                itemValue.Equals(
                                    entity.GetType().GetProperty(filter.Key).GetValue(entity)
                                )
                            )
                        ).ToList();
                    }
                }
            }

            return list;
        }
    }
}
