using NUnit.Framework;

namespace Grouping.Tests
{
    [TestFixture]
    public class PetersGrouperFactoryTests : GroupingTests
    {
        private const string UngroupedText = "Ungrouped";

        protected override IItemGrouperFactory<object, string, int> CreateFactory()
        {
            return new PetersGrouperFactory<object, string, int>(UngroupedText, o => o as string, o => o is int ? (int) o : default(int));
        }

        protected override string Ungrouped{get {return UngroupedText;}}
    }
}