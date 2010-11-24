using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Linq;

namespace DynamicReader.Extractors
{
    //TODO: Add tests
    public interface IElementsExtractor
    {
        bool TryExtractElements(GetMemberBinder binder, out object result, IEnumerable<XElement> dynamicObject);
    }
}