using UnityEngine;
using BehaviorDesigner.Runtime;

public class Menu : MonoBehaviour
{

    int currentScene = -1;

    public Objective currentObjective;
    RectTransform parent;

    public Transform boosTransform = null;
    // Use this for initialization
    void Start()
    {
        parent = GetComponent<RectTransform>();
        //currentScene = Player.CurrentUser.LastPlayedScene;
        UpdateSceneDisplay(Player.CurrentUser.LastPlayedScene);
        if (currentObjective == null)
            OnMainTaskSelected();
    }

    void UpdateSceneDisplay(int scene)
    {
        if (currentScene == scene)
            return;
        int len = ObjectiveManager.Instance.GetLevelLength();
        while (scene < 0 || scene >= len)
        {
            if (scene < 0)
                scene += len;
            if (scene >= len)
                scene -= len;
        }
        LevelData ld = ObjectiveManager.Instance.GetLevelData(scene);
        if (ld == null)
            return;
        currentScene = scene;

        CommonUtils.SetChildRawImage(parent, "Middle/MainTasks/backImage", ld.mainTexture);
        CommonUtils.SetChildRawImage(parent, "Middle/LoopTasks/backImage", ld.loopTexture);
        if (boosTransform)
        {
            boosTransform.DetachChildren();
            if (ld.bossObjectives && ld.bossObjectives.targetObjects)
            {
                var createObj = (GameObject)GameObject.Instantiate(ld.bossObjectives.targetObjects, boosTransform.position, Quaternion.identity);

                createObj.transform.SetParent(boosTransform);
                // createObj.transform.localPosition = Vector3.zero;
                createObj.AddComponent<AutoDestroyByRemove>();
                if(ld.bossLocalPosition != Vector3.zero)
                {
                    createObj.transform.localPosition = ld.bossLocalPosition;

                }
                if(ld.bossScale != Vector3.zero)
                {
                    createObj.transform.localScale = ld.bossScale;

                }
                CommonUtils.SetChildComponentActive<BehaviorTree>(createObj.transform, false);
               
               // CommonUtils.SetChildComponentActive<Rigidbody>(createObj.transform, false);
            }
        }
        OnMainTaskSelected();
    }

    public void OnNextClicked()
    {
        UpdateSceneDisplay(currentScene + 1);
    }

    public void OnPrevClicked()
    {
        UpdateSceneDisplay(currentScene - 1);
    }

    public void OnMainTaskSelected()
    {
        Objective obj = ObjectiveManager.Instance.GetSceneCurrentObjective(currentScene);
        Display(obj);

    }

    void Display(Objective obj)
    {
        if (currentObjective == obj)
            return;
        currentObjective = obj;
        Debug.Log(string.Format("{0} ~ {1}", Mathf.CeilToInt(currentObjective.reward * 0.8f), Mathf.CeilToInt(currentObjective.reward * 1.5f)));
        //显示target
        CommonUtils.SetChildText(parent, "Middle/Bg/TargetTitle/Text", currentObjective.GetObjString());
        CommonUtils.SetChildText(parent, "Middle/Bg/RewardTitle/RewardText", string.Format("{0} ~ {1}", Mathf.CeilToInt(currentObjective.reward * 0.8f), Mathf.CeilToInt(currentObjective.reward * 1.5f)));

        int recommandCount = 0;
        DisplayRecommand("Middle/Bg/Recommand/PowerItem", currentObjective.powerRequired != -1, false, ref recommandCount);
        DisplayRecommand("Middle/Bg/Recommand/MaxZoom", currentObjective.maxZoomRequired != -1, false, ref recommandCount);
        DisplayRecommand("Middle/Bg/Recommand/stability", currentObjective.stabilityRequired != -1, false, ref recommandCount);
        DisplayRecommand("Middle/Bg/Recommand/capacity", currentObjective.capacityRequired != -1, false, ref recommandCount);
    }

    public void DisplayRecommand(string name, bool show, bool isArrive, ref int displayCount)
    {
        if (show)
        {
            CommonUtils.SetChildActive(parent, name, show);
            RectTransform powerRect = CommonUtils.GetChildComponent<RectTransform>(parent, name);
            if (powerRect)
            {
                powerRect.anchoredPosition = new Vector2(displayCount * 40, 0);
            }
            displayCount += 1;

        }
        else
        {
            CommonUtils.SetChildActive(parent, name, show);
        }
    }
}
