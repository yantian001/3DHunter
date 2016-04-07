using UnityEngine;
using System.Collections;

public class CameraProjection : MonoBehaviour
{

    public CameraFilterPack_Color_GrayScale cameraGray;

    // Use this for initialization
    void Start()
    {
        if(cameraGray == null)
        {
            cameraGray = GetComponent<CameraFilterPack_Color_GrayScale>();
            if(cameraGray == null)
            {
                cameraGray = gameObject.AddComponent<CameraFilterPack_Color_GrayScale>();
            }
        }
        cameraGray.enabled = false;
    }

    public void Awake()
    {
        LeanTween.addListener((int)Events.PROJECTION, OnProjection);
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.PROJECTION, OnProjection);
    }

    void OnProjection(LTEvent evt)
    {
        bool isInProjection = ConvertUtil.ToBool(evt.data);
        cameraGray.enabled = isInProjection;
    }
}
