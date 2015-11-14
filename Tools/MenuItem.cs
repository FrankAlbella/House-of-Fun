using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfFun.Tools
{
    class MenuItem<T> : IMenuItem where T : IMenuItem, new()
    {
        char[] itemName;
        string sItemName;

        new public int ItemX { get; set; }
        new public int ItemY { get; set; }

        new bool isHightlighted = false;

        private T gameClass = new T();

        public override int GetItemX()
        {
            return ItemX;
        }
        public override int GetItemY()
        {
            return ItemY;
        }

        public MenuItem(string s, int x, int y)
        {

            this.ItemX = x;
            this.ItemY = y;

            this.itemName = new char[s.Length];
            this.sItemName = s;

            int i = 0;
            foreach (char c in itemName)
            {
                itemName[i] = c;
                i++;
            }
            Write();
            Selected();
        }

        public override void CanExecute()
        {

            if (this.isHightlighted)
                gameClass.Execute();
        }

        public override void Write()
        {
            Console.SetCursorPosition(ItemX, ItemY);

            Console.Write(sItemName);
            
            Console.SetCursorPosition(0, 5);
        }

        // if the menu item is currently selected
        public override void Selected()
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            if(cursorX == ItemX && cursorY == ItemY)
            {
                this.isHightlighted = true;

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                Console.Write(sItemName);

                Console.ResetColor();              
            }
            else if(isHightlighted)
            {
                this.isHightlighted = false;

                Console.ResetColor();

                Console.SetCursorPosition(ItemX, ItemY);
                Console.Write(sItemName);
            }
            Console.SetCursorPosition(GameCore.CursorX, GameCore.CursorY);
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
