using System.Linq;
using NUnit.Framework;

namespace Grouping.Tests
{
    public abstract class GroupingTests
    {
        private IItemGrouperFactory<object, string, int> factory;

        protected abstract IItemGrouperFactory<object, string, int> CreateFactory();
        protected abstract string Ungrouped{get;}

        [SetUp]
        public void SetUp()
        {
            factory = CreateFactory();
        }

        [Test]
        public void TestGroupEmptyReturnsEmpty()
        {
            var grouper = factory.Create(new object[0]);
            Assert.That(grouper.Groups, Is.Empty);
        }

        [Test]
        public void TestGroupSingleGroupItemReturnsEmpty()
        {
            var grouper = factory.Create(new object[] {"heading"});
            Assert.That(grouper.Groups, Is.Empty);
        }

        [Test]
        public void TestGroupSingleGroupItemSingleResultReturnsSingleGroupContainingResult()
        {
            var grouper = factory.Create(new object[] {"heading", 1});
            var groups = grouper.Groups.ToList();
            Assert.That(groups.Single(), Is.EqualTo("heading"));
            Assert.That(grouper.GetContent("heading").Single(), Is.EqualTo(1));
        }

        [Test]
        public void TestGroupTwoGroupItemsTwoResultsEachReturnsGroupedResults()
        {
            var grouper = factory.Create(new object[] {"heading1", 1, 2, "heading2", 3, 4, });
            var groups = grouper.Groups.ToList();
            Assert.That(groups.First(), Is.EqualTo("heading1"));
            Assert.That(groups.Last(), Is.EqualTo("heading2"));
            Assert.That(grouper.GetContent("heading1").First(), Is.EqualTo(1));
            Assert.That(grouper.GetContent("heading1").Last(), Is.EqualTo(2));
            Assert.That(grouper.GetContent("heading2").First(), Is.EqualTo(3));
            Assert.That(grouper.GetContent("heading2").Last(), Is.EqualTo(4));
        }

        [Test]
        public void TestGroupNoGroupsReturnsResultsUnderUngrouped()
        {
            var grouper = factory.Create(new object[] { 1, 2, 3, 4, });
            var groups = grouper.Groups.ToList();
            Assert.That(groups.Count(), Is.EqualTo(1));
            Assert.That(grouper.GetContent(Ungrouped).Count(), Is.EqualTo(4));
        }
    }
}
