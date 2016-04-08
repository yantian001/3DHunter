using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class CameraProjection : MonoBehaviour
{

    public ColorCorrectionRamp cameraGray;

    

    // Use this for initialization
    void Start()
    {
        if(cameraGray == null)
        {
            cameraGray = GetComponent<ColorCorrectionRamp>();
            if(cameraGray == null)
            {
                cameraGray = gameObject.AddComponent<ColorCorrectionRamp>();
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
