using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        static object[] TestCases =
        {
            new object[]
            {
                new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red-Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                    new Shirt(Guid.NewGuid(), "Blue-Large", Size.Large,  Color.Blue),
                },
                new SearchOptions(new[] { Size.Small }, new[] { Color.Red }) // Simple case
            },
            new object[]
            {
                new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red-Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Blue-Medium", Size.Medium, Color.Blue)
                },
                new SearchOptions(new[] { Size.Small }, new[] { Color.Red }) // From the readme
            },
            new object[]
            {
                new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red-Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                },
                new SearchOptions(new[] { Size.Small }, new[] { Color.Black }) // Ensure search options are intersected
            },
            new object[]
            {
                new List<Shirt>(),
                new SearchOptions(Size.All, Color.All) // No inventory
            },
            new object[]
            {
                new List<Shirt>
                {
                    new Shirt(Guid.NewGuid(), "Red-Small", Size.Small, Color.Red),
                    new Shirt(Guid.NewGuid(), "Blue-Medium", Size.Medium, Color.Blue)
                },
                new SearchOptions(new Size[0], new Color[0]) // Empty search options means all search options (although this isn't mentioned in the readme)
            }
        };

        [TestCaseSource(nameof(TestCases))]
        public void Test(IEnumerable<Shirt> inventory, SearchOptions options)
        {
            var searchEngine = new SearchEngine(inventory);

            var results = searchEngine.Search(options);

            AssertResults(inventory, options, results.Shirts);
            AssertSizeCounts(inventory, options, results.SizeCounts);
            AssertColorCounts(inventory, options, results.ColorCounts);
        }
    }
}
