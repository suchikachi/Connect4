using System;
using System.Text;
using System.Threading;
namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string cn4 = " ::::::::  ::::    :::     :::\n:+:    :+: :+:+:   :+:    :+:\n+:+        :+:+:+  +:+   +:+ +:+\n+#+        +#+ +:+ +#+  +#+  +:+\n+#+        +#+  +#+#+# +#+#+#+#+#+\n#+#    #+# #+#   #+#+#       #+#\n ########  ###    ####       ###\n";

            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < cn4.Length; i++)
            {
                Console.Write(cn4[i]);
                Thread.Sleep(1);
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nPlayer 1 username: ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            string player1 = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Player 2 username: ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            string player2 = Console.ReadLine();

            int[,] board = new int[6, 7];
            int currentPlayer = 1;
            int column, row;

            while (true)
            {
                Thread.Sleep(100);
                
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("\n 1 ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" 2 ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(" 3 ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" 4 ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(" 5 ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" 6 ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write(" 7 ");

                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine();

                    for (int j = 0; j < 7; j++)
                    {
                        if (j % 2 == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        if (board[i, j] == 0)
                        {
                            Console.Write(" \u2588 ");
                        }
                        else if (board[i, j] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(" x ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(" O ");
                        }
                    }

                    Thread.Sleep(50);
                }
                
                while (true)
                {
                    if (currentPlayer == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    if (currentPlayer == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }
                        
                    Console.Write($"\n\nPlayer {currentPlayer} column: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    if (int.TryParse(Console.ReadLine(), out column) && column >= 1 && column <= 7)
                    {
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Please choose a column between 1 and 7");
                }

                column--;
                row = 5;

                while (board[row, column] != 0)
                {
                    row--;
                    if (row < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Column is full");
                        break;
                    }
                }

                if (row >= 0)
                {
                    board[row, column] = currentPlayer;
                    if (CheckWin(board, row, column))
                    {
                        if (currentPlayer == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n{player1} wins!");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n{player2} wins!");
                            Console.ReadKey();
                            break;
                        }
                        
                    }

                    if (currentPlayer == 1)
                    {
                        currentPlayer = 2;
                    }
                    else
                    {
                        currentPlayer = 1;
                    }
                }
            }
        }

        static bool CheckWin(int[,] board, int row, int column)
        {
            return CheckHorizontalWin(board, row, column) || 
                CheckVerticalWin(board, row, column) || 
                CheckDiagonalLeftWin(board, row, column) || 
                CheckDiagonalRightWin(board, row, column);
        }

        static bool CheckHorizontalWin(int[,] board, int row, int column)
        {
            int player = board[row, column];
            int count = 1;

            // Check to the right
            for (int i = column + 1; i < 7; i++)
            {
                if (board[row, i] == player)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            // Check to the left
            for (int i = column - 1; i >= 0; i--)
            {
                if (board[row, i] == player)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count >= 4;
        }

        static bool CheckVerticalWin(int[,] board, int row, int column)
        {
            int player = board[row, column];
            int count = 1;

            // Check upward
            for (int i = row - 1; i >= 0; i--)
            {
                if (board[i, column] == player)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            // Check downward
            for (int i = row + 1; i < 6; i++)
            {
                if (board[i, column] == player)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count >= 4;
        }


        static bool CheckDiagonalLeftWin(int[,] board, int row, int column)
        {
            int player = board[row, column];
            int count = 1;

            // Check up and left
            for (int i = 1; i < 7; i++)
            {
                if (row - i >= 0 && column - i >= 0)
                {
                    if (board[row - i, column - i] == player)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            // Check down and right
            for (int i = 1; i < 7; i++)
            {
                if (row + i < 6 && column + i < 7)
                {
                    if (board[row + i, column + i] == player)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return count >= 4;
        }

        static bool CheckDiagonalRightWin(int[,] board, int row, int column)
        {
            int player = board[row, column];
            int count = 1;

            // Check up and right
            for (int i = 1; i < 7; i++)
            {
                if (row - i >= 0 && column + i < 7)
                {
                    if (board[row - i, column + i] == player)
                    {
                        if (board[row - i, column + i] == player)
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Check down and left
            for (int i = 1; i < 7; i++)
            {
                if (row + i < 6 && column - i >= 0)
                {
                    if (board[row + i, column - i] == player)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return count >= 4;
        }
    }
}
