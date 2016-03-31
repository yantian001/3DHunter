using UnityEngine;
using GameDataEditor;
using System.Collections;
using System.Collections.Generic;


public class GDETest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(GameDataEditor.GDEDataManager.Init("gde_data"))
        {
            Debug.Log("init gde success");
            Dictionary<string, object> weapons;
            if(GDEDataManager.GetAllDataBySchema("Weapon", out weapons))
            {
                for(int i=0;i<weapons.Keys.Count;i++)
                {
                    Debug.Log(weapons.Keys);
                }
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
