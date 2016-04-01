using UnityEngine;
using System.Collections;

public class GameFinish : MonoBehaviour
{

    public RectTransform finish;

    public float scaleInTime = 0.5f;

    Vector3 localScale;
    Vector2 tempPosition;

    public void OnEnable()
    {
        LeanTween.addListener((int)Events.GAMEFINISH, OnGameFinish);
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.GAMEFINISH, OnGameFinish);
    }

    public void Start()
    {
        if (finish)
        {
            // finish.transform.position = finish.transform.position + new Vector3(Screen.width, 0f, 0f);
            tempPosition = finish.anchoredPosition;
            //localScale = finish.
        }
    }

    void OnGameFinish(LTEvent evt)
    {
        if (evt.data == null || finish == null)
            return;

        GameRecords record = evt.data as GameRecords;
        if (record == null)
            return;

        SetDisplay(record);

        finish.anchoredPosition = Vector2.zero;
        var transformComplete = finish.FindChild("Complete");
        localScale = transformComplete.localScale;
        transformComplete.localScale = Vector3.zero;
        LeanTween.scale(transformComplete.gameObject, localScale, scaleInTime);
    }

    void SetDisplay(GameRecords record)
    {

        if (record.FinishType == GameFinishType.Completed)
        {
            CommonUtils.SetChildText(finish, "Complete/Title", "YOU WIN");
            CommonUtils.SetChildActive(finish, "Complete/ButtonReplay", false);
        }
        else if (record.FinishType == GameFinishType.Failed)
        {
            CommonUtils.SetChildText(finish, "Complete/Title", "ENEMY WIN");
            CommonUtils.SetChildActive(finish, "Complete/ButtonNext", false);
           // CommonUtils.SetChildActive(finish, "Complete/ButtonShare", false);

        }
        else if (record.FinishType == GameFinishType.TimeUp)
        {
            CommonUtils.SetChildText(finish, "Complete/Title", "TIME UP");
            CommonUtils.SetChildActive(finish, "Complete/ButtonNext", false);
          //  CommonUtils.SetChildActive(finish, "Complete/ButtonShare", false);
        }

        CommonUtils.SetChildText(finish, "Complete/Scores/HeadShotTitle/Count", record.HeadShotCount.ToString());
        CommonUtils.SetChildText(finish, "Complete/Scores/KillEnemy/Count", record.EnemyKills.ToString());
        CommonUtils.SetChildText(finish, "Complete/Scores/TimeLeft/Count", record.TimeLeft.ToString());

    }
}
