// See https://aka.ms/new-console-template for more information
using System;

namespace TextAdventure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Welcome to Text Adventure!!!!!!!!!!!");

            string name = "";
            while (name == "")
            {
                name = Ask("What is your name adventurer?\n");

                if (!AskYesOrNo($"Is your name {name}?"))
                {
                    name = "";
                }
            }*/

            Hero hero = new();
            NewGame(hero);
            while (hero.Location != "quit")
            {
                break;
            }

        }

        static void NewGame(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Text Adventure!!!!!!!!!!!");

            string name = "";
            while (name == "")
            {
                name = Ask("What is your name adventurer?\n");

                if (!AskYesOrNo($"Is your name {name}?"))
                {
                    name = "";
                }
            }

            hero.Name = name;
            hero.Location = "tableroom";

        }
        
        static string Ask(string question)
        {
            string response = "";
            while (response == "")
            {
                Console.Write(question);
                response = Console.ReadLine().Trim();
            }

            return response;
        }

        static bool AskYesOrNo(string question)
        {
            while (true)
            {
                string response = Ask(question + " (y/n) ").ToLower();
                if (response == "y") return true;
                if (response == "n") return false;
            }
        }

    }
}