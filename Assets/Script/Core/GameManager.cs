using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{

    private int targetKilled = 0;

    bool enemyCleared = false;
    public GameStatu gameStatu = GameStatu.Init;
    // Use this for initialization
    void Start()
    {

    }

    public void Awake()
    {
        //监听死亡
        LeanTween.addListener((int)Events.ENEMYDIE, OnEnemyDie);
        LeanTween.addListener((int)Events.ENEMYCLEARED, OnEnemyCleared);

    }

  

    public void OnDestroy()
    {
        LeanTween.removeListener((int)Events.ENEMYDIE, OnEnemyDie);
        LeanTween.removeListener((int)Events.ENEMYCLEARED, OnEnemyCleared);
    }

    private void OnEnemyCleared(LTEvent obj)
    {
        //throw new NotImplementedException();
        enemyCleared = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetKilled >= GameValue.s_currentObjective.objectiveCount)
        {
            ChangeGameStatu(GameStatu.Completed);
        }
        else if(enemyCleared && targetKilled <= GameValue.s_currentObjective.objectiveCount)
        {
            ChangeGameStatu(GameStatu.Failed);
        }
    }

    

    void ChangeGameStatu(GameStatu statu)
    {
        gameStatu = statu;
        GameValue.staus = statu;
        //if (statu == GameStatu.Paused || statu == GameStatu.Failed || statu == GameStatu.Completed)
        //{
        //    BahaviorGlobalVariables.SetVariableValue("InGame", false);
        //}
        //else
        //{
        //    BahaviorGlobalVariables.SetVariableValue("InGame", true);
        //}
    }

    public void OnEnemyDie(LTEvent evt)
    {
        if (evt.data != null)
        {
            EnemyDeadInfo edi = evt.data as EnemyDeadInfo;
            if (edi != null && edi.animal != null)
            {
                Animal target = GameValue.s_currentObjective.targetObjects.GetComponent<Animal>();
                if (target)
                {
                    if (target.Id == edi.animal.Id)
                    {
                        if (GameValue.s_currentObjective.objectiveType == ObjectiveType.COUNT)
                        {
                            targetKilled += 1;
                        }
                        else if (GameValue.s_currentObjective.objectiveType == ObjectiveType.HEADKILL)
                        {
                            if (edi.hitPos == HitPosition.HEAD)
                                targetKilled += 1;
                        }
                        else if (GameValue.s_currentObjective.objectiveType == ObjectiveType.HEARTKILL)
                        {
                            if (edi.hitPos == HitPosition.HEART)
                                targetKilled += 1;
                        }
                        else if (GameValue.s_currentObjective.objectiveType == ObjectiveType.LUNGKILL)
                        {
                            if (edi.hitPos == HitPosition.LUNG)
                                targetKilled += 1;
                        }
                    }
                }
                else
                {
                    Debug.LogError("Dont find type Animal at targetobjects!");
                }
            }
        }
    }
}
