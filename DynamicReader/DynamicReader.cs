using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;

namespace DynamicReader
{
    public abstract class DynamicReader : DynamicObject
    {
        protected IDictionary<string, object> cache = new Dictionary<string, object>();
        protected Func<object> on_missing = () => {throw new Exception();};

        protected DynamicReader(Func<object> onMissing)
        {
            on_missing = onMissing;
        }

        protected DynamicReader()
        {
            
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            cache.Add(binder.Name, value);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = cache.ContainsKey(binder.Name) ? cache[binder.Name] : on_missing();
            return true;
        }

        //protected abstract void DynamicallySetMember(string propertyName, object value);
    }
}