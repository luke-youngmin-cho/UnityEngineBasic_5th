using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULB.RPG.UISystems
{
    public interface IUI
    {
        Canvas canvas { get; }
        event Action OnShow;
        event Action OnHide;

        void Show();
        void Hide();
    }
}
