using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    //public Button ButtonMap;
    //public Button ButtonShop;
    public Button ButtonPlay;

    public ButtonIndex[] buttons;

    //public RectTransform ZoneMap;
    //public RectTransform ZoneShop;

    public Texture2D ButtonSelectTexture;
    public Texture2D ButtonNormalTexture;

    public int currentSelect = 0;

    Button currentButton;
    ButtonIndex currentBtnIndex;
    RectTransform currentZone;
    // Use this for initialization
    void Start()
    {
        if (buttons == null||buttons.Length==0)
            buttons = FindObjectsOfType(typeof(ButtonIndex)) as ButtonIndex[];

        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonIndex bi = buttons[i];
            if (bi != null)
            {
                if (bi.relateZone != null)
                {
                    bi.relateZone.anchoredPosition = new Vector2(Screen.width*2f, 0) + bi.relateZone.anchoredPosition;
                    var btn = bi.GetComponent<Button>();
                    if (btn != null)
                    {
                        btn.onClick.AddListener(() => { OnButtonClicked(btn.GetComponent<ButtonIndex>()); });
                        //btn.onClick.AddListener()
                    }
                }
            }
            if (bi.order == currentSelect)
                currentBtnIndex = bi;
        }
        ChangeSelect();

        if(ButtonPlay)
        {
            ButtonPlay.onClick.AddListener(OnPlayClicked);
        }

        Invoke("DisplayAds", 0.5f);

    }

    void DisplayAds()
    {
        ChartboostUtil.Instance.ShowInterstitialOnHomescreen();
    }
    void OnPlayClicked()
    {
        if (GameValue.mapId == -1)
            GameValue.mapId = 1;
        if(GameValue.level == -1)
        {
            GameValue.level = Player.CurrentUser.GetSceneCurrentLevel(GameValue.mapId);
        }
        GameValue.s_CurrentSceneName = GameValue.GetMapSceneName();
        LeanTween.dispatchEvent((int)Events.GAMESTART,true);
    }

    void OnButtonClicked(ButtonIndex bix)
    {
        if (currentBtnIndex == bix)
            return;
        currentBtnIndex = bix;
        ChangeSelect();
    }

    void ChangeSelect()
    {
        if (currentBtnIndex == null)
            return;
        currentSelect = currentBtnIndex.order;
        if(currentButton != null)
        {
            ChangeButtonTexture(currentButton, ButtonNormalTexture);
        }
        currentButton = currentBtnIndex.GetComponent<Button>();
        if(currentButton!= null)
        {
            ChangeButtonTexture(currentButton, ButtonSelectTexture);
        }
        if(currentZone)
        {
            LeanTween.moveX(currentZone, currentZone.anchoredPosition.x - Screen.width*2f,0.5f);
            currentZone = currentBtnIndex.relateZone;
            LeanTween.moveX(currentZone, 0, 0.5f);
        }
        else
        {
            currentZone = currentBtnIndex.relateZone;
            currentZone.anchoredPosition = new Vector2(0, currentZone.anchoredPosition.y);
        }
    }

    void ChangeButtonTexture(Button btn ,Texture2D texture)
    {
        if (btn == null)
            return;
        var raw = btn.GetComponent<RawImage>();
        if (raw)
            raw.texture = texture;
    }

}
