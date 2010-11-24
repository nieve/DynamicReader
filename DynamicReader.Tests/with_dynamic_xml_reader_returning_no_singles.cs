using System.Collections.Generic;
using System.Xml.Linq;
using Machine.Specifications;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;

namespace DynamicReader.Tests
{
    public class with_dynamic_xml_reader_returning_no_singles : with_an_x_element
    {
        private Because of = () => { reader = new DynamicXmlReader(contacts); };
        private static dynamic reader;

        private It should_have_a_contacts_property = () => Assert.NotNull(reader.contact);
        private It should_have_object_graph_capacity = () => Assert.NotNull(reader.contact.address.city);
        private It value_only_properties_should_return_ienumerables = () => Assert.IsInstanceOf<IEnumerable<string>>(reader.contact.address.city);
        
        private It properties_should_be_case_sensitive = () => Assert.Throws<RuntimeBinderException>(() =>
            {
                var x = reader.Contact;
            });
        private It should_throw_on_an_inexistant_element = () => Assert.Throws<RuntimeBinderException>(()=>
           {
               var x = reader.contact.address.something;
           });
    }

    public class Phone
    {
        public string AreaCode { get; set; }
        public string Number { get; set; }
    }
}