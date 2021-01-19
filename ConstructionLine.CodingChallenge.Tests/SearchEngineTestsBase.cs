using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchEngineTestsBase
    {
        protected static void AssertResults(IEnumerable<Shirt> inventory, SearchOptions options, IEnumerable<Shirt> resultingShirts)
        {
            Assert.That(resultingShirts, Is.Not.Null);

            var resultingShirtIds = resultingShirts.Select(s => s.Id).ToList();
            var selectedSizeIds = options.Sizes.Select(s => s.Id).ToList();
            var selectedColorIds = options.Colors.Select(c => c.Id).ToList();

            //Check everything in the inventory that matches the search options is in the results
            foreach (var shirt in inventory)
            {
                if (selectedSizeIds.Contains(shirt.Size.Id)
                    && selectedColorIds.Contains(shirt.Color.Id)
                    && !resultingShirtIds.Contains(shirt.Id))
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' not found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }

            //Check everything in the results matches the search options
            foreach (var shirt in resultingShirts)
            {
                if (!selectedSizeIds.Contains(shirt.Size.Id)
                    && !selectedColorIds.Contains(shirt.Color.Id))
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }
        }


        protected static void AssertSizeCounts(IEnumerable<Shirt> shirts, SearchOptions searchOptions, IEnumerable<SizeCount> sizeCounts)
        {
            Assert.That(sizeCounts, Is.Not.Null);

            foreach (var size in Size.All)
            {
                var sizeCount = sizeCounts.SingleOrDefault(s => s.Size.Id == size.Id);
                Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size.Name}' not found in results");

                var expectedSizeCount = shirts
                    .Count(s => s.Size.Id == size.Id
                                && (!searchOptions.Sizes.Any() || searchOptions.Sizes.Select(c => c.Id).Contains(s.Size.Id)));

                Assert.That(sizeCount.Count, Is.EqualTo(expectedSizeCount), 
                    $"Size count for '{sizeCount.Size.Name}' showing '{sizeCount.Count}' should be '{expectedSizeCount}'");
            }
        }


        protected static void AssertColorCounts(IEnumerable<Shirt> shirts, SearchOptions searchOptions, IEnumerable<ColorCount> colorCounts)
        {
            Assert.That(colorCounts, Is.Not.Null);
            
            foreach (var color in Color.All)
            {
                var colorCount = colorCounts.SingleOrDefault(s => s.Color.Id == color.Id);
                Assert.That(colorCount, Is.Not.Null, $"Color count for '{color.Name}' not found in results");

                var expectedColorCount = shirts
                    .Count(c => c.Color.Id == color.Id  
                                && (!searchOptions.Colors.Any() || searchOptions.Colors.Select(s => s.Id).Contains(c.Color.Id)));

                Assert.That(colorCount.Count, Is.EqualTo(expectedColorCount),
                    $"Color count for '{colorCount.Color.Name}' showing '{colorCount.Count}' should be '{expectedColorCount}'");
            }
        }
    }
}