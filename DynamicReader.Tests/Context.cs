using System;
using System.Xml.Linq;
using Machine.Specifications;

namespace DynamicReader.Tests
{
    public abstract class with_an_x_element
    {
        protected static XElement contacts;
        Establish context = () => {
            contacts = new XElement("contacts",
                new XElement("contact",
                    new XElement("name", "Patrick Hines"),
                    new XElement("phone", "206-555-0144",
                        new XAttribute("type", "home")),
                    new XElement("phone", "425-555-0145",
                        new XAttribute("type", "work")),
                    new XElement("address",
                        new XElement("street1", "123 Main St"),
                        new XElement("city", "Mercer Island"),
                        new XElement("state", "WA"),
                        new XElement("postal", "68042")
                    )
                ),
                new XElement("contact",
                    new XElement("name", "Jon Dire"),
                    new XElement("phone", "206-666-0144",
                        new XAttribute("type", "home")),
                    new XElement("phone", "425-672-0145",
                        new XAttribute("type", "work")),
                    new XElement("address",
                        new XElement("street1", "13 Drury St"),
                        new XElement("city", "Wimbledon"),
                        new XElement("state", "NY"),
                        new XElement("postal", "59042")
                    )
                ));
        } ;
    }
}
