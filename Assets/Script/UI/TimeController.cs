using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimeController : MonoBehaviour
{

    public int TimeCount = 70;

    public Text TimeText = null;

    int currentTime = 0;
    // Use this for initialization
    void Start()
    {
        currentTime = TimeCount;
        SetTimeText();
        Invoke("StartCountDown", 0.5f);
    }

    void StartCountDown()
    {
        StartCoroutine("TimeCountDown");
    }

    IEnumerator TimeCountDown()
    {
       
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            if (GameValue.staus == GameStatu.InGame)
            {
                currentTime--;
                SetTimeText();
            }
        }
        LeanTween.dispatchEvent((int)Events.TIMEUP);

    }

    void SetTimeText()
    {
        if (TimeText)
        {
            TimeText.text = currentTime.ToString();
        }
    }
    /// <summary>
    ///  获取剩余时间
    /// </summary>
    /// <returns></returns>
    public int GetTimeLeft()
    {
        return currentTime;
    }

    public void SetTimeLeft(int left = 0)
    {
        currentTime = left;
    }
}
