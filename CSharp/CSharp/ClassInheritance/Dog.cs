using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritance
{
    internal class Dog : Creature, IFourLeggedWalker
    {
        public float TailLength;

        public override void Breath()
        {
            Console.WriteLine("개가 숨을 쉰다");
        }

        public void FourLeggedWalk()
        {
            Console.WriteLine("개가 사족보행 한다");
        }
    }
}
