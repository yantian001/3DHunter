using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnItemConfig
{
    public int level;

    public int count;
}

public class SpawnItem : MonoBehaviour {

    public int beginLevel = 1;
    /// <summary>
    /// 是否多个人物
    /// </summary>
    public bool isMuti = false;

    
    public SpawnItemConfig[] itemConfigs;
}
