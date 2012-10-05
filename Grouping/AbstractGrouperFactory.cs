using System;
using System.Collections.Generic;

namespace Grouping
{
    /// <summary>
    /// Base class providing information that any concrete implementation is likely to need.
    /// </summary>
    public abstract class AbstractGrouperFactory<TItem, TGroup, TResult> : IItemGrouperFactory<TItem, TGroup, TResult>
    {
        protected TItem Ungrouped;
        protected Func<TItem, TGroup> GetGroup;
        protected Func<TItem, TResult> GetResult;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="ungrouped">The group under results that were not in a group in the input will be placed in the output.</param>
        /// <param name="getGroup">Function for turning an input item into a group. It should return the default of <see cref="TGroup"/> for input items that are not groups.</param>
        /// <param name="getResult">Function for turning an input item into a result. It should return the default of <see cref="TResult"/> for input items that are not results.</param>
        protected AbstractGrouperFactory(TItem ungrouped, Func<TItem, TGroup> getGroup, Func<TItem, TResult> getResult)
        {
            Ungrouped = ungrouped;
            GetGroup = getGroup;
            GetResult = getResult;
        }

        public abstract IItemGrouper<TGroup, TResult> Create(IEnumerable<TItem> items);
    }
}