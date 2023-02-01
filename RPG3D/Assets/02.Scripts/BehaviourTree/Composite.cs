using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULB.RPG.AISystems
{
    public abstract class Composite : Behaviour, IChildren
    {
        public List<Behaviour> children { get; set; }

        public Composite() 
        {
            children = new List<Behaviour>();
        }
    }
}
