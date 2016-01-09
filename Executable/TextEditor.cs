using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfFun.Tools;
using System.Diagnostics;
using System.IO;

namespace HouseOfFun.Executable
{
    class TextEditor : GenericClass
    {
        ConsoleKeyInfo keyPressed;

        TextEditBuffer textEditBuffer;

        public string @textDocumentPath { get; set; }
        string documentName;

        public int cursorX;
        public int cursorY;

        bool shouldExit;

        public override void Execute()
        {
            Initialize();

            Run();

            GameCore.Reset();
        }

        private void Initialize()
        {
            Console.Clear();
            Console.CursorVisible = true;
#if RELEASE
            Console.Write("Please enter the name of your document: ");
            documentName = Console.ReadLine();
#endif 
#if DEBUG
            documentName = "DEBUG";
#endif
            Console.Clear();
            Console.Write(new string(' ', (Console.BufferWidth / 2) - (documentName.Length / 2)));
            Console.Write(documentName);

            textEditBuffer = new TextEditBuffer();
            textEditBuffer.Initialize();

            cursorX = Console.CursorLeft;
            cursorY = Console.CursorTop;

            Console.SetCursorPosition(1, 1);
        }

        private void Run()
        {
            
            shouldExit = false;
            while (!shouldExit)
            {

                keyPressed = Console.ReadKey(true);

                cursorX = Console.CursorLeft;
                cursorY = Console.CursorTop;

                if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    if (cursorY > 1)
                    {
                        if (cursorX - 1 <= textEditBuffer.characterList.ElementAt(cursorY - 2).Count)
                            Console.SetCursorPosition(cursorX, cursorY - 1);
                        else
                            Console.SetCursorPosition(textEditBuffer.characterList.ElementAt(cursorY - 2).Count + 1, cursorY - 1);
                    }
                }
                else if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    try
                    {
                        if (cursorX - 1 <= textEditBuffer.characterList.ElementAt(cursorY).Count)
                            Console.SetCursorPosition(cursorX, cursorY + 1);
                        else
                            Console.SetCursorPosition(textEditBuffer.characterList.ElementAt(cursorY).Count + 1, cursorY + 1);
                    }
                    catch { }
                }
                else if (keyPressed.Key == ConsoleKey.LeftArrow)
                {
                    if (cursorX > 1)
                        Console.SetCursorPosition(cursorX - 1, cursorY);
                }
                else if (keyPressed.Key == ConsoleKey.RightArrow)
                {
                    if (cursorX < textEditBuffer.characterList.ElementAt(cursorY - 1).Count + 1)
                        Console.SetCursorPosition(cursorX + 1, cursorY);
                }

                else if (keyPressed.Key == ConsoleKey.Backspace)
                    textEditBuffer.Remove(cursorX, cursorY);
                else if (keyPressed.Key == ConsoleKey.Enter)
                    textEditBuffer.NewLine(cursorX, cursorY);
                else if (keyPressed.Key == ConsoleKey.Home)
                    Console.SetCursorPosition(1, cursorY);
                else if (keyPressed.Key == ConsoleKey.End)
                    Console.SetCursorPosition(textEditBuffer.characterList.ElementAt(cursorY - 1).Count + 1, cursorY);
                else if (keyPressed.Key == ConsoleKey.Escape)
                    FinishUp();
                else
                    textEditBuffer.Write(keyPressed.KeyChar, cursorX, cursorY);
            }
        }

        private void FinishUp()
        {
            if (textDocumentPath == null)
            {
                Console.SetCursorPosition(0, textEditBuffer.characterList.Count + 2);
                Console.Write("Enter save directory: ");
                textDocumentPath = Console.ReadLine();

            }
            string[] output = new string[textEditBuffer.characterList.Count];
            int i = 0;
            foreach (var list in textEditBuffer.characterList)
            {
                foreach (char c in textEditBuffer.characterList.ElementAt(i))
                {
                    output[i] += c;
                }
                i++;
            }
            using (StreamWriter file = new StreamWriter(textDocumentPath + documentName + ".txt"))
            {
                foreach (string line in output)
                {
                    file.WriteLine(line);
                }
            }
            Console.SetCursorPosition(0, textEditBuffer.characterList.Count + 3);
            Console.Write("Save successful! Press enter to return");
            Console.ReadLine();
            shouldExit = true;
        }
    }
}
