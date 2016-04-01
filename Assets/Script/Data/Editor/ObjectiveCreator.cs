using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ObjectiveCreator
{
#if UNITY_EDITOR
    [MenuItem("3DHunter/Create ObjectiveData")]
    public static void Execute()
    {
        Objective data = ScriptableObject.CreateInstance("Objective") as Objective;
        AssetDatabase.CreateAsset(data, "Assets/Resources/Levels/Level1-1.asset");
        AssetDatabase.Refresh();
    }
#endif
}
