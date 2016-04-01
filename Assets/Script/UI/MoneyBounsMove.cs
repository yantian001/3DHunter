using UnityEngine;
using System.Collections;

public class MoneyBounsMove : MonoBehaviour {

    public float distance = 0;

    public float time = 1f;
	// Use this for initialization
	void Start () {
        LeanTween.moveY(gameObject, transform.position.y + distance, time).setDestroyOnComplete(true);
	}
	
}
