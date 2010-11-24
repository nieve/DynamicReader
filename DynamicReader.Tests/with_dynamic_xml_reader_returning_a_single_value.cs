using Machine.Specifications;
using NUnit.Framework;

namespace DynamicReader.Tests
{
    public class with_dynamic_xml_reader_returning_a_single_value : with_a_signle_line_element
    {
        private static dynamic reader;

        private Because of = () => { reader = new DynamicXmlReader(contacts, true); };

        private It should_have_single_phone_property = () =>
            {
                var number = reader.Contact.Phone.Number;
                Assert.AreEqual(number, "555555");
            };
        private It should_have_phone_property = () => Assert.IsInstanceOf<string>(reader.Contact.Name);
    }
}