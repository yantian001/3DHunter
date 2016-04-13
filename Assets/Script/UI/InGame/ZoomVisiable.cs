using UnityEngine;
using System.Collections;

public class ZoomVisiable : MonoBehaviour
{
    public bool invent = false;
    public void Awake()
    {
        LeanTween.addListener((int)Events.ZOOM, OnZoom);
        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        LeanTween.removeListener((int)Events.ZOOM, OnZoom);
    }

   
    public void OnZoom(LTEvent evt)
    {
        bool zoom = ConvertUtil.ToBool(evt.data);
        if (invent)
        {
            zoom = !zoom;
        }
        gameObject.SetActive(zoom);
    }

}
