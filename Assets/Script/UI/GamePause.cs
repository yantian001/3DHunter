using UnityEngine;
using System.Collections;

public class GamePause : MonoBehaviour
{

    public RectTransform pause;

    private Vector2 tempPosition;



    public void OnPause(LTEvent evt)
    {
        if (pause == null)
            return;
        if (GameValue.staus == GameStatu.InGame || GameValue.staus == GameStatu.Paused)
        {
            pause.anchoredPosition = Vector2.zero;
        }
        LeanTween.addListener((int)Events.GAMECONTINUE, OnContinue);
    }

    public void OnContinue(LTEvent evt)
    {
        LeanTween.removeListener((int)Events.GAMECONTINUE, OnContinue);
        pause.anchoredPosition = tempPosition;
    }


    public void OnEnable()
    {
        LeanTween.addListener((int)Events.GAMEPAUSE, OnPause);

    }

    public void Start()
    {
        if(pause)
        {
            tempPosition = pause.anchoredPosition;
        }
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.GAMEPAUSE, OnPause);

    }
}
