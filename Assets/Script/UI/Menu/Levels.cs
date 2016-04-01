using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Levels : MonoBehaviour
{

    int currentMapIndex = 0;

    int mapLength = 0;
    [SerializeField]
    public LevelMap[] Maps;

    public GameObject LevelItem;

    public GameObject LevelButtonItem;

    public RectTransform theParent;



    float width = 900f;

    Vector2 positionTemp;

    // Use this for initialization
    void Start()
    {
        if (theParent == null)
        {
            theParent = GetComponent<RectTransform>();

        }
        if (Maps != null)
        {
            mapLength = Maps.Length;
        }
        positionTemp = theParent.anchoredPosition;
        InitLevels();
        if (GameValue.mapId != -1)
        {
            currentMapIndex = GameValue.mapId - 1;
        }

        if (currentMapIndex > 0)
        {
            if (currentMapIndex >= mapLength)
            {
                currentMapIndex = currentMapIndex % mapLength;
            }
        }
        Moveto(currentMapIndex);
    }

    public void Moveto(int index)
    {
        if (index >= 0)
        {
            if (index >= mapLength)
            {
                index = index % mapLength;
            }
            currentMapIndex = index;
            LeanTween.moveX(theParent, positionTemp.x - width * index, 0.5f);
            GameValue.mapId = currentMapIndex + 1;
            GameValue.level = -1;
        }

    }

    public void OnNextClick()
    {
        // LeanTween.moveX(theParent.gameObject, theParent.position.x - 900, 1f);
        //Vector3 moveto = theParent.position + new Vector3(-900, 0, 0);
        //LeanTween.moveX(theParent, theParent.anchoredPosition.x - 900, 0.5f);
        Moveto(currentMapIndex + 1);
    }

    public void OnPrevClick()
    {
        // LeanTween.moveX(theParent.gameObject, theParent.position.x - 900, 1f);
        //Vector3 moveto = theParent.position + new Vector3(-900, 0, 0);
        Moveto(currentMapIndex - 1);
    }


    void InitLevels()
    {
        if (!theParent || Maps == null || !LevelItem || !LevelButtonItem)
            return;
        theParent.DetachChildren();
        // width = LevelItem.GetComponent<RectTransform>().sizeDelta.x;
        var layoutGroup = theParent.GetComponent<HorizontalLayoutGroup>();
        float totalWidth = layoutGroup.spacing * (Maps.Length - 1) + Maps.Length * width + layoutGroup.padding.left + layoutGroup.padding.right;
        theParent.sizeDelta = new Vector2(totalWidth, theParent.sizeDelta.y);
        // theParent.
        for (int i = 0; i < Maps.Length; i++)
        {
            var item = GameObject.Instantiate(LevelItem);
            item.transform.SetParent(theParent);
            item.transform.localScale = Vector3.one;
            var levelZone = item.transform.FindChild("Panel/LevelItems/LevelGrid").GetComponent<RectTransform>();
            CommonUtils.SetChildRawImage(item.GetComponent<RectTransform>(), "MapIcon/Icon", Maps[i].MapIcon);
            CommonUtils.SetChildText(item.GetComponent<RectTransform>(), "MapIcon/Name", Maps[i].MapName);
            if (levelZone)
            {
                int levelCount = GameValue.GetMapLevelCount(Maps[i].MapId);
                levelZone.DetachChildren();
                var gridGroup = levelZone.GetComponent<GridLayoutGroup>();
                int col = Mathf.FloorToInt((levelZone.rect.width - gridGroup.padding.left - gridGroup.padding.right + gridGroup.spacing.x) / (gridGroup.cellSize.x + gridGroup.spacing.x));
                int row = Mathf.CeilToInt((float)levelCount / col);
                //levelZone.rect.Set(levelZone.rect.x,levelZone.rect.y,levelZone.rect.width, row * gridGroup.cellSize.y + gridGroup.padding.top);
                levelZone.sizeDelta = new Vector2(levelZone.sizeDelta.x, row * gridGroup.cellSize.y + gridGroup.padding.top + (row - 1) * gridGroup.spacing.y);
                for (int j = 0; j < levelCount; j++)
                {
                    var levelItem = Instantiate(LevelButtonItem) as GameObject;
                    //levelItem.GetComponent<RectTransform>().SetParent(levelZone);
                    levelItem.transform.SetParent(levelZone);
                    levelItem.transform.localScale = new Vector3(1f, 1f, 1f);
                    CommonUtils.SetChildText(levelItem.GetComponent<RectTransform>(), "Text", (j + 1).ToString());

                    var button = levelItem.GetComponent<Button>();
                    if (button)
                    {
                        bool isUnlocked = Player.CurrentUser.IsLevelUnlocked(Maps[i].MapId, j + 1);
                        button.interactable = isUnlocked;
                        if (isUnlocked)
                        {
                            button.onClick.AddListener(() => { OnLevelItemClicked(button); });
                        }
                    }
                }
                //移动到当前关所在位子
                int levelCurrent = Player.CurrentUser.GetSceneCurrentLevel(Maps[i].MapId);
                if (levelCurrent > 15)
                {
                    int currentRow = Mathf.FloorToInt((float)levelCurrent / col);
                    levelZone.anchoredPosition = new Vector2(levelZone.anchoredPosition.x, (currentRow - 1) * gridGroup.cellSize.y + gridGroup.padding.top + (currentRow - 1) * gridGroup.spacing.y);
                }
            }
        }

    }

    private void OnLevelItemClicked(Button button)
    {
        //throw new NotImplementedException();
        var text = button.GetComponentInChildren<Text>();
        if (text)
        {
            GameValue.level = ConvertUtil.ToInt32(text.text, -1);
            Debug.Log(GameValue.level);
        }
    }
}
