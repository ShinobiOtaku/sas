using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class ColorSearchEngine
    {
        private readonly ILookup<Guid, Shirt> _shirtsByColor;

        public ColorSearchEngine(IEnumerable<Shirt> shirts) => 
            _shirtsByColor = shirts.ToLookup(x => x.Color.Id, x => x);

        public ColorSearchResults Search(IEnumerable<Color> colors)
        {
            var colourMatched = colors.SelectMany(c => _shirtsByColor[c.Id]);

            var colorCounts = Color.All.Select(c => new ColorCount
            {
                Color = c,
                Count = colourMatched.Count(x => x.Color.Id == c.Id)
            });

            return new ColorSearchResults
            {
                Shirts = colourMatched,
                ColorCounts = colorCounts.ToArray()
            };
        }
    }
}