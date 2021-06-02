using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Currency.Models
{
	[XmlRoot(ElementName = "Valute")]
	public class Valute
	{
		
		[XmlElement(ElementName = "CharCode")]
		public string CharCode { get; set; }
		[XmlElement(ElementName = "Nominal")]
		public string Nominal { get; set; }
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "Value")]
		public string Value { get; set; }
		[XmlAttribute(AttributeName = "ID")]
		public string ID { get; set; }
		public virtual ValCurs ValCurs { get; set; }
	}
}
