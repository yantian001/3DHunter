using UnityEngine;
using System.Collections;

public class LevelData : ScriptableObject
{
    public int Id;

    public int currentLevel = 0;

    public int randomLevel = 0;

    public Objective[] objectives;

    public Objective bossObjectives;

    public Texture2D mainTexture;

    public Texture2D loopTexture;

    public Vector3 bossLocalPosition;
    public Vector3 bossScale;

    public Objective GetCurrentObjective()
    {
        if(currentLevel >=0 && currentLevel < objectives.Length)
        {
            return objectives[currentLevel];
        }
        return null;
    }


}
