// namespace Players;


public abstract class Player : IPlayer
{
    public string Name { get; }
    public Player(string name){
        this.Name=name;
    }
}

public class PlayerHuman : Player
{
    public string Name { get; }

    public PlayerHuman(string name) : base(name)
    {
        this.Name = name;
    }

}

public class PlayerBot : Player
{
    public string Name { get; }

    public PlayerBot(string name) : base(name)
    {
        this.Name = name;
    }
    private AiDifficulty _aiDifficulty;
}

