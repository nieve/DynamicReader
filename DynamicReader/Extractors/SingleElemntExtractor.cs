using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace DynamicReader.Extractors
{
    public class SingleElemntExtractor : IElementsExtractor
    {
        public bool TryExtractElements(GetMemberBinder binder, out object result, IEnumerable<XElement> dynamicObject)
        {
            result = TryGetElementsOrValue(dynamicObject.Elements(binder.Name));
            if (result != null) return true;
            return false;
        }

        private static object TryGetElementsOrValue(IEnumerable<XElement> requestedElements)
        {
            if (requestedElements.HasPluralElements())
            {
                if (DynamicXmlExtensions.ContainsValueOnly((XElement) requestedElements.First()))
                    return requestedElements.Select(e => e.Value);
                return new DynamicXmlReader(requestedElements);
            }
            if (requestedElements.HasSingleElement())
            {
                var element = requestedElements.Single();
                if (element.ContainsValueOnly())
                    return element.Value;
                return new DynamicXmlReader(element, true);
            }
            return null;
        }

    }
}