using UnityEngine;

public class Player : MonoBehaviour
{

    public int Money;
    /// <summary>
    /// 上次played的场景
    /// </summary>
    public int LastPlayedScene
    {
        get
        {
            return PlayerPrefs.GetInt("lastScene", 0);
        }
        set
        {
            SetKeyIntValue("lastScene", value);
        }
    }
    private static Player _instance = null;

    public static Player CurrentUser
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Player>();
                if (_instance == null)
                {
                    GameObject p = new GameObject("PlayerHandler");
                    _instance = p.AddComponent<Player>();
                }
            }
            return _instance;
        }
        private set
        {

        }
    }

    public void SetKeyIntValue(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
        PlayerPrefs.Save();
    }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        if (!PlayerPrefs.HasKey("money"))
        {
            UseMoney(-50);
        }
        Money = PlayerPrefs.GetInt("money", 0);

    }

    public void OnEnable()
    {
        LeanTween.addListener((int)Events.MONEYUSED, OnMoneyUsed);
       
    }

    private void OnGameFinish(LTEvent obj)
    {
        //throw new NotImplementedException();
        if (obj.data != null)
        {
            var record = obj.data as GameRecords;
            if (record != null)
            {
                UseMoney(-GameValue.moneyPerTimeLeft * record.TimeLeft);
                if (record.FinishType == GameFinishType.Completed)
                {
                    SetLevelRecord(record.MapId, record.Level);
                }
            }
        }
    }

    public void SetLevelRecord(int mapid, int level)
    {
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.MONEYUSED, OnMoneyUsed);
        //  LeanTween.removeListener((int)Events.GAMEFINISH, OnGameFinish);
    }

    void OnMoneyUsed(LTEvent evt)
    {
        if (evt.data != null)
        {
            UseMoney(ConvertUtil.ToInt32(evt.data, 0));
        }
    }

    /// <summary>
    /// 使用金钱
    /// </summary>
    /// <param name="moneyUse"></param>
    public void UseMoney(int moneyUse)
    {
        Money -= moneyUse;
        if (Money <= 0)
            Money = 0;
        PlayerPrefs.SetInt("money", Money);
        PlayerPrefs.Save();
        LeanTween.dispatchEvent((int)Events.MONEYCHANGED);
    }
    /// <summary>
    /// 判断金币是否足够
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public bool IsMoneyEnough(int money)
    {
        return Money >= money;
    }
  
}
