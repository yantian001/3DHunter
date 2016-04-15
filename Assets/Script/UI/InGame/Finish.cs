using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{


    RectTransform rect;

    Vector2 tempPos;
    bool succed;

    Button[] rewardButtons;

    public Button btnReturn;
    public Button btnNext;

    // Use this for initialization
    void Start()
    {
        rect = GetComponent<RectTransform>();
        tempPos = rect.anchoredPosition;

        Transform rewardRect = rect.FindChild("Middle/Bg/Reward");
        if (rewardRect)
        {
            rewardButtons = rewardRect.GetComponentsInChildren<Button>();
            if (rewardButtons != null)
            {
                for (int i = 0; i < rewardButtons.Length; i++)
                {
                    rewardButtons[i].onClick.AddListener(OnRewardClicked);
                }
            }
        }

        if (btnReturn == null)
        {
            btnReturn = CommonUtils.GetChildComponent<Button>(rect, "Top/BackButton");
        }
        if (btnNext == null)
        {
            btnNext = CommonUtils.GetChildComponent<Button>(rect, "Bottom/NextButton");
        }

        SetButtonInterable(false);
    }

    void SetButtonInterable(bool b)
    {
        if (btnReturn)
        {
            btnReturn.interactable = b;
        }

        if (btnNext)
        {
            btnNext.interactable = b;
        }
    }

    private void OnRewardClicked()
    {
        // throw new NotImplementedException();
        if (rewardButtons != null)
        {
            for (int i = 0; i < rewardButtons.Length; i++)
            {
                rewardButtons[i].interactable = false;
            }
        }

        float rewardRate = 1f + Random.Range(-0.2f, 0.5f);
        int reward = Mathf.CeilToInt(rewardRate * GameValue.s_currentObjective.reward);
        Debug.Log("get reward " + reward);
        LeanTween.dispatchEvent((int)Events.MONEYUSED, -reward);
        RectTransform rctReward = CommonUtils.GetChild(rect, "Middle/RewardDisplay");
        if (rctReward)
        {
            rctReward.gameObject.SetActive(true);
            CommonUtils.SetChildText(rctReward, "Text", reward > 0 ? " + " + reward.ToString() : reward.ToString());
        }

        SetButtonInterable(true);
    }

    public void OnEnable()
    {
        LeanTween.addListener((int)Events.GAMEFINISH, OnGameFinish);
    }

    private void OnGameFinish(LTEvent obj)
    {
        // throw new NotImplementedException();
        succed = ConvertUtil.ToBool(obj.data);
        string title = "Hunting Success";
        if (!succed)
        {
            title = "Hunting Failed";
        }

        CommonUtils.SetChildText(rect, "Middle/Bg/Title/Text", title);

        //add click handle

        LeanTween.move(rect, Vector3.zero, .5f);
        //CommonUtils.SetChildText(rect, "Middle/Bg/Target/Text")
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.GAMEFINISH, OnGameFinish);

    }



    // Update is called once per frame
    void Update()
    {

    }
}
