using UnityEngine;
using System.Collections;

public class TestChangematerial : MonoBehaviour {

    public Material other;

    public Material defaultMaterial;
    float time = 0;
    int changecount = 0;
    // Use this for initialization
    SkinnedMeshRenderer mr;
    void Start () {
        mr = GetComponent<SkinnedMeshRenderer>();
        defaultMaterial = mr.material;
       
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > 2)
        {
            time -= 2;
            changecount++;
            if(changecount % 2 == 1)
            {
                mr.material = other;
            }
            else
            {
                mr.material = defaultMaterial;
            }
        }
	}
}
