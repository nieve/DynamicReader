using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace DynamicReader.Extractors
{
    public class SingleAttributesExtractor : IAttributesExtractor
    {
        public bool TryExtractAttributes(GetMemberBinder binder, out object result, IEnumerable<XElement> dynamicObject)
        {
            result = TryGetAttributesOrValue(dynamicObject.Attributes(binder.Name));
            if (result != null) return true;
            return false;
        }

        private static object TryGetAttributesOrValue(IEnumerable<XAttribute> requestedAttributes)
        {
            if (requestedAttributes.HasSingleElement())
                return requestedAttributes.Single().Value;
            return TryGetAttributes(requestedAttributes);
        }

        private static object TryGetAttributes(IEnumerable<XAttribute> requestedAttributes)
        {
            if (requestedAttributes.Any())
                return requestedAttributes.Select(a => a.Value);
            return null;
        }
    }
}