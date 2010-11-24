using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace DynamicReader.Extractors
{
    public class MultipleAttributesExtractor : IAttributesExtractor
    {
        public bool TryExtractAttributes(GetMemberBinder binder, out object result, IEnumerable<XElement> dynamicObject)
        {
            result = TryGetAttributes(dynamicObject.Attributes(binder.Name));
            return result != null;
        }

        private static object TryGetAttributes(IEnumerable<XAttribute> requestedAttributes)
        {
            if (requestedAttributes.Any())
                return requestedAttributes.Select(a => a.Value);
            return null;
        }
    }
}