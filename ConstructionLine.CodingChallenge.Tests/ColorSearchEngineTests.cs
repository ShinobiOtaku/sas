using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class ColorSearchEngineTests
    {
        [Test]
        public void SimpleCase()
        {
            var redShirt = new Shirt(Guid.NewGuid(), "Red-Small", Size.Small, Color.Red);
            var shirts = new List<Shirt>
            {
                redShirt,
                new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue-Large", Size.Large,  Color.Blue),
            };

            var searchEngine = new ColorSearchEngine(shirts);

            var results = searchEngine.Search(new[] { Color.Red });

            Assert.AreEqual(1, results.Shirts.Count());
            Assert.AreEqual(redShirt, results.Shirts.Single());

            Assert.AreEqual(Color.All.Count(), results.ColorCounts.Count());
            Assert.AreEqual(results.ColorCounts.Single(x => x.Color == Color.Red).Count, 1);
            Assert.IsTrue(results.ColorCounts.Where(x => x.Color != Color.Red).All(x => x.Count == 0));
        }

        [Test]
        public void NoShirtsFound()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue-Large", Size.Large,  Color.Blue),
            };

            var searchEngine = new ColorSearchEngine(shirts);

            var results = searchEngine.Search(new[] { Color.Red });

            Assert.AreEqual(0, results.Shirts.Count());
            Assert.IsTrue(results.ColorCounts.All(x => x.Count == 0));
        }

        [Test]
        public void FindsMultipleShirtsForSameColor()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red-Small-1", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red-Small-2", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue-Large", Size.Large,  Color.Blue),
            };

            var searchEngine = new ColorSearchEngine(shirts);

            var results = searchEngine.Search(new[] { Color.Red });

            Assert.AreEqual(2, results.Shirts.Count());
            Assert.AreEqual(results.ColorCounts.Single(x => x.Color == Color.Red).Count, 2);
        }

        [Test]
        public void FindsMultipleShirtsForMultipleColors()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red-Small-1", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red-Small-2", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black-Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue-Large", Size.Large,  Color.Blue),
            };

            var searchEngine = new ColorSearchEngine(shirts);

            var results = searchEngine.Search(new[] { Color.Red, Color.Black });

            Assert.AreEqual(3, results.Shirts.Count());

            // The readme doesn't specify, but the existing test suggests that this relates to the number in the inventory rather than the number in the results
            Assert.AreEqual(results.ColorCounts.Single(x => x.Color == Color.Red).Count, 2);
            Assert.AreEqual(results.ColorCounts.Single(x => x.Color == Color.Black).Count, 1);
        }
    }
}
