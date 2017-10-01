using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditableAttribute : Attribute
    {
        public string DisplayName { get; set; } = null;
        public bool ReadOnly { get; set; } = false;
    }
}
