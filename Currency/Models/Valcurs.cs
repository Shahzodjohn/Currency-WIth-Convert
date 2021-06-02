using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Currency.Models
{


	[XmlRoot(ElementName = "ValCurs")]
	public class ValCurs
	{
		public int Id { get; set; }
		[XmlElement(ElementName = "Valute")]
		public virtual List<Valute> Valute { get; set; }
		[XmlAttribute(AttributeName = "Date")]
		public string Date { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}

}

