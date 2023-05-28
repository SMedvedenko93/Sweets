
static public class Enemy
{
    public static bool CheckOrient(string EnemyOrient, string CharacterOrient)
    {
        switch (EnemyOrient)
        {
            case "left":
                if (CharacterOrient == "right") { return true; } else { return false; }
            case "right":
                if (CharacterOrient == "left") { return true; } else { return false; }
            case "up":
                if (CharacterOrient == "down") { return true; } else { return false; }
            case "down":
                if (CharacterOrient == "up") { return true; } else { return false; }
        }
        return false;
    }
}
