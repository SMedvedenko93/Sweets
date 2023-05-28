using System.Collections.Generic;

public class Level
{
    public int enemy;
    public int tutorial;
    public string enemyOrient;
    public float enemyPositionX;
    public float enemyPositionZ;
    public float trapPositionX;
    public float trapPositionZ;
    public List<string> enemyPath;
    public List<Character> characterList;
    public List<Prop> propList;
    public List<Hint> hints;
}
