using System;
using System.Collections.Generic;
using System.Dynamic;

namespace DynamicReader
{
    public class DynamicDictionaryReader : DynamicReader{
        private string _currentName = "";

        public DynamicDictionaryReader(Func<object> onMissing, 
            IDictionary<string, object> dictionary) : base(onMissing)
        {
            cache = dictionary;
        }

        public DynamicDictionaryReader(IDictionary<string, object> dictionary)
        {
            cache = dictionary;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var searchedValue = indexes[0];
            _currentName = "";
            return base.TryGetIndex(binder, indexes, out result);
        }

        public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
        {
            var isClass = binder.ReturnType.IsClass;
            if (binder.Name == "All")
            {
                _currentName = _currentName == "" ? binder.Name 
                    : _currentName + "." + binder.Name;
                result = this;
                return true;
            }
            return base.TryGetMember(binder, out result);
        }
    }
}