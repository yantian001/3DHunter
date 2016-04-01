using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelText : MonoBehaviour {

    public Text levelText;
	// Use this for initialization
	void Start () {
        if (levelText)
            levelText.text = GameValue.level.ToString();
	}
	
}
