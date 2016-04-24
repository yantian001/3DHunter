using UnityEngine;
using System.Collections;

public class LevelData : ScriptableObject
{
    public int Id;

    public int currentLevel
    {
        get
        {
            int level = 0;
            for (int i = 0; i < objectives.Length; i++)
            {
                if (objectives[i] != null && objectives[i].IsFinished)
                {
                    level++;
                }
                else
                {
                    break;
                }
            }
            return level;
        }
        private set
        {

        }
    }

    public int randomLevel = -1;

    public int bossUnlockLevel = 7;
    public Objective[] objectives;

    public Objective bossObjectives;

    public Texture2D mainTexture;

    public Texture2D loopTexture;

    public Vector3 bossLocalPosition;
    public Vector3 bossScale;

    public string sceneName;

    public Objective GetCurrentObjective()
    {
        int level = currentLevel;
        if (level >= 0 && level < objectives.Length)
        {
            return objectives[level];
        }
        return null;
    }

    public void SetRandom(int r)
    {
        randomLevel = r;
    }
    public Objective GetRandomObjective()
    {
        if (randomLevel == -1)
        {
            randomLevel = Random.Range(0, currentLevel);
        }

        return objectives[randomLevel];
    }

    public Objective GetBossObjective()
    {
        return bossObjectives;
    }

    public string GetCurrentLevelString()
    {
        return string.Format("{0}/{1}", currentLevel + 1, objectives.Length);
    }

	public int GetLevelsCount()
	{
		if (objectives == null)
			return 0;
		return objectives.Length;
	}

    public bool IsMainCompleted()
    {
        return currentLevel == objectives.Length;
    }

    public bool IsBossCompleted()
    {
        if (bossObjectives == null)
            return false;
        return bossObjectives.IsFinished;
    }
}
