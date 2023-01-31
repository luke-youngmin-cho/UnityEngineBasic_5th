using System.Collections.Generic;

namespace ULB.RPG.AISystems
{
    public interface IChildren
    {
        List<Behaviour> children { get; set; }
    }
}
