namespace TextAdventure;

public class Hero
{
    public string Name = "";
    public int Health = 100;
    public List<string> Items = new List<string>();
    public string Location = "newgame";
    public bool Defending = false;

    public int GetDamage()
    {
        int damage = 0;
        if (Items.Contains("woodensword")) damage = 50;
        if (Items.Contains("knife")) damage = 60;
        if (Items.Contains("shinysword")) damage = 80;
        return damage;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void Reset()
    {
        Name = "";
        Health = 100;
        Items = new List<string>();
        Location = "newgame";
        Defending = false;
    }
}