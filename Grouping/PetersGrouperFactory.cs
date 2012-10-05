using System;
using System.Collections.Generic;
using System.Linq;

namespace Grouping
{
    public class PetersGrouperFactory<TItem, TGroup, TResult> : AbstractGrouperFactory<TItem, TGroup, TResult>
    {
        public PetersGrouperFactory(TItem ungrouped, Func<TItem, TGroup> getGroup, Func<TItem, TResult> getResult) 
            : base(ungrouped, getGroup, getResult)
        {}

        public override IItemGrouper<TGroup, TResult> Create(IEnumerable<TItem> items)
        {
            var indexedItems = new[] {Ungrouped}.Concat(items).Select((o, i) => new {Item = o, Index = i}).ToList();

            var indexedGroups = indexedItems
                        .Select(ii => new { Group = GetGroup(ii.Item), ii.Index})
                        .Where(ii => ! EqualityComparer<TGroup>.Default.Equals(ii.Group, default(TGroup)))
                        .ToList();

            ILookup<TGroup, TResult> resultsByGroup = indexedItems
                        .Select(ii => new { Result = GetResult(ii.Item), ii.Index})
                        .Where(ii => ! EqualityComparer<TResult>.Default.Equals(ii.Result, default(TResult)))
                        .Select(ii => new {ii.Result, ii.Index, indexedGroups.Last(lbl => lbl.Index < ii.Index).Group})
                        .ToLookup(ii => ii.Group, ii => ii.Result);

            return new LookupGrouper(resultsByGroup);
        }

        private class LookupGrouper : IItemGrouper<TGroup, TResult>
        {
            private readonly ILookup<TGroup, TResult> resultsByGroup;
            public LookupGrouper(ILookup<TGroup, TResult> resultsByGroup) { this.resultsByGroup = resultsByGroup; }
            public IEnumerable<TResult> GetContent(TGroup group) { return resultsByGroup[group]; }
            public IEnumerable<TGroup> Groups{get {return resultsByGroup.Select(g => g.Key);}}
        }
    }
}