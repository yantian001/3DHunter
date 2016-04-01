using UnityEngine;
using System.Collections;

public class Objective : ScriptableObject
{
    /// <summary>
    /// 任务类型
    /// </summary>
    [Tooltip("任务类型")]
    public ObjectiveType objectiveType;
    /// <summary>
    /// 动物类型
    /// </summary>
    [Tooltip("动物类型")]
    public AnimalType animalType;
    /// <summary>
    /// 任务数量
    /// </summary>
    [Tooltip("任务数量")]
    public int objectiveCount = 1;
    /// <summary>
    /// 动物警觉时长
    /// </summary>
    [Tooltip("警觉时长")]
    public float vigilanceTime = 20f;
    /// <summary>
    /// 目标产生的地点ID
    /// </summary>
    public int targetPositionId;
    /// <summary>
    /// 任务目标对象
    /// </summary>
    [Tooltip("任务目标对象")]
    public GameObject[] targetObjects;
    /// <summary>
    /// 任务目标产生数量
    /// </summary>
    [Tooltip("任务目标产生数量")]
    public int targetSpwanCount = 1;
    /// <summary>
    /// 目标血量
    /// </summary>
    [Tooltip("目标血量")]
    public int targetHP = 50;
    /// <summary>
    /// 任务奖励
    /// </summary>
    [Tooltip("任务奖励")]
    public float reward = 0;

    /// <summary>
    /// 非任务目标对象
    /// </summary>
    [Tooltip("其他对象")]
    public GameObject[] otherObjects;
    /// <summary>
    /// 非任务目标对象产生数量
    /// </summary>
    [Tooltip("非任务目标对象产生数量")]
    public int otherSpwanCount = 0;
    /// <summary>
    /// 武器火力要求
    /// </summary>
    [Tooltip(" 武器火力要求")]
    public float powerRequired = -1f;
    /// <summary>
    /// 武器最大视距要求
    /// </summary>
    [Tooltip("武器最大视距要求")]
    public float maxZoomRequired = -1f;
    /// <summary>
    /// 武器稳定性要求
    /// </summary>
    [Tooltip("武器稳定性要求")]
    public float stabilityRequired = -1f;
    /// <summary>
    /// 武器弹夹数量要求
    /// </summary>
    [Tooltip("武器弹夹数量要求")]
    public float capacityRequired = -1f;
    
}
