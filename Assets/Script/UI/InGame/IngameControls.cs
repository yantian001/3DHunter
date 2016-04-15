using System;
using UnityEngine;

public class IngameControls : MonoBehaviour
{
    Vector3 tempPosition;

    RectTransform rect;
    public void Start()
    {
        rect = GetComponent<RectTransform>();
        tempPosition = rect.anchoredPosition3D;
    }

    public void Awake()
    {
        LeanTween.addListener((int)Events.PREVIEWSTART, OnPreviewStart);
        LeanTween.addListener((int)Events.GAMEFINISH, OnGameFinish);
    }

    private void OnGameFinish(LTEvent obj)
    {
        // throw new NotImplementedException();
        rect.anchoredPosition3D = tempPosition;

    }

    private void OnPreviewStart(LTEvent obj)
    {
        // throw new NotImplementedException();
        rect.anchoredPosition = Vector3.zero;
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.PREVIEWSTART, OnPreviewStart);
        LeanTween.removeListener((int)Events.GAMEFINISH, OnGameFinish);

    }
}
