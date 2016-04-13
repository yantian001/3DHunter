﻿public enum Events
{
    NONE,
    /// <summary>
    /// 敌人死亡
    /// </summary>
    ENEMYDIE,
    /// <summary>
    /// 时间到
    /// </summary>
    TIMEUP,
    /// <summary>
    /// 使用了money
    /// </summary>
    MONEYUSED,
    /// <summary>
    /// 金币数量变化
    /// </summary>
    MONEYCHANGED,
    GAMERESTART,
    MAINMENU,
    BACKTOSTART,
    GAMENEXT,
    INTERSTITIALCLOSED,
    GAMEFINISH,
    GAMEPAUSE,
    GAMECONTINUE,
    GAMESTART,
    USEMEDIKIT,
    VIDEOCLOSED,
    GAMEQUIT,
    GAMEMORE,
    GAMERATE,
    /// <summary>
    /// 敌人走光了
    /// </summary>
    ENEMYCLEARED,
    ENEMYAWAY,
    PROJECTION,
    /// <summary>
    /// 打开狙击镜
    /// </summary>
    ZOOM,
}