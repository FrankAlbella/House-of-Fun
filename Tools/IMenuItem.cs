using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfFun.Tools
{
    abstract class IMenuItem
    {
        public bool isHightlighted;
        public int ItemX { get; set; }
        public int ItemY { get; set; }

        abstract public int GetItemX();
        abstract public int GetItemY();

        abstract public void Execute();
        abstract public void CanExecute();
        abstract public void Selected();
        abstract public void Write();
    }
}
