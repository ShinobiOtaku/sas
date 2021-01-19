using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly ColorSearchEngine _colorEngine;
        private readonly SizeSearchEngine _sizeEngine;

        public SearchEngine(IEnumerable<Shirt> shirts)
        {
            _colorEngine = new ColorSearchEngine(shirts);
            _sizeEngine = new SizeSearchEngine(shirts);
        }

        public SearchResults Search(SearchOptions options)
        {
            var colorResult = _colorEngine.Search(options.Colors);
            var sizeResult = _sizeEngine.Search(options.Sizes);

            return new SearchResults
            {
                Shirts = colorResult.Shirts.Intersect(sizeResult.Shirts).ToArray(),
                SizeCounts = sizeResult.SizeCounts,
                ColorCounts = colorResult.ColorCounts
            };
        }
    }
}