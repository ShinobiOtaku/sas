using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SizeSearchEngine
    {
        private readonly ILookup<Guid, Shirt> _shirtsBySize;

        public SizeSearchEngine(IEnumerable<Shirt> shirts) => 
            _shirtsBySize = shirts.ToLookup(x => x.Size.Id, x => x);


        public SizeSearchResults Search(IEnumerable<Size> sizes)
        {
            var sizeMatched = sizes.SelectMany(s => _shirtsBySize[s.Id]);

            var sizeCounts = Size.All.Select(s => new SizeCount
            {
                Size = s,
                Count = sizeMatched.Count(x => x.Size.Id == s.Id)
            });

            return new SizeSearchResults
            {
                Shirts = sizeMatched.ToArray(),
                SizeCounts = sizeCounts.ToArray()
            };
        }
    }
}