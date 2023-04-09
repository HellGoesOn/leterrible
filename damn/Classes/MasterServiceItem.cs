using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damn.Classes
{
    public class MasterServiceItem
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public MasterServiceItem(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
