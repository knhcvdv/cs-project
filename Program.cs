// Import necessary libraries
using System;
using DataProcessing; // Import a custom namespace called DataProcessing

namespace EDF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TextProcessing tp;
            FileProcessing fp;

            int task = -1;

            // Check the number of command line arguments to  determine the task
            if (args.Length == 1) { task = 0; }
            else if (args.Length == 3 && (args[0] == "-e" || args[0] == "--encrypt")) { task = 1; }
            else if (args.Length == 2 && (args[0] == "-d" || args[0] == "--decrypt")) { task = 2; }
            else if (args.Length == 4 && (args[0] == "-f" || args[0] == "--file")) { task = 3; }

            switch (task)
            {
                case 0:
                    // Handle the case when the user needs help
                    if (args[0] == "-h" || args[0] == "--help")
                    {
                        Console.WriteLine("\n\x1b[30;1mList of commands as examples:\x1b[0m");
                        Console.WriteLine("• edf -e Hello,\\ world! mIUhi19f7fgY81");
                        Console.WriteLine("• edf -d mIUhi19f7fgY81");
                        Console.WriteLine("• edf -f /source/file/path /new/file/name KEY\n");
                    }
                    break;

                case 1:
                    // Case for encryption
                    tp = new TextProcessing(args[2]);
                    string encrypted = tp.encrypt(args[1]);
                    Console.WriteLine($"Encrypted text using the key you entered:\n\n\x1b[32m{encrypted}\x1b[0m");
                    break;

                case 2:
                    // Case for decryption
                    Console.Write("\x1b[30;1mEnter your code:\x1b[0m ");

                    string inputText = string.Empty;
                    while (true)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            Console.WriteLine();
                            break;
                        }
                        char inputChar = keyInfo.KeyChar;
                        inputText += inputChar;
                    }
                    if (inputText == null) inputText = "";

                    tp = new TextProcessing(args[1]);
                    string decrypted = tp.decrypt(inputText);
                    Console.WriteLine($"\nDecrypted text using the key you entered:\n\n\x1b[32m{decrypted}\x1b[0m");
                    break;

                case 3:
                    // Case for file processing
                    fp = new FileProcessing(args[3]);
                    fp.XOR(args[1], args[2]);
                    Console.WriteLine($"A file named \x1b[32;1m{args[2]}\x1b[0m was successfully created.");
                    break;

                default:
                    // Handle cases with incorrect or missing parameters
                    Console.WriteLine("\x1b[33mYou muxt enter the correct parameters to complete the task! \x1b[0m");
                    break;
            }
        }
    }
}
