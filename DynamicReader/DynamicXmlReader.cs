using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using DynamicReader.Extractors;

namespace DynamicReader
{
    public class DynamicXmlReader : DynamicObject, IEnumerable<DynamicObject>
    {
        private readonly IEnumerable<XElement> _cache;
        private readonly IElementsExtractor _elementsExtractor;
        private readonly IAttributesExtractor _attributesExtractor;

        private DynamicXmlReader(bool returnSingles)
        {
            _elementsExtractor = returnSingles
                ? (IElementsExtractor) new SingleElemntExtractor()
                : new MultipleElemntsExtractor();
            _attributesExtractor = returnSingles
                ? (IAttributesExtractor) new SingleAttributesExtractor()
                : new MultipleAttributesExtractor();
        }

        public DynamicXmlReader(XElement cache, bool returnSingles) 
            : this(returnSingles)
        {
            _cache = new List<XElement> { cache };
        }

        public DynamicXmlReader(IEnumerable<XElement> cache, bool returnSingles)
            : this(returnSingles)
        {
            _cache = cache;
        }

        public DynamicXmlReader(string uri, bool returnSingles)
            : this(XElement.Load(uri), returnSingles) { }

        public DynamicXmlReader(Stream stream, bool returnSingles)
            : this(XElement.Load(stream), returnSingles) { }

        public DynamicXmlReader(XElement cache) 
            : this(cache, false) { }

        public DynamicXmlReader(IEnumerable<XElement> cache) 
            : this(cache, false) { }

        public DynamicXmlReader(string uri)
            : this(XElement.Load(uri), false) { }

        public DynamicXmlReader(Stream stream)
            : this(XElement.Load(stream), false) { }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var searchedValue = indexes[0];
            return base.TryGetIndex(binder, indexes, out result);
        }
        //TODO: Add keywords support!
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_elementsExtractor.TryExtractElements(binder, out result, _cache)) 
                return true;

            if (_attributesExtractor.TryExtractAttributes(binder, out result, _cache)) 
                return true;

            return base.TryGetMember(binder, out result);
        }

        public IEnumerator<DynamicObject> GetEnumerator()
        {
            return _cache.Select(GetObject).ToList().GetEnumerator();
        }

        private static DynamicObject GetObject(dynamic obj)
        {
            return obj;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}