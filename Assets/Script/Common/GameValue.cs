using UnityEngine;
using System.Collections;

public class GameValue
{

    public static string s_CurrentSceneName = "";

    public static int mapId = -1;
    /// <summary>
    /// current level
    /// </summary>
    public static int level = -1;

    public static string GetMapSceneName()
    {
        if(mapId == 1)
        {
            return "changjing-1";
        }
        else if(mapId== 2)
        {
            return "changjing-2";

        }
        else if (mapId == 3)
        {
            return "changjing-3";
        }
        return "";
    }

    public static int maxLevelPerMap = 40;

    public static int[] MapLevelConfig = new int[] { 40, 40, 40 };

    public static int moneyPerTimeLeft = 5;

    public static GameStatu staus = GameStatu.Init;

    public static bool IsMapLastLevel(int mapid, int levelid)
    {
        if (mapid > MapLevelConfig.Length)
        {
            return true;
        }
        else
        {
            return levelid >= MapLevelConfig[mapid - 1];
        }
    }

    public static int GetMapLevelCount(int mapid)
    {
        if (mapid > MapLevelConfig.Length)
            return 0;
        else
            return MapLevelConfig[mapid-1];
    }
}
