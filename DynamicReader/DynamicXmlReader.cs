using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace DynamicReader
{
    public class DynamicXmlReader : DynamicObject, IEnumerable<DynamicObject>
    {
        private readonly bool _returnSingles;
        private readonly IEnumerable<XElement> _cache;
        private string _currentName;

        public DynamicXmlReader(XElement cache, bool returnSingles)
        {
            _returnSingles = returnSingles;
            _cache = new List<XElement> { cache };
        }

        public DynamicXmlReader(IEnumerable<XElement> cache, bool returnSingles)
        {
            _cache = cache;
            _returnSingles = returnSingles;
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
            _currentName = "";
            return base.TryGetIndex(binder, indexes, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            //TODO: refactor- IElementsExtractor
            if (_returnSingles)
                result = TryGetElementsOrValue(_cache.Elements(binder.Name));
            else result = TryGetElements(_cache.Elements(binder.Name));
            if (result != null) return true;

            //TODO: refactor- IAttributesExtractor
            if (_returnSingles)
                result = TryGetAttributesOrValue(_cache.Attributes(binder.Name));
            else result = TryGetAttributes(_cache.Attributes(binder.Name));
            if (result != null) return true;
            
            return base.TryGetMember(binder, out result);
        }

        private static object TryGetAttributesOrValue(IEnumerable<XAttribute> requestedAttributes)
        {
            if (requestedAttributes.HasSingleElement())
                return requestedAttributes.Single().Value;
            return TryGetAttributes(requestedAttributes);
        }

        private static object TryGetElements(IEnumerable<XElement> requestedElements)
        {
            if (requestedElements.Any())
            {
                if (requestedElements.First().ContainsValueOnly())
                    return requestedElements.Select(e => e.Value);
                return new DynamicXmlReader(requestedElements);
            }
            return null;
        }

        private static object TryGetAttributes(IEnumerable<XAttribute> requestedAttributes)
        {
            if (requestedAttributes.Any())
                return requestedAttributes.Select(a => a.Value);
            return null;
        }

        private static object TryGetElementsOrValue(IEnumerable<XElement> requestedElements)
        {
            if (requestedElements.HasPluralElements())
            {
                if (requestedElements.First().ContainsValueOnly())
                    return requestedElements.Select(e => e.Value);
                return new DynamicXmlReader(requestedElements);
            }
            if (requestedElements.HasSingleElement())
            {
                var element = requestedElements.Single();
                if (element.ContainsValueOnly())
                    return element.Value;
                return requestedElements;
            }
            return null;
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