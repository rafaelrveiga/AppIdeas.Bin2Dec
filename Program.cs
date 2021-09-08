using System.Text.RegularExpressions;
using System;

namespace Bin2Dec
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello! Let's convert a binary number in a decimal number.\n");
            try
            {
                string input = CatchInput();
                ulong output = ConvertToUInt64(input);
                Message(TypeMessage.calculatedMessage, output.ToString());
            }
            catch (AbortedByUserException)
            {
                return;
            }
            catch (System.OverflowException)
            {
                Message(TypeMessage.exceptionMessage, "The input is too large for a Decimal convert.\nThe program will close.");
            }
            catch (System.Exception exception)
            {
                Message(TypeMessage.exceptionMessage, exception.Message);
            }
            finally
            {
                Message(TypeMessage.finalMessage);
            }
        }

        private static string CatchInput()
        {
            Regex validator = new Regex("^[0-1]+$");
            string input;
            bool validInput;

            do
            {
                Console.Write("Type your input [0-1]: ");
                input = Console.ReadLine();
                validInput = validator.IsMatch(input);

                if (!validInput)
                {
                    Console.Write("\nOops... It's a not valid input.");
                    ConsoleKeyInfo tryAgainCatchInput;
                    do
                    {
                        Console.Write("\nTry again (y/n)? ");
                        tryAgainCatchInput = Console.ReadKey();
                    } while (tryAgainCatchInput.KeyChar.ToString().ToUpper() != "Y" &&
                             tryAgainCatchInput.KeyChar.ToString().ToUpper() != "N");

                    if (tryAgainCatchInput.KeyChar.ToString().ToUpper() == "N")
                        throw new AbortedByUserException();

                    Console.WriteLine("\n");
                }
            } while (!validInput);

            return input;
        }

        private static ulong ConvertToUInt64(string input)
        {
            ulong output = 0;
            for (int i = input.Length - 1; i >= 0; i--)
                output += Convert.ToUInt64(input.Substring(i, 1)) * Convert.ToUInt64(Math.Pow(2, (input.Length - 1 - i)));
            return output;
        }

        private static void Message(TypeMessage typeMessage, params string[] args)
        {
            switch (typeMessage)
            {
                case TypeMessage.calculatedMessage:
                    Console.Write($"\nCalculating... here is your result: {args[0]}");
                    break;
                case TypeMessage.exceptionMessage:
                    Console.Write($"\n{args[0]}");
                    break;
                case TypeMessage.finalMessage:
                    Console.WriteLine("\n\nSee you.");
                    break;
            }
        }
    }
}
