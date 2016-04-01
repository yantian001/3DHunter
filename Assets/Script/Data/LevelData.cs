using UnityEngine;
using System.Collections;

public class LevelData : ScriptableObject
{
    public int Id;

    public int currentLevel = 0;

    public int randomLevel = 0;

    public Objective[] objectives;

    public Objective bossObjectives;
}
