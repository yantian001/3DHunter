using UnityEngine;
using System.Collections;
using System;

public class ProjectionButton : MonoBehaviour
{

    bool isInProjection;

    bool isZoomBroke = false;
    public void Awake()
    {
        LeanTween.addListener((int)Events.ZOOM, OnZoom);
        LeanTween.addListener((int)Events.PROJECTIONTIMEOUT, OnTimeOut);

    }

    private void OnTimeOut(LTEvent obj)
    {
        // throw new NotImplementedException();
        isInProjection = false;
        isZoomBroke = false;
        Dispatch();
    }

    public void OnDestroy()
    {
        LeanTween.removeListener((int)Events.ZOOM, OnZoom);
        LeanTween.removeListener((int)Events.PROJECTIONTIMEOUT, OnTimeOut);
    }

    void OnZoom(LTEvent evt)
    {
        bool isZoom = ConvertUtil.ToBool(evt.data);
        if (!isZoom)
        {
            if (isInProjection)
            {
                if (!isZoomBroke)
                    isZoomBroke = true;
            }
        }
        else
        {
            if (isInProjection && isZoomBroke)
                isZoomBroke = false;
        }
        Dispatch();
    }


    // Use this for initialization
    public void OnProjectionClick()
    {
        if (isZoomBroke)
        {
            isInProjection = false;
        }
        else
            isInProjection = !isInProjection;
        isZoomBroke = false;
        Dispatch();
    }

    void Dispatch()
    {
        LeanTween.dispatchEvent((int)Events.PROJECTION, isInProjection && !isZoomBroke);
    }
}
