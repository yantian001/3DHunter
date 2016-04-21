using UnityEngine;
using System.Collections;

public class RotateSelf : MonoBehaviour {

    public float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, new Vector3(0,1,0), Time.deltaTime * speed);
	}
}
