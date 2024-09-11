namespace TextAdventure;

public class Monster
{
    private int _health;
    private int _strength;

    public Monster(int health, int strength)
    {
        _health = health;
        _strength = strength;
    }
    
    public void MakeAttack(Hero hero)
    {
        switch (Program.RollDice(3))
        {
            case 1:
                Console.WriteLine("The minotaur psychs itself up with a war cry. \n" +
                                  "Somehow the muscles become even larger");
                _strength += 15;
                break;
            case 2:
                if (hero.Defending)
                {
                    Console.WriteLine("You manage to move out of the way of the charging beast. \n" +
                                      "It runs into the wall behind you. It looks like it hurt.");
                    TakeDamage(50);
                    break;
                }
                goto case 3;
            case 3: 
                Console.WriteLine("The minotaur charges at you and strikes you with its horns.");
                if (hero.Defending)
                {
                    hero.TakeDamage(_strength - 15);
                    Console.WriteLine("It hurts but bracing yourself helped.");
                    break;
                }
                hero.TakeDamage(_strength);
                Console.WriteLine("The horns strike you in your chest painfully.");
                break;    
                
        }
        
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public bool Defeated()
    {
        return _health <= 0;
    }
}