using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{


    public AS_ActionCamera ActionCamera;
    /// <summary>
    /// 
    /// </summary>
    public Spwaner spwaner = null;

    public TimeController timer;

    public HPSlider hp;

    public GameStatu statu { get; private set; }

    public AudioClip successAudio;

    public AudioClip failAudio;

    GameRecords record = null;
    // Use this for initialization
    void Start()
    {
        record = new GameRecords();
        record.Level = GameValue.level;
        record.MapId = GameValue.mapId;
        if (ActionCamera == null)
        {
            ActionCamera = FindObjectOfType(typeof(AS_ActionCamera)) as AS_ActionCamera;
        }
        if (hp == null)
        {
            hp = FindObjectOfType(typeof(HPSlider)) as HPSlider;
        }

        if (spwaner == null)
        {
            var sp = GameObject.FindGameObjectWithTag("Spawner");
            if (sp)
            {
                spwaner = sp.GetComponent<Spwaner>();
                if (spwaner == null)
                { Debug.LogError("dont found spawner "); }
            }
        }

        if (timer == null)
        {
            timer = FindObjectOfType(typeof(TimeController)) as TimeController;
        }

        ChangeGameStatu(GameStatu.InGame);
    }

    public void OnEnable()
    {
        LeanTween.addListener((int)Events.TIMEUP, OnTimeUp);
        LeanTween.addListener((int)Events.ENEMYDIE, OnEnemyDie);
        LeanTween.addListener((int)Events.GAMEPAUSE, OnPause);
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.TIMEUP, OnTimeUp);
        LeanTween.removeListener((int)Events.ENEMYDIE, OnEnemyDie);
        LeanTween.removeListener((int)Events.GAMEPAUSE, OnPause);

    }


    // Update is called once per frame
    void Update()
    {
        //GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        ////启用特效镜头
        //if (ActionCamera != null)
        //{
        //    if (enemys.Length <= 1)
        //    {
        //        ActionCamera.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        ActionCamera.gameObject.SetActive(false);
        //    }
        //}

        //if (enemys == null || enemys.Length <= 0)
        //{
        //    if (!ActionCamera.InAction)
        //        Debug.Log("Success");
        //}
        if (statu == GameStatu.InGame)
        {
            CheckGameStatus();
            CheckActionCamera();
        }
    }
    /// <summary>
    /// 检查游戏状态
    /// </summary>
    void CheckGameStatus()
    {
        //检查敌人是否杀完
        if (spwaner.GetEnemyCount() <= 0)
        {
            ChangeGameStatu(GameStatu.Completed);
            OnGameCompleted();
        }
        else if (hp.IsDead)
        {
            OnPlayerDie();
        }
    }

    void CheckActionCamera()
    {
        //启用特效镜头
        if (ActionCamera != null)
        {
            if (spwaner.GetEnemyCount() <= 1)
            {
                ActionCamera.gameObject.SetActive(true);
            }
            else
            {
                ActionCamera.gameObject.SetActive(false);
            }
        }

    }

    /// <summary>
    /// 更改游戏状态
    /// </summary>
    /// <param name="s"></param>
    void ChangeGameStatu(GameStatu s)
    {
        statu = s;
        GameValue.staus = statu;
        if (statu == GameStatu.Paused || statu == GameStatu.Failed || statu == GameStatu.Completed)
        {
            BahaviorGlobalVariables.SetVariableValue("InGame", false);
        }
        else
        {
            BahaviorGlobalVariables.SetVariableValue("InGame", true);
        }
    }

    void OnEnemyDie(LTEvent evt)
    {
        if (evt.data != null)
        {
            var edi = evt.data as EnemyDeadInfo;
            if (edi.headShot)
                record.HeadShotCount += 1;
            else
                record.EnemyKills += 1;
        }
    }

    void OnTimeUp(LTEvent evt)
    {
        Debug.Log("Time Up!");
        ChangeGameStatu(GameStatu.Failed);
        record.FinishType = GameFinishType.TimeUp;
        if (failAudio != null)
            LeanAudio.play(failAudio);
        GameFinish();
    }

    /// <summary>
    /// 游戏完成
    /// </summary>
    void OnGameCompleted()
    {
        Debug.Log("Level Completed!");
        ChangeGameStatu(GameStatu.Completed);
        record.FinishType = GameFinishType.Completed;
        if (successAudio != null)
            LeanAudio.play(successAudio);
        GameFinish();

    }

    void OnPlayerDie()
    {
        // Debug.Log("Player Die!");
        ChangeGameStatu(GameStatu.Failed);
        record.FinishType = GameFinishType.Failed;
        // timer.SetTimeLeft(0);
        if (failAudio != null)
            LeanAudio.play(failAudio);
        GameFinish();
    }

    void GameFinish()
    {
        if (timer != null)
        {
            if (record.FinishType != GameFinishType.Failed)
                record.TimeLeft = timer.GetTimeLeft();
            else
                record.TimeLeft = 0;
        }
        LeanTween.dispatchEvent((int)Events.GAMEFINISH, record);
        Invoke("DisplayAds", 0.5f);
    }

    void DisplayAds()
    {
        ChartboostUtil.Instance.ShowInterstitialOnDefault();
    }

    void OnPause(LTEvent evt)
    {
        if (statu == GameStatu.InGame)
        {
            ChangeGameStatu(GameStatu.Paused);
        }

        LeanTween.addListener((int)Events.GAMECONTINUE, OnContinue);
        Invoke("DisplayAds", 0.5f);
        // Time.timeScale = 0;
    }

    void OnContinue(LTEvent evt)
    {
        LeanTween.removeListener((int)Events.GAMECONTINUE, OnContinue);
        ChangeGameStatu(GameStatu.InGame);
        //  Time.timeScale = 1;
    }
}
