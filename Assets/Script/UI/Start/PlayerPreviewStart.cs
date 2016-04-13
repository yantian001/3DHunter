using UnityEngine;
using System.Collections;
using System;

public class PlayerPreviewStart : MonoBehaviour
{
    public void Awake()
    {
        LeanTween.addListener((int)Events.PREVIEWSTART, OnPreviewStart);
        gameObject.SetActive(false);
    }

    private void OnPreviewStart(LTEvent obj)
    {
        gameObject.SetActive(true);
    }

    public void OnDestroy()
    {
        LeanTween.removeListener((int)Events.PREVIEWSTART, OnPreviewStart);

    }

}
