using UnityEngine;
using System.Collections;
using System;

public class ObjectiveManager : MonoBehaviour
{

    public LevelData[] levels;

    private static ObjectiveManager _instance;

    public static ObjectiveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectiveManager>();
                if (_instance == null)
                {
                    GameObject container = new GameObject("ObjectiveManagerContainer");
                    _instance = container.AddComponent<ObjectiveManager>();
                }

            }
            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            GameValue.s_currentObjective = levels[0].objectives[0];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Objective GetSceneCurrentObjective(int scene)
    {
        LevelData ld = GetLevelData(scene);
        if (ld != null)
        {
            return ld.GetCurrentObjective();
        }
       
        return null;
    }

    public Objective GetSceneLoopObjective(int scene)
    {
        //if (scene >= 0 && scene < levels.Length)
        //{
        //    return levels[scene].GetRandomObjective();
        //}
        return null;
    }

    public Objective GetBossObjective(int scene)
    {
        LevelData ld = GetLevelData(scene);
        if (ld != null)
            return ld.GetBossObjective();
        return null;
    }

    public LevelData GetLevelData(int scene)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].Id == scene)
                return levels[i];
        }
        return null;
    }

    public int GetLevelLength()
    {
        return levels.Length;
    }

    public Objective GetSceneCurrentObjective(int currentScene, int currentLevel)
    {
        //if (currentScene >= 0 && currentScene < levels.Length)
        //{
        //    return levels[currentScene].GetObjective(currentLevel);
        //}
        LevelData ld = GetLevelData(currentScene);
        if (ld != null)
        {
            return ld.GetObjective(currentLevel);
        }
        return null;
    }
}
