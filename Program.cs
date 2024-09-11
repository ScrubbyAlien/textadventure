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
            Monster minotaur = new Monster(250, 30);
            while (hero.Location != "quit") 
            {
                Console.Clear();
                switch (hero.Location)
                {
                    case "newgame":
                        NewGame(hero);
                        break;
                    
                    case "tableroom":
                        Rooms.TableRoom(hero);
                        break;
                    
                    case "corridor":
                        Rooms.Corridor(hero);
                        break;
                    
                    case "lockedroom":
                        Rooms.LockedRoom(hero);
                        break;
                    
                    case "thirdroom":
                        Rooms.ThirdRoom(hero);
                        break;
                    
                    case "backoutside":
                        Rooms.BackOutside(hero);
                        break;
                    
                    case "bossfight":
                        Rooms.BossFight(hero, minotaur);
                        break;
                    
                    case "win":
                        Rooms.Win(hero);
                        break;
                    
                    case "lose":
                        Rooms.Lose(hero);
                        break;
                    
                    case "gameover":
                        Rooms.GameOver(hero);
                        break;
                    
                    default:
                        Console.Error.WriteLine($"You forgot to implement '{hero.Location}'!");
                        break;
                }
            }
        }

        static void NewGame(Hero hero)
        {
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

        
        
        public static string Ask(string question)
        {
            string response = "";
            while (response == "")
            {
                Console.Write(question);
                response = Console.ReadLine().Trim();
            }

            return response;
        }

        public static bool AskYesOrNo(string question)
        {
            while (true)
            {
                string response = Ask(question + " (y/n) ").ToLower();
                if (response == "y") return true;
                if (response == "n") return false;
            }
        }
        
        public static string MakeDecision(List<string> answerList)
        {
            string response = "";
            while (!answerList.Contains(response.ToLower()))
            {
                string choices = String.Join("/", answerList);
                response = Ask($"(Choose: {choices}): ");
            }

            return response.ToLower();
        }

        public static int RollDice(int sides)
        {
            return new Random().Next(1, sides + 1);
        }
        
    }
}