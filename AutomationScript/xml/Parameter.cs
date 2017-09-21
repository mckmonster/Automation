using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutomationScript.xml

{
    public class Parameter
    {
        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }

        [XmlAttribute]
        public string Value
        {
            get;
            set;
        }

        [XmlAttribute]
        public ParameterType Type
        {
            get;
            set;
        }
    }
}
