using System;
using System.Collections.Generic;

namespace HouseOfFun.Tools
{
    class WriteBuffer
    {
        public int arrayIndex = 0;
        public char[,] characterArray { get; set; }
        char[,] originalCharacterArray;
        

        public void Write(string s)
        {
            int i = 0;
            foreach (char character in s)
            {
                characterArray[arrayIndex, i] = character;
                i++;
            }

            Print();

            arrayIndex++;
        }

        private void Print(bool newLine = true)
        {
            for (int y = 0; y < characterArray.GetLength(1); y++)
            {
                Console.Write(characterArray[arrayIndex, y]);
                if (y == characterArray.GetLength(1) - 1 && newLine == true)
                    Console.Write("\n");
            }
        }

        public void SetupBuffer(int rows, int columns)
        {
            characterArray = new char[rows, columns];
            originalCharacterArray = new char[rows, columns];
        }

        public void StoreBuffer()
        {
            arrayIndex = 0;
            while (arrayIndex < 3)
            {
                for (int x = 1; x < 11; x += 4)
                    originalCharacterArray[arrayIndex, x] = characterArray[arrayIndex, x];
                arrayIndex++;
            }
        }

        public void ResetBuffer()
        {
            arrayIndex = 0;
            
            
            while (arrayIndex < 3)
            {
                int x = 1;
                while (x < 11)
                    { characterArray[arrayIndex, x] = originalCharacterArray[arrayIndex, x]; x += 4; }
                Print();
                arrayIndex++;
                
            }
            Console.SetCursorPosition(5, 1);
        }

        public void ClearBuffer()
        {
            Array.Clear(characterArray, arrayIndex - 1, characterArray.GetLength(0));
            arrayIndex = 0;
        }

        public void WriteIfSpace(string s)
        {
            if (characterArray[Console.CursorTop, Console.CursorLeft] == ' ')
            {
                Console.Write(s);
                    
                characterArray[Console.CursorTop, Console.CursorLeft - 1] = (char)s.ToCharArray()[0];
            }
            else
                return;
        }

        public bool IsCharSpace(int x, int y)
        {
            if (characterArray[Console.CursorTop, Console.CursorLeft] == ' ')
                return true;
            else
                return false;
        }

        public void WriteAt(string s, int x, int y, int originalX, int originalY)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(x, y);
            Console.Write(s);
            Console.SetCursorPosition(originalX, originalY);
        }


    }
}
