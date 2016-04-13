using UnityEngine;
using System.Collections;
using System;

public class PreviewCamera : MonoBehaviour
{

    public GameObject lookedObject;
    Camera cam;

    public void Awake()
    {
        LeanTween.addListener((int)Events.PREVIEWSTART, OnPreviewStart);
    }

    public void OnDestroy()
    {
        LeanTween.removeListener((int)Events.PREVIEWSTART, OnPreviewStart);
    }

    private void OnPreviewStart(LTEvent obj)
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        // cam.transform.position = lookedObject.transform.forward *2 + lookedObject.transform.position + new Vector3(0,100,0);
        // cam.transform.LookAt(lookedObject.transform);
        Animal[] animals = FindObjectsOfType<Animal>();
        int objId = GameValue.s_currentObjective.GetAnimalId();
        for (int i = 0; i < animals.Length; i++)
        {
            if (animals[i].Id == objId)
            {
                lookedObject = animals[i].gameObject;
                break;
            }
        }
        cam.transform.position = lookedObject.transform.forward * 5 + lookedObject.transform.position + new Vector3(0, 1, 0);

        //lookedObject = GameValue.s_currentObjective.targetObjects;
    }

    // Update is called once per frame
    void Update()
    {
        // cam.transform.position = lookedObject.transform.forward * 5 + lookedObject.transform.position + new Vector3(0, 1, 0);
        cam.transform.LookAt(lookedObject.transform);
    }
}
