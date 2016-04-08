using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class CameraProjection : MonoBehaviour
{

    public ColorCorrectionRamp cameraGray;

    public Camera main;

    public Camera animalCamera;

    public Camera animalInternalCamera;

    public LayerMask projectionMask;

    int normalMask;
    // Use this for initialization
    void Start()
    {
        if (cameraGray == null)
        {
            cameraGray = GetComponent<ColorCorrectionRamp>();
            if (cameraGray == null)
            {
                cameraGray = gameObject.AddComponent<ColorCorrectionRamp>();
            }
        }
        normalMask = main.cullingMask;
        SetComponentEnable(false);
    }


    void SetComponentEnable(bool enable)
    {
        if (cameraGray)
        {
            cameraGray.enabled = enable;
        }
        if (animalCamera)
        {
            animalCamera.gameObject.SetActive( enable);
        }
        if (animalInternalCamera)
        {
            animalInternalCamera.gameObject.SetActive(enable);
        }

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
        if (isInProjection == true)
        {
            main.cullingMask = projectionMask;
        }
        else
        {
            main.cullingMask = normalMask;
        }
        SetComponentEnable(isInProjection);
       
        cameraGray.enabled = isInProjection;
    }
}
