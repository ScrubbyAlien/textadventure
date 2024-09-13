// See https://aka.ms/new-console-template for more information
using System;

namespace TextAdventure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hero hero = new();
            Monster minotaur = new("minotaur");
            Monster goblin = new("goblin");
            while (hero.Location != "quit") 
            {
                Console.Clear();
                switch (hero.Location)
                {
                    case "newgame":
                        NewGame(hero, minotaur, goblin);
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
                        Rooms.ThirdRoom(hero, goblin);
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
                        Rooms.GameOver(hero, minotaur);
                        break;
                    
                    default:
                        Console.Error.WriteLine($"You forgot to implement '{hero.Location}'!");
                        break;
                }
            }
        }

        static void NewGame(Hero hero, Monster minotaur, Monster goblin)
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
            minotaur.Reset(250, 20);
            goblin.Reset(110, 10);

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