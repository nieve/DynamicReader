using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DynamicReader
{
    public static class DynamicXmlExtensions
    {
        public static bool HasPluralElements<T>(this IEnumerable<T> enumrbl)
            where T : class
        {
            var last = enumrbl.LastOrDefault();
            var first = enumrbl.FirstOrDefault();
            return first != null && last != null && first != last;
        }

        public static bool HasSingleElement<T>(this IEnumerable<T> enumrbl)
            where T : class
        {
            var value = enumrbl.SingleOrDefault();
            return value != null;    
        }

        public static bool ContainsValueOnly(this XElement element)
        {
            return !element.HasElements && !element.HasAttributes;
        }
    }
}