namespace TextAdventure;

public class Monster
{
    public int Health;
    public int Strength;
    private string _name;
    public bool Dancing;

    public Monster(string name)
    {
        _name = name;
    }
    
    public void MakeAttack(Hero hero)
    {
        switch (Program.RollDice(3))
        {
            case 1:
                if (_name == "minotaur")
                {
                    Console.WriteLine("The minotaur psychs itself up with a war cry. \n" +
                                      "Somehow the muscles become even larger");
                    Strength += 15;
                }

                if (_name == "goblin")
                {
                    goto case 2;
                }
                break;
            case 2:
                if (_name == "goblin")
                {
                    Dancing = false;
                    Console.WriteLine("The goblin slashes you with its rugged sword.");
                    if (hero.Defending) Console.WriteLine("The goblin slashes your arm. It hurts despite bracing yourself.");
                    hero.TakeDamage(10);
                    break;
                }
                if (_name == "minotaur" && hero.Defending)
                {
                    Console.WriteLine("You manage to move out of the way of the charging beast. \n" +
                                      "It runs into the wall behind you. It looks like it hurt.");
                    TakeDamage(50);
                    break;
                }
                goto case 3;
            case 3:
                if (_name == "minotaur")
                {
                    Console.WriteLine("The minotaur charges at you and strikes you with its horns.");
                    if (hero.Defending)
                    {
                        hero.TakeDamage(Strength - 15);
                        Console.WriteLine("It hurts but bracing yourself helped.");
                        break;
                    }
                    hero.TakeDamage(Strength);
                    Console.WriteLine("The horns strike you in your chest painfully.");
                    break;    
                }

                if (_name == "goblin")
                {
                    Dancing = true;
                    Console.WriteLine("The goblin starts rocking out. It seems difficult to land a hit.");
                    break;
                }

                break;
                
        }
        
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public bool Defeated()
    {
        return Health <= 0;
    }

    public void Reset(int health, int strength)
    {
        Health = health;
        Strength = strength;
    }
}