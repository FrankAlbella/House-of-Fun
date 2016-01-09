using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfFun.Tools
{
    class TextEditBuffer
    {
        public List<List<char>> characterList;

        public void Initialize()
        {
            characterList = new List<List<char>>();
            characterList.Capacity = 1;
            characterList.Add(new List<char>());

        }

        /// <summary>Used to write an item to the character list and print it to the screen</summary>
        /// <param name="c">Character added to the list and printed to the screen</param>
        /// <param name="x">X coordinate of the cursor</param>
        /// <param name="y">Y coordinate of the cursor</param>
        public void Write(char c, int x, int y)
        {
            characterList.ElementAt(y - 1).Insert(x - 1, c);
            if (characterList.ElementAt(y - 1).Count > Console.BufferWidth - 2)
            {
                if (characterList.Last().Count >= Console.BufferWidth - 2)
                {
                    characterList.Capacity += 1;
                    characterList.Add(new List<char>());

                    if (characterList.Count >= Console.BufferHeight)
                        Console.SetBufferSize(GameCore.windowWidth, Console.BufferHeight + 1);
                }
                for (int elementY = y; elementY < characterList.Count; elementY++)
                {
                    characterList.ElementAt(y).Insert(0, characterList.ElementAt(y - 1).Last());
                    characterList.ElementAt(y - 1).RemoveAt(characterList.ElementAt(y - 1).Count - 1);
                }
            }
            
            Update(x, y);

            if (x < Console.BufferWidth - 1)
                Console.SetCursorPosition(x + 1, y);
            else
                Console.SetCursorPosition(2, y + 1);
        }

        /// <summary>Updates all the characters after the cursor on the console window</summary>
        /// <param name="x">X coordinate of the cursor</param>
        /// <param name="y">Y coordinate of the cursor</param>
        private void Update(int x, int y)
        {
            for (int elementY = y - 1; elementY < characterList.Count; elementY++)
            {
                for (int elementX = x - 1; elementX < characterList.ElementAt(elementY).Count; elementX++)
                {
                    Console.Write(characterList.ElementAt(elementY).ElementAt(elementX));
                }
                x = 1;
                if (elementY + 1 != characterList.Count)
                    Console.SetCursorPosition(1, y + 1);
            }
            Console.SetCursorPosition(x, y);
        }

        /// <summary>Used to remove an item from the character list and remove it from the screen</summary>
        /// <param name="x">X coordinate of the cursor</param>
        /// <param name="y">Y coordinate of the cursor</param>
        public void Remove(int x, int y)
        {
            //Checks if the cursor is below the top row and if the list for that row is empty
            if (characterList.ElementAt(y - 1).Count == 0 && x + y != 2)
            {
                //Goes through every row
                for (int i = y; i < characterList.Count + 1; i++)
                {
                    try
                    {
                        characterList.ElementAt(i - 1).Clear();
                        Console.SetCursorPosition(1, i);
                        foreach (char c in characterList.ElementAt(i))
                        {
                            characterList.ElementAt(i - 1).Add(c);
                            Console.Write(c);
                        }
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(1, i);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        characterList.RemoveAt(i - 1);
                        characterList.Capacity -= 1;

                        Console.SetCursorPosition(1, i);
                        Console.Write(new string(' ', Console.BufferWidth));

                        //Sets the cursor to it's new position
                        try
                        {
                            Console.SetCursorPosition(characterList.ElementAt(y - 2).Count + 1, y - 1);
                        }
                        catch
                        {
                            Console.SetCursorPosition(1, 1);
                        }
                    }
                        
                }
            }
            else if (x + y != 2 && x > 1)
            {
                characterList.ElementAt(y - 1).RemoveAt(x - 2);
                Console.SetCursorPosition(1, y);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(1, y);
                foreach (char c in characterList.ElementAt(y - 1))
                {
                    Console.Write(c);
                }
                Console.SetCursorPosition(x - 1, y);
            }
        }

        /// <summary>Used for creating a new row in the buffer and the screen</summary>
        /// <param name="x">X coordinate of the cursor</param>
        /// <param name="y">Y coordinate of the cursor</param>
        public void NewLine(int x, int y)
        {
            int elementsAfterX = characterList.ElementAt(y - 1).Count - (x - 1);

            characterList.Capacity += 1;
            characterList.Add(new List<char>());

            if (y + 1 >= Console.BufferHeight)
                Console.SetBufferSize(Console.BufferWidth, Console.BufferHeight + 1);

            for (int i = characterList.Count - 1; i > y; i--)
            {
                characterList.ElementAt(i).Clear();
                Console.SetCursorPosition(1, i);
                foreach (char c in characterList.ElementAt(i - 1))
                {
                    characterList.ElementAt(i).Add(c);
                }
            }
            characterList.ElementAt(y).Clear();

            char[] copyArray = new char[elementsAfterX];
            characterList.ElementAt(y - 1).CopyTo(x - 1, copyArray, 0, elementsAfterX);
            foreach (char c in copyArray)
            {
                characterList.ElementAt(y).Add(c);
            }
            characterList.ElementAt(y - 1).RemoveRange(x - 1, elementsAfterX);

            int j = 0;
            foreach (var list in characterList)
            {
                
                Console.SetCursorPosition(1, j + 1);
                foreach (char c in characterList.ElementAt(j))
                {
                    Console.Write(c);
                }
                j++;
                Console.Write(new string(' ', Console.BufferWidth));
            }

            Console.SetCursorPosition(1, y + 1);

        }
    }
}
