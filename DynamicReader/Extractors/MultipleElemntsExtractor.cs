using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace DynamicReader.Extractors
{
    public class MultipleElemntsExtractor : IElementsExtractor
    {
        public bool TryExtractElements(GetMemberBinder binder, out object result, IEnumerable<XElement> dynamicObject)
        {
            result = TryGetElements(dynamicObject.Elements(binder.Name));
            if (result != null) return true;
            return false;
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
    }
}