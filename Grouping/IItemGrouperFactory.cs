using System.Collections.Generic;

namespace Grouping
{
    /// <summary>
    /// Create a grouper, for turning flat list into grouped list.
    /// </summary>
    /// <typeparam name="TItem">The type of items in the input flat list.</typeparam>
    /// <typeparam name="TGroup">The type of the groups in the output.</typeparam>
    /// <typeparam name="TResult">The type of the results, under each group, in the output.</typeparam>
    public interface IItemGrouperFactory<in TItem, TGroup, out TResult>
    {
        /// <summary>
        /// Create the grouper for the given flat list of items.
        /// </summary>
        IItemGrouper<TGroup, TResult> Create(IEnumerable<TItem> items);
    }
}