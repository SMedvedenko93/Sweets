using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    private static List<Location> LevelTypeListGlobal;
    private int indexLevelType;
    private int indexLevel;
    string json;

    public List<Location> getLevelList()
    {
        return LevelTypeListGlobal;
    }

    public void Parse()
    {
        TextAsset jsonR = (TextAsset)Resources.Load("Location", typeof(TextAsset));
        string content = jsonR.text;

        List<Location> LevelTypeList = JsonConvert.DeserializeObject<List<Location>>(content);
        indexLevelType = 0;
        indexLevel = 0;

        LevelTypeListGlobal = new List<Location>();

        foreach (Location location in LevelTypeList)
        {
            LevelTypeListGlobal.Add(
                new Location()
                {
                    name = location.name,
                    image = location.image,
                    location = location.location,
                    levelList = new List<Level>(),
                    levelCount = location.levelCount,
                    tutorial = new List<string>(),
                }
            );

            foreach (string step in location.tutorial)
            {
                LevelTypeListGlobal[indexLevelType].tutorial.Add(step);
            }

            indexLevel = 0;

            if (location.levelList.Count > 0)
            {
                foreach (Level level in location.levelList)
                {
                    LevelTypeListGlobal[indexLevelType].levelList.Add(
                        new Level()
                        {
                            enemy = level.enemy,
                            tutorial = level.tutorial,
                            enemyOrient = level.enemyOrient,
                            enemyPositionX = level.enemyPositionX,
                            enemyPositionZ = level.enemyPositionZ,
                            trapPositionX = level.trapPositionX,
                            trapPositionZ = level.trapPositionZ,
                            enemyPath = new List<string>(),
                            characterList = new List<Character>(),
                            propList = new List<Prop>(),
                            hints = new List<Hint>(),
                        }
                    );

                    foreach (string path in level.enemyPath)
                    {
                        LevelTypeListGlobal[indexLevelType].levelList[indexLevel].enemyPath.Add(path);
                    }

                    foreach (Character character in level.characterList)
                    {
                        LevelTypeListGlobal[indexLevelType].levelList[indexLevel].characterList.Add(
                            new Character()
                            {
                                name = character.name,
                                count = character.count,
                            }
                        );
                    }

                    /*
                    foreach (Prop prop in level.propList)
                    {
                        LevelTypeListGlobal[indexLevelType].levelList[indexLevel].propList.Add(
                            new Prop()
                            {
                                name = prop.name,
                                positionX = prop.positionX,
                                positionZ = prop.positionZ,
                            }
                        );
                    }
                    */

                    foreach (Hint hint in level.hints)
                    {
                        LevelTypeListGlobal[indexLevelType].levelList[indexLevel].hints.Add(
                            new Hint()
                            {
                                name = hint.name,
                                positionX = hint.positionX,
                                positionZ = hint.positionZ,
                                orientation = hint.orientation
                            }
                        );
                    }

                    indexLevel++;
                }
            }

            indexLevelType++;
        }
    }
}