using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int whosPlaying = 1;
            char[] spotPicker = { '1', '2', '3', '4', '5', '6', '7', '8', '9' }; //spots on the board u can pick
            int status = 0; //game status, is it still going, has someone won or is it a draw

            do  // using do while because idk how many loops, the game keeps going until someone wins
            {
                Console.Clear();

                whosPlaying = whosNext(whosPlaying);

                introToGame(whosPlaying); //starts game and lets whoever's turn it is to pick a spot
                drawGameBoard(spotPicker); //draws game board every round

                Game(spotPicker, whosPlaying);

                status = whowon(spotPicker);


            } while (status.Equals(0)); // while game is still going, no winner or draw yet

            if (status.Equals(1)) // somebody won
            {
                Console.Clear();
                introToGame(whosPlaying);
                drawGameBoard(spotPicker);
                Console.WriteLine($"Player {whosPlaying} won the game!");
            }

            if (status.Equals(2)) // games a draw
            {
                Console.Clear();
                introToGame(whosPlaying);
                drawGameBoard(spotPicker);

                Console.WriteLine("Its a draw.");
            }
        }

        private static int whowon(char[] spotPicker)
        {
            if (isitaDraw(spotPicker))
            {
                return 2;
            }

            if (gameWinner(spotPicker))
            {
                return 1;
            }

            return 0;
        }

        private static bool isitaDraw(char[] spotPicker)
        {
            return spotPicker[0] != '1' &&
                   spotPicker[1] != '2' &&
                   spotPicker[2] != '3' &&
                   spotPicker[3] != '4' &&  // got all this online i could not understand how to setup a draw game
                   spotPicker[4] != '5' &&
                   spotPicker[5] != '6' &&
                   spotPicker[6] != '7' &&
                   spotPicker[7] != '8' &&
                   spotPicker[8] != '9';
        }

        private static bool gameWinner(char[] spotPicker) // checking if its a winning combo to declare a winner (column, row, diag)
        {
            if (possibleCombos(spotPicker, 0, 1, 2))
            {
                return true;
            }

            if (possibleCombos(spotPicker, 3, 4, 5))
            {
                return true;
            }

            if (possibleCombos(spotPicker, 6, 7, 8))
            {
                return true;
            }

            if (possibleCombos(spotPicker, 0, 3, 6))
            {
                return true;
            }

            if (possibleCombos(spotPicker, 1, 4, 7))
            {
                return true;
            }

            if (possibleCombos(spotPicker, 2, 5, 8))
            {
                return true;
            }

            if (possibleCombos(spotPicker, 0, 4, 8))
            {
                return true;
            }

            if (possibleCombos(spotPicker, 2, 4, 6))
            {
                return true;
            }

            return false;
        }


        private static void Game(char[] spotPicker, int whosPlaying)
        {
            bool notvalidspot = true;
            do
            {

                string playerInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(playerInput) && playerInput.Equals("1") ||
                    playerInput.Equals("2") ||
                    playerInput.Equals("3") ||
                    playerInput.Equals("4") ||                   // if the player chooses a spot on the board thats not nothing and is 1-9
                    playerInput.Equals("5") ||                  // 
                    playerInput.Equals("6") ||
                    playerInput.Equals("7") ||
                    playerInput.Equals("8") ||
                    playerInput.Equals("9"))
                {


                    int.TryParse(playerInput, out var playerSelection); //converting the players input into a int variable (got this online)

                    char spotTheyChose = spotPicker[playerSelection - 1]; // have to subtract one because its an array and they start at 0

                    if (spotTheyChose.Equals('X') || spotTheyChose.Equals('O')) // sees if the slot is taken
                    {
                        Console.WriteLine("That spot is already taken, choose another spot");
                    }
                    else
                    {
                        spotPicker[playerSelection - 1] = XorO(whosPlaying); //if its not taken itll figure out X or O placement

                        notvalidspot = false;
                    }
                }
                else
                {
                    Console.WriteLine("You need to pick a number 1 trough 9");
                }
            } while (notvalidspot);
        }



        private static char XorO(int whosPlaying)
        {
            if (whosPlaying % 2 == 0) // if theres no remainder, so its player 2
            {
                return 'O'; // itll return an O for player 2 because player 2 is O's
            }

            return 'X'; // if its player 1, itll place an X
        }
        static void introToGame(int PlayerNumber) //since theres only 2 players, 1 or 2, im using int
        {
            // intro to game
            Console.WriteLine("Welcome to Tic Tac Toe!");

            Console.WriteLine("Player 1: X | Player 2: O");

            // see whos turn it is and instruct user to pick number 1 through 9 on gameboard
            Console.WriteLine($"Player {PlayerNumber}'s turn, select a spot numbered 1 through 9"); // added the $ to add PlayerNumber in a string (googled it)

        }
        static void drawGameBoard(char[] spotPicker)
        {
            Console.WriteLine($"  {spotPicker[0]} | {spotPicker[1]} | {spotPicker[2]}"); // have to use an array because theres multiple values (X and O) going into one spot, and arrays start at 0
            Console.WriteLine(" ---|---|---");
            Console.WriteLine($"  {spotPicker[3]} | {spotPicker[4]} | {spotPicker[5]}");
            Console.WriteLine(" ---|---|---");
            Console.WriteLine($"  {spotPicker[6]} | {spotPicker[7]} | {spotPicker[8]}");
            Console.WriteLine("Choose a spot: ");
        }

        static int whosNext(int player)
        {
            if (player.Equals(1)) // learned player.Equals because its a bool
            {
                return 2;        // if we pass player 2, the condition is not met
            }
            else                 // if its player 2, next one is player 1
            {
                return 1;
            }
        }
        private static bool possibleCombos(char[] testing, int pos1, int pos2, int pos3)
        {
            return testing[pos1].Equals(testing[pos2]) && testing[pos2].Equals(testing[pos3]);
        }
    }
}