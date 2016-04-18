using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour
{

    public LevelData[] levels;

    private static ObjectiveManager _instance;

    public static ObjectiveManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<ObjectiveManager>();
                if(_instance == null)
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
        if(_instance == null)
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
        if(scene >= 0 && scene < levels.Length)
        {
            return levels[scene].GetCurrentObjective();
        }
        return null;
    }

    public LevelData GetLevelData(int scene)
    {
        if (scene >= 0 && scene < levels.Length)
            return levels[scene];
        else
            return null;
    }

    public int GetLevelLength()
    {
        return levels.Length;
    }
}
