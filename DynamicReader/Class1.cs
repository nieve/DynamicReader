using System;
using System.Collections.Generic;

namespace DynamicReader
{
    public class Class1
    {
        [STAThread]
        public static void Main()
        {
            var dict = new Dictionary<string, object>
                           {
                               {"Name", "Robert"}, 
                               {"Age", 34}, 
                               {"Birthdate", new DateTime(1976, 1, 5)},
                               {"Address", new {Address1="52 rue bidon", Postcode = "59000", City="Lille"}}
                           };
            var o = DoSomething(dict);
            Get(o);
        }

        public static void Get(dynamic obj)
        {
            string name = obj.Name;
            int age = obj.Age;
            DateTime bday = obj.Birthdate;
            var address = obj.Address;
            var city = obj.Address.City;
            var o = obj.All["niv"];
        }

        public static dynamic DoSomething(IDictionary<string, object> dictionary)
        {
            DynamicReader obj = new DynamicDictionaryReader(dictionary);
            //foreach (string key in dictionary.Keys)
            //    obj.DynamicallySetMember(key, dictionary[key]);
            return obj;
        }
    }
}
