using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfFun.Tools;

namespace HouseOfFun.Executable
{
    class GenericClass : IMenuItem
    {
        bool isHighlighted;

        public override void CanExecute()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {
            Console.Clear();
            Console.WriteLine("We are currently under construction, please come back later.");
            Console.WriteLine("Press ESC to return");

            bool shouldExit = false;
            
            while(!shouldExit)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                if (keyPressed.Key == ConsoleKey.Escape)
                    shouldExit = true;
            }
            GameCore.Reset();
        }

        public override int GetItemX()
        {
            throw new NotImplementedException();
        }

        public override int GetItemY()
        {
            throw new NotImplementedException();
        }

        public override void Selected()
        {
            throw new NotImplementedException();
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }
    }
}
