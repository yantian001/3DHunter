using UnityEngine;

public class Player : MonoBehaviour
{

    public int Money;
    private int _level1Max = 0;
    public int Level1Max
    {
        get
        {
            return _level1Max;
        }
        private set
        {
            if (value > _level1Max)
            {
                _level1Max = value;
                SetKeyIntValue("Level1", _level1Max);
            }
        }
    }

    private int _level2Max = 0;
    public int Level2Max
    {
        get
        {
            return _level2Max;
        }
        private set
        {
            if (value > _level2Max)
            {
                _level2Max = value;
                SetKeyIntValue("Level2", _level2Max);
            }
        }
    }
    private int _level3Max = 0;
    public int Level3Max
    {
        get
        {
            return _level3Max;
        }
        set
        {
            if (value > _level3Max)
            {
                _level3Max = value;
                SetKeyIntValue("Level3", _level3Max);
            }
        }
    }


    public int Gun2Active
    {
        get
        {
            return PlayerPrefs.GetInt("Gun2Active", 0);
        }
        private set
        {
            SetKeyIntValue("Gun2Active", value);
        }
    }

    public int Gun2Ammo
    {
        get
        {
            return PlayerPrefs.GetInt("Gun2Ammo", 0);
        }
        set
        {
            SetKeyIntValue("Gun2Ammo", value);
        }
    }

    public int Gun3Active
    {
        get
        {
            return PlayerPrefs.GetInt("Gun3Active", 0);
        }
        private set
        {
            SetKeyIntValue("Gun3Active", value);
        }
    }

    public int Gun3Ammo
    {
        get
        {
            return PlayerPrefs.GetInt("Gun3Ammo", 0);
        }
        set
        {
            SetKeyIntValue("Gun3Ammo", value);
        }
    }

    public int Medikit
    {
        get
        {
            return PlayerPrefs.GetInt("Medikit", 2);
        }
        set
        {
            SetKeyIntValue("Medikit", value);
        }
    }

    public int BombCount
    {
        get
        {
            return PlayerPrefs.GetInt("Bomb", 0);
        }
        set
        {
            SetKeyIntValue("Bomb", value);
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
        if(!PlayerPrefs.HasKey("money"))
        {
            UseMoney(-500);
        }
        if(!PlayerPrefs.HasKey("Medikit"))
        {
            Medikit = 2;
        }
        Money = PlayerPrefs.GetInt("money", 0);
        Level1Max = PlayerPrefs.GetInt("Level1", 0);
        Level2Max = PlayerPrefs.GetInt("Level2", 0);
        Level3Max = PlayerPrefs.GetInt("Level3", 0);

    }

    public void OnEnable()
    {
        LeanTween.addListener((int)Events.MONEYUSED, OnMoneyUsed);
        LeanTween.addListener((int)Events.GAMEFINISH, OnGameFinish);
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
        if (mapid == 1)
        {
            Level1Max = level;
        }
        else if (mapid == 2)
        {
            Level2Max = level;
        }
        else if (mapid == 3)
        {
            Level3Max = level;
        }
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.MONEYUSED, OnMoneyUsed);
        LeanTween.removeListener((int)Events.GAMEFINISH, OnGameFinish);
    }

    void OnMoneyUsed(LTEvent evt)
    {
        if (evt.data != null)
        {
            UseMoney(ConvertUtil.ToInt32(evt.data, 0));
        }
    }

    public void UseMoney(int moneyUse)
    {
        Money -= moneyUse;
        PlayerPrefs.SetInt("money", Money);
        PlayerPrefs.Save();
        LeanTween.dispatchEvent((int)Events.MONEYCHANGED);
    }

    public bool IsLevelUnlocked(int mapId, int j)
    {
        if (mapId == 1)
        { return j <= Level1Max + 1; }
        else if (mapId == 2)
            return j <= Level2Max + 1;
        else if (mapId == 3)
            return j <= Level3Max + 1;
        return false;
    }

    public int GetSceneCurrentLevel(int scene)
    {
        if (scene == 1)
        {
            return GetLevel(scene, Level1Max);
        }
        else if (scene == 2)
        {
            return GetLevel(scene, Level2Max);
        }
        else if (scene == 3)
        {
            return GetLevel(scene, Level3Max);
        }
        return 0;
    }

    int GetLevel(int scene, int current)
    {
        if (current + 1 > GameValue.GetMapLevelCount(scene))
            return GameValue.GetMapLevelCount(scene);
        else
            return current + 1;
    }

    public bool IsGunActived(int gunid)
    {

        if (gunid == 1)
            return true;
        else if (gunid == 2)
            return Gun2Active == 1;
        else if (gunid == 3)
            return Gun3Active == 1;
        return false;
           
    }

    public int GetMaterialCount(int id)
    {
        if(id == 101)
        {
            return Medikit;
        }
        else if(id == 102)
        {
            return BombCount;
        }
        else if(id == 1)
        {
            return 0;
        }
        else if(id == 2)
        {
            return Gun2Ammo;
        }
        else if(id == 3)
        {
            return Gun3Ammo;
        }
        return 0;
    }

    public void BuyGunAmmo(int gunid , int count)
    {
        if(gunid == 2)
        {
            Gun2Ammo += count;
        }
        else if(gunid == 3)
        {
            Gun3Ammo += count;
        }
        else if(gunid == 101)
        {
            Medikit += count;
        }
        else if(gunid == 102)
        {
            BombCount += count;
        }
    }

    public void BuyGun(int gunid)
    {
        if(gunid == 2)
        {
            Gun2Active = 1;
            Gun2Ammo = 10;
        }
        else if(gunid == 3)
        {
            Gun3Active = 1;
            Gun3Ammo = 10;
        }
    }
}
