using System;
using HouseOfFun.Tools;

namespace HouseOfFun.Executable
{
    class TicTacCore : GenericClass
    {
        #region Variables
        ConsoleKeyInfo charPressed;

        int cursorY;
        int cursorX;

        int playerIndex;

        WriteBuffer writeBuffer = new WriteBuffer();

        public int playerWinner { get; set; }
        public static bool isThereWinner { get; set; }
        #endregion

        public override void Execute()
        {
            Initialize();

            Run();

            writeBuffer.ClearBuffer();
            GameCore.Reset();
        }
        
        void Initialize()
        {
            Console.Clear();

            Console.TreatControlCAsInput = true;

            Console.CursorVisible = true;

            writeBuffer.SetupBuffer(3, 11);

            writeBuffer.Write("   |   |   ");
            writeBuffer.Write("   |   |   ");
            writeBuffer.Write("   |   |   ");

            Console.SetCursorPosition(5, 1);

            cursorY = Console.CursorTop;
            cursorX = Console.CursorLeft;
         
            playerIndex = 1;
            playerWinner = 0;


            writeBuffer.StoreBuffer();
        }

        void Run()
        {
            bool shouldExit = false;
            while (!shouldExit)
            {
                writeBuffer.WriteAt("Player " + playerIndex + "'s turn", 0, 3, cursorX, cursorY);

                charPressed = Console.ReadKey(true);

                if (charPressed.Key == ConsoleKey.DownArrow && cursorY < 2 ||
                    charPressed.Key == ConsoleKey.S && cursorY < 2)
                    Console.CursorTop += 1;
                else if (charPressed.Key == ConsoleKey.UpArrow && cursorY > 0 ||
                         charPressed.Key == ConsoleKey.W && cursorY > 0)
                    Console.CursorTop -= 1;
                else if (charPressed.Key == ConsoleKey.LeftArrow && cursorX > 0 ||
                         charPressed.Key == ConsoleKey.A && cursorX > 0)
                {
                    if (cursorX > 3 && cursorX < 7)
                        Console.SetCursorPosition(1, cursorY);
                    if (cursorX > 7 && cursorX < 10)
                        Console.SetCursorPosition(5, cursorY);
                }
                else if (charPressed.Key == ConsoleKey.RightArrow && cursorX < 9 ||
                         charPressed.Key == ConsoleKey.D && cursorX < 9)
                {
                    if (cursorX < 3)
                        Console.SetCursorPosition(5, cursorY);
                    if (cursorX > 3 && cursorX < 7)
                        Console.SetCursorPosition(9, cursorY);
                }
                else if (charPressed.Key == ConsoleKey.Spacebar && writeBuffer.IsCharSpace(cursorX, cursorY) ||
                            charPressed.Key == ConsoleKey.Enter && writeBuffer.IsCharSpace(cursorX, cursorY) ||
                            charPressed.Key == ConsoleKey.NumPad0 && writeBuffer.IsCharSpace(cursorX, cursorY))
                {
                    if (playerIndex == 1)
                    {
                        writeBuffer.WriteIfSpace("X");
                    }
                    else if (playerIndex == 2)
                    {
                        writeBuffer.WriteIfSpace("O");
                    }

                    switch (playerIndex)
                    {
                        case 1:
                            playerIndex = 2;
                            break;
                        case 2:
                            playerIndex = 1;
                            break;
                    }

                    Console.SetCursorPosition(5, 1);
                    CheckForWinner();

                    if (playerWinner > 0)
                        shouldExit = true;
                }
                else if (charPressed.Key == ConsoleKey.Escape)
                    shouldExit = true;
                else if (charPressed.Key == ConsoleKey.F1)
                    Reset();

                cursorY = Console.CursorTop;
                cursorX = Console.CursorLeft;
#if DEBUG
                writeBuffer.WriteAt("CursorX: " + cursorX + ", CursorY: " + cursorY, 0, 4, cursorX, cursorY);
#endif

            }
            if (playerWinner > 0)
                Winner(playerWinner);
        }

        private void Winner(int i)
        {
            Console.CursorVisible = false;
            if (playerWinner < 3)
                writeBuffer.WriteAt("Player " + i + ", you're winner!", 0, 3, cursorX, cursorY);
            else if (playerWinner == 3)
                writeBuffer.WriteAt("Tie! You're no winner!", 0, 3, cursorX, cursorY);
            writeBuffer.WriteAt("ESC to return, press any other key to play again.", 0, 4, cursorX, cursorY);
            if (Console.ReadKey(true).Key != ConsoleKey.Escape)
                { Reset(); Run(); }

        }

        

        private void Reset()
        {
            Console.Clear();
            writeBuffer.ResetBuffer();
            playerIndex = 1;
            playerWinner = 0;
            isThereWinner = false;
            Console.CursorVisible = true;
        }
        private void CheckForWinner()
        {
            int x = 1;
            int y = 0;

            var characterArray = writeBuffer.characterArray;

            for (y = 0; y < writeBuffer.arrayIndex; y++)
            {
                if (characterArray[y, x] == 'X' && characterArray[y, x + 4] == 'X' && characterArray[y, x + 8] == 'X')
                { playerWinner = 1; isThereWinner = true; }
                else if (characterArray[y, x] == 'X' && characterArray[y, x + 4] == 'X' && characterArray[y, x + 8] == 'X')
                { playerWinner = 1; isThereWinner = true; }

                else if (characterArray[y, x] == 'O' && characterArray[y, x + 4] == 'O' && characterArray[y, x + 8] == 'O')
                { playerWinner = 2; isThereWinner = true; }
                else if (characterArray[y, x] == 'O' && characterArray[y, x + 4] == 'O' && characterArray[y, x + 8] == 'O')
                { playerWinner = 2; isThereWinner = true; }
            }

            y = 0;

            for (x = 1; x < 11; x += 4)
            {
                if (characterArray[y, x] == 'X' && characterArray[y + 1, x] == 'X' && characterArray[y + 2, x] == 'X')
                { playerWinner = 1; isThereWinner = true; }

                else if (characterArray[y, x] == 'O' && characterArray[y + 1, x] == 'O' && characterArray[y + 2, x] == 'O')
                { playerWinner = 2; isThereWinner = true; }
            }

            x = 1;

            if (characterArray[y, x] == 'X' && characterArray[y + 1, x + 4] == 'X' && characterArray[y + 2, x + 8] == 'X')
            { playerWinner = 1; isThereWinner = true; }
            else if (characterArray[y, x + 8] == 'X' && characterArray[y + 1, x + 4] == 'X' && characterArray[y + 2, x] == 'X')
            { playerWinner = 1; isThereWinner = true; }

            else if (characterArray[y, x] == 'O' && characterArray[y + 1, x + 4] == 'O' && characterArray[y + 2, x + 8] == 'O')
            { playerWinner = 2; isThereWinner = true; }
            else if (characterArray[y, x + 8] == 'O' && characterArray[y + 1, x + 4] == 'O' && characterArray[y + 2, x] == 'O')
            { playerWinner = 2; isThereWinner = true; }

            if (!isThereWinner &&
                     characterArray[0, 1] != ' ' && characterArray[0, 5] != ' ' && characterArray[0, 9] != ' ' &&
                     characterArray[1, 1] != ' ' && characterArray[1, 5] != ' ' && characterArray[1, 9] != ' ' &&
                     characterArray[2, 1] != ' ' && characterArray[2, 5] != ' ' && characterArray[2, 9] != ' ')
            { playerWinner = 3; }
        }

        public override void CanExecute()
        {
            throw new NotImplementedException();
        }
    }

}
