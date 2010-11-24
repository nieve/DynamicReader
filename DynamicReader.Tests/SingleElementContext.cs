using System.Xml.Linq;
using Machine.Specifications;

namespace DynamicReader.Tests
{
    public abstract class with_a_signle_line_element
    {
        protected static XElement contacts;
        Establish context = () => {
            contacts = new XElement("Contacts",
                new XElement("Contact",
                new XElement("Name", "Jon Doe"),
                new XElement("Phone", 
                    new XElement("AreaCode", "0208"), 
                    new XElement("Number", 555555 )
                )));
        };
    }
}