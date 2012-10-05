using System.Collections.Generic;

namespace Grouping
{
    /// <summary>
    /// Interface for examining the result of turning a flat list into a grouped list.
    /// </summary>
    /// <typeparam name="TGroup">The type of the groups.</typeparam>
    /// <typeparam name="TResult">The type of the results, under each group.</typeparam>
    public interface IItemGrouper<TGroup, out TResult>
    {
        /// <summary>
        /// Gets the results under the given group.
        /// </summary>
        IEnumerable<TResult> GetContent(TGroup group);

        /// <summary>
        /// Enumerates the groups.
        /// </summary>
        IEnumerable<TGroup> Groups{get;}
    }
}