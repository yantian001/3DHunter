﻿using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{

    private int targetKilled = 0;

    bool enemyCleared = false;
    public GameStatu gameStatu = GameStatu.Init;


    public void Awake()
    {
        //监听死亡
        LeanTween.addListener((int)Events.ENEMYDIE, OnEnemyDie);
        LeanTween.addListener((int)Events.ENEMYCLEARED, OnEnemyCleared);
        LeanTween.addListener((int)Events.GAMEPAUSE, OnPause);
        LeanTween.addListener((int)Events.PREVIEWSTART, OnPreviewStart);
        Time.timeScale = 1;
    }

    private void OnPreviewStart(LTEvent obj)
    {
        //throw new NotImplementedException();
        ChangeGameStatu(GameStatu.InGame);
    }

    public void OnDestroy()
    {
        LeanTween.removeListener((int)Events.ENEMYDIE, OnEnemyDie);
        LeanTween.removeListener((int)Events.ENEMYCLEARED, OnEnemyCleared);
        LeanTween.removeListener((int)Events.GAMEPAUSE, OnPause);
        LeanTween.removeListener((int)Events.PREVIEWSTART, OnPreviewStart);
    }

    void OnPause(LTEvent evt)
    {

        if (gameStatu == GameStatu.InGame)
        {
            ChangeGameStatu(GameStatu.Paused);
        }
        LeanTween.addListener((int)Events.GAMECONTINUE, OnContinue);
        Time.timeScale = 0;
    }

    void OnContinue(LTEvent evt)
    {
        LeanTween.removeListener((int)Events.GAMECONTINUE, OnContinue);
        ChangeGameStatu(GameStatu.InGame);
        Time.timeScale = 1;
    }

    private void OnEnemyCleared(LTEvent obj)
    {
        //throw new NotImplementedException();
        enemyCleared = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInGame())
        {
            if (targetKilled >= GameValue.s_currentObjective.objectiveCount)
            {
                ChangeGameStatu(GameStatu.Completed);
                OnGameFinish(true);
            }
            else if (enemyCleared && targetKilled <= GameValue.s_currentObjective.objectiveCount)
            {
                ChangeGameStatu(GameStatu.Failed);
                OnGameFinish(false);
            }
        }
    }

    private void OnGameFinish(bool v)
    {
        //throw new NotImplementedException();
        Debug.Log("Game Finish :" + v.ToString());
        if (v)
        {
            GameValue.s_currentObjective.IsFinished = true;
        }
        LeanTween.delayedCall(2f, () => { LeanTween.dispatchEvent((int)Events.GAMEFINISH, v); });

    }

    public bool IsInGame()
    {
        return gameStatu == GameStatu.InGame;
    }

    void ChangeGameStatu(GameStatu statu)
    {
        gameStatu = statu;
        GameValue.staus = statu;
        Debug.Log("Game Statu : " + gameStatu.ToString());
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
