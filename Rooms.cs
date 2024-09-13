namespace TextAdventure;

public static class Rooms
{
    public static void TableRoom(Hero hero)
    {
        hero.Items.Add("woodensword");
        
        Console.WriteLine("You are equipped with one wooden sword, and your task\n" +
                      "is to slay the monster at the end of the adventure. \n" +
                      "In front of you is a stone table with two items on it, a knife and a key\n" +
                      "You can only pick up one of these items.");
        
        List<string> answerList = ["key", "knife", "none"];

        switch (Program.MakeDecision(answerList))
        {
            case "key":
                hero.Items.Add("key");
                Console.WriteLine("You pick up the key and move into the corridor at the back of the room. (press enter to continue)");
                break;
            
            case "knife":
                hero.Items.Add("knife");
                hero.Items.Remove("woodensword");
                Console.WriteLine("You pick up the knife and leave your wooden sword. \n" +
                                  "You proceed to the corridor at the back of the room. (press enter to continue)");
                break;
            
            case "none":
                Console.WriteLine("You leave the key and the knife on the stone table and move to the corridor at the \n" +
                                  "back of the room. (press enter to continue)");
                break;
        }

        Console.ReadLine();
        hero.Location = "corridor";
    }
        
    public static void Corridor(Hero hero)
    {
            Console.WriteLine("You exit the room and find yourself standing in a dark hallway. \n" +
                          "You can either enter another room on your right side, \n" +
                          "or continue down the hallway on your left.");
            
            List<string> answerList = ["left", "right"];
            while (hero.Location == "corridor")
            {
                switch (Program.MakeDecision(answerList))
                {
                    case "left":
                        if (hero.Items.Contains("key"))
                        {
                            Console.WriteLine("The door is locked but the key you found in the previous room unlocks it. \n" +
                                              "You enter the room (press enter to continue)");
                            hero.Items.Remove("key");
                            hero.Location = "lockedroom";
                            break;
                        }
                        Console.WriteLine("The door is locked and you turn back. You may enter the room on the right.");
                        answerList.Remove("left");
                        break;
                
                    case "right":
                        Console.WriteLine("You enter the room on the right. (press enter to continue)");
                        hero.Location = "thirdroom";
                        break;
                }
            }

            Console.ReadLine();
    }

    public static void LockedRoom(Hero hero)
    {
        Console.WriteLine("Inside the locked room you find a shiny sword!");

        if (Program.AskYesOrNo("Do you want it instead of your wooden sword?"))
        {
            Console.WriteLine("You pick up the new shiny sword, leaving your old wooden sword behind.");
            hero.Items.Remove("woodensword");
            hero.Items.Add("shinysword");
        }
        else
        {
            Console.WriteLine("You decide to trust your old faithful wooden sword, leaving the shiny sword.");
        }
        
        Console.WriteLine("You pass through a door at the end of the room, entering a new room. (press enter to continue)");
        Console.ReadLine();
        hero.Location = "thirdroom";
    }

    public static void ThirdRoom(Hero hero, Monster goblin)
    {
        Console.WriteLine("Upon entering the room you find a lifeless corpse on the ground in front of you.\n" +
                          "Its hand is clasped around something shiny.\n");

        if (Program.AskYesOrNo("Do you want to steal the shiny object?"))
        {
            //here we fight
            Console.WriteLine("As you move to pick up the shiny object, a goblin ambushes you from the shadow!\n" +
                              "(press enter to fight!)");
            Console.ReadLine();
            while (!goblin.Defeated())
            {
                Console.Clear();
                hero.Defending = false;
                if (goblin.Dancing) Console.WriteLine("The goblin is rocking out! What do you do?");
                else Console.WriteLine("The goblin prepares itself! What do you do?");
                switch (Program.MakeDecision(["attack", "defend", "talk"]))
                {
                    case "attack":
                        if (goblin.Dancing)
                        {
                            if (Program.RollDice(2) == 2)
                            {
                                Console.WriteLine("You swing at the goblin and draw blood.");
                                goblin.TakeDamage(hero.GetDamage());
                                break;
                            }
                            Console.WriteLine("The goblin's dancing was too much and you miss your swing.");
                            break;
                        }
                        Console.WriteLine("You swing at the goblin and draw blood.");
                        goblin.TakeDamage(hero.GetDamage());
                        break;
                    case "defend":
                        Console.WriteLine("You put your arms up and brace for the incoming attack.");
                        hero.Defending = true;
                        break;
                    case "talk":
                        Console.WriteLine("You attempt to talk to the goblin but it does not listen.");
                        break;
                }

                if (!goblin.Defeated())
                {
                    goblin.MakeAttack(hero);
                    if (hero.Health <= 0)
                    {
                        Console.WriteLine("The goblin slashes your throat and you bleed out on the ground.\n" +
                                          "(press enter to continue)");
                        Console.ReadLine();
                        hero.Location = "lose";
                        return; // exit back to main loop
                    }
                }
                else
                {
                    Console.WriteLine("The goblin falls dead to the ground.");
                }
                
                Console.WriteLine("(press enter to continue)");
                Console.ReadLine();
            }
            
            Console.Clear();
            Console.WriteLine("The goblin lies dead on the ground and you return to your business.");
            Console.WriteLine("You pick up the shiny amulet.");

            if (Program.RollDice(6) >= 3)
            {
                Console.WriteLine("A warm feeling spreads over your body");
                hero.Items.Add("blessedamulet");
            }
            else
            {
                Console.WriteLine("A cold shiver runs down your spine.");
                hero.Items.Add("cursedamulet");
            }
        }
        Console.WriteLine("You leave the corpse and continue into the next room. (press enter to continue)");
        Console.ReadLine();
        
        hero.Location = "backoutside";
    }

    public static void BackOutside(Hero hero)
    {
        Console.WriteLine("You exit the room and find yourself face to face with a fearsome minotaur. \n" +
                          "It charges toward you and you are forced to defend yourself. (press enter to continue)");
        Console.ReadLine();
        hero.Location = "bossfight";
    }

    public static void BossFight(Hero hero, Monster minotaur)
    {
        hero.Defending = false; // reset defending flag
        Console.WriteLine("The minotaur is preparing to charge at you. What do you do?\n" +
                          "Attack it? Defend yourself? Try to talk it out?");
        List<string> answerList = ["attack", "defend", "talk"];
        // if (hero.Items.Contains("cursedamulet")) answerList.Add("use cursed amulet");
        // if (hero.Items.Contains("blessedamulet")) answerList.Add("use blessed amulet");
        
        switch (Program.MakeDecision(answerList))
        {
            case "attack":
                minotaur.TakeDamage(hero.GetDamage());
                Console.WriteLine("You swing your sword at the minotaur and draw blood.");
                if (hero.Items.Contains("blessedamulet"))
                {
                    Console.WriteLine("The blessed amulet fills you with strength and you deal additional damage.");
                    minotaur.TakeDamage(15);
                }
                if (hero.Items.Contains("cursedamulet"))
                {
                    Console.WriteLine("The cursed amulet fills you with uncertainty. \n" +
                                      "You can't tell if it helped you or not.");
                    minotaur.TakeDamage(new Random().Next(-15, 16));
                }
                break;
            
            case "defend":
                Console.WriteLine("You brace yourself for impact. The minotaur charges toward you.");
                hero.Defending = true;
                break;
            
            case "talk":
                Console.WriteLine("You attempt to convince the fearsome creature to stand down.");
                if (Program.RollDice(10) == 10)
                {
                    Console.WriteLine("Somehow how the creature understands you. It lays down its weapon and lets you pass.");
                    hero.Location = "win";
                    break;
                }
                Console.WriteLine("The minotaur ignores your arguments and charges toward you.");
                break;
            
        }

        // check win or lose state //
        if (minotaur.Defeated()) // slew it with weapon
        {
            Console.WriteLine("You have slain the minotaur. Its body lays before you and you are free to exit the dungeon. \n" +
                              "(press enter to continue)");
            hero.Location = "win";
        }
        else if (hero.Location == "win") // if you talked it down
        {
            Console.WriteLine("You are free to exit the dungeon. \n" +
                              "(press enter to continue)");
        }
        else
        {
            minotaur.MakeAttack(hero);
            if (minotaur.Defeated()) // runs into wall
            {
                Console.WriteLine("Its horns get stuck in the wall and it is took weak to get them out. You are free to pass. \n" +
                                  "(press enter to continue)");
                hero.Location = "win";
            } 
            else if (hero.Health <= 0) // hero loses
            {
                Console.WriteLine("The minotaur pierces your heart with its horns and your body falls lifeless to the ground.\n" +
                                  "(press enter to continue)");
                hero.Location = "lose";
            }
            else
            {
                Console.WriteLine("The minotaur still stands and readies its next move. (press enter to continue fighting)");
            }
        }

        Console.ReadLine();
    }

    public static void Win(Hero hero)
    {
        Console.WriteLine("Congratulations! You have defeated the monster and escaped the dungeon.\n" +
                          "Fresh air greets your lungs and a whole world is out there to explore!\n" +
                          "(press enter to continue)");
        hero.Location = "gameover";
        Console.ReadLine();
    }

    public static void Lose(Hero hero)
    {
        Console.WriteLine("You fall to the ground as the last spark of life leaves your body and your\n" +
                          "vision goes dark. You died.\n" +
                          "(press enter to continue)");
        hero.Location = "gameover";
        Console.ReadLine();
    }

    public static void GameOver(Hero hero, Monster minotaur)
    {
        if (Program.AskYesOrNo($"Thank you for playing, {hero.Name}! Do you wish to play again?"))
        {
            hero.Reset();
            minotaur.Reset(250, 30);
            return;
        }

        hero.Location = "quit";
        Console.WriteLine("Good bye!");
        Console.ReadLine();
    }
}