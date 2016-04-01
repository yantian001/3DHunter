using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelDataCreator  {
#if UNITY_EDITOR
    [MenuItem("3DHunter/Create LevelData")]
    public static void Execute()
    {
        LevelData data = ScriptableObject.CreateInstance("LevelData") as LevelData;
        AssetDatabase.CreateAsset(data, "Assets/Resources/Levels/Level1.asset");
        AssetDatabase.Refresh();
    }
#endif
}
