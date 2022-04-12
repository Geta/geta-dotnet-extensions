using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Xunit;

namespace Geta.Net.Extensions.Tests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void DistinctBy_removes_duplicate_entries()
        {
            var list = new List<string> { "1", "2", "1" };
            var distinctList = list.DistinctBy(x => x);

            Assert.Equal(1, distinctList.Count(x => x == "1"));
        }

        [Fact]
        public void Partition_splits_list_in_multiple_partitions()
        {
            var list = new Fixture().CreateMany<string>(12);

            var partitionedList = list.Partition(3);

            var expectedPartitionCount = 4;
            Assert.Equal(expectedPartitionCount, partitionedList.Count());
        }
    }
}
