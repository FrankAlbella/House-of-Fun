using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseOfFun.Tools;
using HouseOfFun.Executable;
using System.IO;

namespace HouseOfFun
{
    class GameCore
    {
        static ConsoleKeyInfo keyPressed;

        public static int CursorX { get; set; }
        public static int CursorY { get; set; }

        public static MenuItem<TicTacCore> ticTacToeMenu;
        static MenuItem<TextEditor> textEditorMenu;
        static MenuItem<GenericClass> exampleMenuItem2;
        static MenuItem<GenericClass> exampleMenuItem3;
        static MenuItem<GenericClass> exampleMenuItem4;
        static MenuItem<GenericClass> exampleMenuItem5;
        static MenuItem<GenericClass> exampleMenuItem6;

        static List<IMenuItem> menuItems;
        static int[] numberOfItems;

        static string userName = Environment.UserName;
        static string documentPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string directoryPath = documentPath + @"\House of Fun";

        static void Main(string[] args)
        {
#if DEBUG
            TextEditor textEditor = new TextEditor();
            textEditor.Execute();
#endif
            Initalize();

            Run();

        }
        public static void Reset()
        {
            try
            {
                Console.Clear();
                DrawTitle();
                for (int i = 0; i < menuItems.Count; i++)
                {
                    var menuItem = menuItems.ElementAt(i);
                    menuItem.Write();
                }
                Console.SetCursorPosition(CursorX, CursorY);
                Console.CursorVisible = false;
            }
            catch(NullReferenceException)
            {
                Console.Clear();
                Initalize();
            }
        }
        private static void Initalize()
        {
            Console.Clear();

            DirectoryInfo di = Directory.CreateDirectory(directoryPath);

            Console.CursorVisible = false;
            DrawTitle();
            //Initalize all menu items
            ticTacToeMenu = new MenuItem<TicTacCore>("Tic Tac Toe", 0, 5);
            textEditorMenu = new MenuItem<TextEditor>("Text Editor", 11, 6);
            exampleMenuItem2 = new MenuItem<GenericClass>("Example2", 19, 5);
            exampleMenuItem3 = new MenuItem<GenericClass>("Example3", 27, 6);
            exampleMenuItem4 = new MenuItem<GenericClass>("Example4", 35, 5);
            exampleMenuItem5 = new MenuItem<GenericClass>("Example5", 43, 6);
            exampleMenuItem6 = new MenuItem<GenericClass>("Example6", 51, 5);

            // Add all menu items to the list
            menuItems = new List<IMenuItem>();
            menuItems.Add(ticTacToeMenu);
            menuItems.Add(textEditorMenu);
            menuItems.Add(exampleMenuItem2);
            menuItems.Add(exampleMenuItem3);
            menuItems.Add(exampleMenuItem4);
            menuItems.Add(exampleMenuItem5);
            menuItems.Add(exampleMenuItem6);

            Console.SetCursorPosition(0, 5);

            CursorX = Console.CursorLeft;
            CursorY = Console.CursorTop;

            numberOfItems = new int[menuItems.Count];
        }

        private static void Run()
        {
            bool shouldExit = false;
            while(!shouldExit)
            {
                keyPressed = Console.ReadKey(true);

                //Down arrow -- down movement
                if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    int newX = 1;
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        var menuItem = menuItems.ElementAt(i);
                        if (CursorY + 1 == menuItem.GetItemY())
                            numberOfItems[i] = CursorX - menuItem.GetItemX();
                    }
                    for (int i = 0; i < numberOfItems.Length; i++)
                    {

                        if (numberOfItems[i] == 0)
                            numberOfItems[i] += 10000;
                    }
                    for (int i = 0; i < numberOfItems.Length; i++)
                    {
                        if (i < numberOfItems.Length - 1)
                        {
                            if (Math.Abs(numberOfItems[i]) < Math.Abs(numberOfItems[i + 1]))
                            {
                                numberOfItems[i + 1] = numberOfItems[i];
                            }
                        }
                        else if (i == numberOfItems.Length - 1)
                        {
                            for (int y = 0; y < numberOfItems.Length; y++)
                            {
                                numberOfItems[y] = numberOfItems[i];
                            }
                        }
                    }
                    
                    if (numberOfItems.Max() > 0)
                        newX = CursorX - numberOfItems.Max();
                    else if (numberOfItems.Max() < 0)
                        newX = CursorX + Math.Abs(numberOfItems.Max());

                    if (newX < 1000 && newX > -1000)
                        Console.SetCursorPosition(newX, CursorY + 1);
                    Array.Clear(numberOfItems, 0, numberOfItems.Length);
                }

                //Up arrow -- up movement
                if (keyPressed.Key == ConsoleKey.UpArrow && CursorY > 0)
                {
                    int newX = 1;
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        var menuItem = menuItems.ElementAt(i);
                        if (CursorY - 1 == menuItem.GetItemY())
                            numberOfItems[i] = CursorX - menuItem.GetItemX();
                    }
                    for (int i = 0; i < numberOfItems.Length; i++)
                    {

                        if (numberOfItems[i] == 0)
                            numberOfItems[i] += 10000;
                    }
                    for (int i = 0; i < numberOfItems.Length; i++)
                    {
                        if (i < numberOfItems.Length - 1)
                        {
                            if (Math.Abs(numberOfItems[i]) < Math.Abs(numberOfItems[i + 1]))
                            {
                                numberOfItems[i + 1] = numberOfItems[i];
                            }
                        }
                        else if (i == numberOfItems.Length - 1)
                        {
                            for (int y = 0; y < numberOfItems.Length; y++)
                            {
                                numberOfItems[y] = numberOfItems[i];
                            }
                        }
                    }

                    if (numberOfItems.Max() > 0)
                        newX = CursorX - numberOfItems.Max();
                    else if (numberOfItems.Max() < 0)
                        newX = CursorX + Math.Abs(numberOfItems.Max());

                    if (newX < 1000 && newX > -1000)
                        Console.SetCursorPosition(newX, CursorY - 1);
                    Array.Clear(numberOfItems, 0, numberOfItems.Length);
                }

                //Right arrow -- right movement
                if (keyPressed.Key == ConsoleKey.RightArrow)
                {
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        var menuItem = menuItems.ElementAt(i);
                        if (CursorY == menuItem.GetItemY())
                            numberOfItems[i] = CursorX - menuItem.GetItemX();
                    }
                    for (int i = 0; i < numberOfItems.Length; i++)
                    {
                        if (numberOfItems[i] >= 0)
                            numberOfItems[i] -= 10000;
                    }
                    
                    int newX = CursorX + Math.Abs(numberOfItems.Max());
                    if (newX <  1000 && newX > -1000)
                        Console.SetCursorPosition(newX, CursorY);
                    Array.Clear(numberOfItems, 0, numberOfItems.Length);
                }

                //Left arrow -- left movement
                if (keyPressed.Key == ConsoleKey.LeftArrow && CursorX > 0)
                {
                    for (int i = 0; i < menuItems.Count; i++)
                    {
                        var menuItem = menuItems.ElementAt(i);
                        if (CursorY == menuItem.GetItemY())
                            numberOfItems[i] = CursorX - menuItem.GetItemX();
                    }
                    for (int i = 0; i < numberOfItems.Length; i++)
                    {
                        if (numberOfItems[i] <=0)
                            numberOfItems[i] += 10000;
                    }

                    int newX = CursorX - numberOfItems.Min();
                    if (newX < 1000 && newX > -1000)
                        Console.SetCursorPosition(newX, CursorY);
                    Array.Clear(numberOfItems, 0, numberOfItems.Length);
                }
                
                CursorX = Console.CursorLeft;
                CursorY = Console.CursorTop;

                foreach (var menuItem in menuItems)
                {
                    menuItem.Selected();
                }
                
                if (keyPressed.Key == ConsoleKey.Enter || keyPressed.Key == ConsoleKey.Spacebar)
                {
                    foreach (var menuItem in menuItems)
                    {
                        menuItem.CanExecute();
                        menuItem.Selected();
                    }
                }
            }
        }

        private static void DrawTitle()
        {
            string titleMessage = "Welcome to the House of Fun!";

            Console.Write(new string('=', Console.BufferWidth));
            Console.Write('='); Console.Write(new string(' ', Console.BufferWidth - 2)); Console.Write('=');
            Console.Write('=');
                Console.Write(new string(' ', ((Console.BufferWidth - 2) / 2) - (titleMessage.Length / 2)));
                Console.Write(titleMessage);
                Console.Write(new string(' ', ((Console.BufferWidth - 2) / 2) - (titleMessage.Length / 2)));
                Console.Write('=');
            Console.Write('='); Console.Write(new string(' ', Console.BufferWidth - 2)); Console.Write('=');
            Console.Write(new string('=', Console.BufferWidth));
        }
    }
}
