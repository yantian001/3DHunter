using UnityEngine;
using System.Collections;
using System;

public class PreviewCamera : MonoBehaviour
{

    public GameObject lookedObject;
    Camera cam;

    bool cameraPreview = false;
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
        // gameObject.SetActive(false);
        CameraRestore();
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        CameraActive();
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
        cam.transform.position = lookedObject.transform.forward * 5 + lookedObject.transform.position + new Vector3(0, 2, 0);

        //lookedObject = GameValue.s_currentObjective.targetObjects;
    }

    private Camera[] cams;
    private bool[] audiolistenerEnabledTemp;
    private bool[] cameraEnabledTemp;

    void CameraActive()
    {
        if (!cameraPreview)
        {
            cams = (Camera[])GameObject.FindObjectsOfType(typeof(Camera));
            audiolistenerEnabledTemp = new bool[cams.Length];
            cameraEnabledTemp = new bool[cams.Length];
            for (int i = 0; i < cams.Length; i++)
            {
                cameraEnabledTemp[i] = cams[i].enabled;

                if (cams[i].gameObject.GetComponent<AudioListener>())
                {
                    audiolistenerEnabledTemp[i] = cams[i].gameObject.GetComponent<AudioListener>().enabled;
                }

                cams[i].enabled = false;
                if (cams[i].gameObject.GetComponent<AudioListener>())
                {
                    cams[i].gameObject.GetComponent<AudioListener>().enabled = false;
                }
            }
            cameraPreview = true;
        }
    }

    public void CameraRestore()
    {
        if (cameraPreview)
        {
            cameraPreview = false;
            //cams = (Camera[])GameObject.FindObjectsOfType(typeof(Camera));
            if (cameraEnabledTemp != null && cams != null)
            {
                if (cams.Length > 0 && cameraEnabledTemp.Length > 0 && cameraEnabledTemp.Length == cams.Length)
                {
                    for (int i = 0; i < cams.Length; i++)
                    {
                        cams[i].enabled = cameraEnabledTemp[i];
                        if (cams[i].gameObject.GetComponent<AudioListener>())
                        {
                            cams[i].gameObject.GetComponent<AudioListener>().enabled = audiolistenerEnabledTemp[i];
                        }
                    }
                }
            }
            cam.enabled = false;
            cam.gameObject.GetComponent<AudioListener>().enabled = false;
            //Debug.Log ("Restore Cameras");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // cam.transform.position = lookedObject.transform.forward * 5 + lookedObject.transform.position + new Vector3(0, 1, 0);
        if (cameraPreview)
        {
            cam.enabled = true;
            cam.GetComponent<AudioListener>().enabled = true;
            cam.transform.LookAt(lookedObject.transform);

        }
    }
}
