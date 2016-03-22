using UnityEngine;
using System.Collections;

public class rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LeanTween.rotate(gameObject,transform.localRotation.eulerAngles + new Vector3(0, 359, 0), 0.5f).setRepeat(1111111);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
