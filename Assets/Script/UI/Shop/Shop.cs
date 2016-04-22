using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameDataEditor;
using System.Collections.Generic;
public class Shop : MonoBehaviour
{



    RectTransform rect;

    Button BuyBtn;
    Button PowerBtn;

    RawImage selected;

    Button leftBtn;
    Button rightBtn;

    Button StabilityBtn;
    Button MaxZoomBtn;
    Button CapacityBtn;
    Button upgradeBtn;
    GDEWeaponData weapon;
    GDEWeaponAttributeData powerAttr;
    GDEWeaponAttributeData stabilityAttr;
    GDEWeaponAttributeData maxZoomAttr;
    GDEWeaponAttributeData capacityAttr;
    GDEWeaponAttributeData infraredAttr;



    List<string> gunlist;

    int currentAttr = -1;

    int currentGun = 0;

    int MaxGun = 0;


    GameObject[] weapons;

    Text costTxt;

    Vector3 selectTmpPosition;
    // Use this for initialization
    void Start()
    {


        GDEDataManager.Init("gde_data");
        rect = GetComponent<RectTransform>();
        FunctionBtnsetUp();

        weapons = new GameObject[3];

        weapons[0] = GameObject.Find("Q-0").gameObject;

        weapons[1] = GameObject.Find("Q-1").gameObject;

        weapons[2] = GameObject.Find("Q-2").gameObject;



        GDEDataManager.GetAllDataKeysBySchema("Weapon", out gunlist);


        MaxGun = gunlist.Count;

        updateWeapon(WeaponManager.Instance.GetCurrentWeaponId());

    }

    void updateWeapon(int value)
    {


        //Debug.Log(value);
        while (value < 0 || value >= MaxGun)
        {
            if (value < 0)
                value += MaxGun;
            if (value >= MaxGun)
                value -= MaxGun;
        }
        currentAttr = -1;
        getWeapon(value);
        readGunData();
        updateBuyBtn();
        for (int i = 0; i < 3; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[value].SetActive(true);
    }

    GDEWeaponData getWeapon(int value)
    {
        //if (!GDEDataManager.DataDictionary.TryGetCustom(gunlist[value], out weapon))
        //{
        //    weapon = null;
        //}
        weapon = WeaponManager.Instance.GetWeaponById(value);
        return weapon;
    }


    void FunctionBtnsetUp()
    {
        if (!selected)
        {
            selected = CommonUtils.GetChildComponent<RawImage>(rect, "bottom/selected");
            selectTmpPosition = selected.rectTransform.anchoredPosition;
        }

        if (!leftBtn)
        {
            leftBtn = CommonUtils.GetChildComponent<Button>(rect, "middle/leftBtn");

            leftBtn.onClick.AddListener(delegate () { this.OnClickBtnFunction("leftBtn"); });
        }

        if (!rightBtn)
        {
            rightBtn = CommonUtils.GetChildComponent<Button>(rect, "middle/rightBtn");

            rightBtn.onClick.AddListener(delegate () { this.OnClickBtnFunction("rightBtn"); });
        }

        if (!upgradeBtn)
        {
            upgradeBtn = CommonUtils.GetChildComponent<Button>(rect, "middle/upgradebg/upgradeBtn");

            upgradeBtn.onClick.AddListener(delegate () { this.OnClickBtnFunction("upgradeBtn"); });
        }

        if (!PowerBtn)
        {
            PowerBtn = CommonUtils.GetChildComponent<Button>(rect, "bottom/PowerBtn");

            PowerBtn.onClick.AddListener(delegate () { this.OnClickBtnFunction("PowerBtn"); });
        }

        if (!StabilityBtn)
        {
            StabilityBtn = CommonUtils.GetChildComponent<Button>(rect, "bottom/StabilityBtn");
            StabilityBtn.onClick.AddListener(delegate () { this.OnClickBtnFunction("StabilityBtn"); });
        }

        if (!MaxZoomBtn)
        {
            MaxZoomBtn = CommonUtils.GetChildComponent<Button>(rect, "bottom/MaxZoomBtn");
            MaxZoomBtn.onClick.AddListener(delegate () { this.OnClickBtnFunction("MaxZoomBtn"); });
        }

        if (!CapacityBtn)
        {
            CapacityBtn = CommonUtils.GetChildComponent<Button>(rect, "bottom/CapacityBtn");
            CapacityBtn.onClick.AddListener(delegate () { this.OnClickBtnFunction("CapacityBtn"); });
        }

        if (!BuyBtn)
        {
            BuyBtn = CommonUtils.GetChildComponent<Button>(rect, "middle/task/BuyBtn");
            BuyBtn.onClick.AddListener(delegate () { this.OnClickBtnFunction("BuyBtn"); });
        }

        if (!costTxt)
        {
            costTxt = CommonUtils.GetChildComponent<Text>(rect, "middle/upgradebg/Cost/Text");
        }

    }

    void updateBuyBtn()
    {
        if (!BuyBtn) return;

        if (!weapon.Owned)
        {
            upgradeBtn.gameObject.SetActive(false);
            costTxt.transform.parent.gameObject.SetActive(true);
            costTxt.text = weapon.Price.ToString();
            Text t = BuyBtn.transform.FindChild("Text").GetComponent<Text>();
            t.text = "Buy";
            BuyBtn.gameObject.SetActive(true);

        }
        else if (weapon.Owned && !weapon.Equipped)
        {
            Text t = BuyBtn.transform.FindChild("Text").GetComponent<Text>();
            t.text = "Equipment";
            // BuyBtn.interactable = true;
            BuyBtn.gameObject.SetActive(true);
            costTxt.transform.parent.gameObject.SetActive(false);
            upgradeBtn.gameObject.SetActive(false);
        }
        else if (weapon.Equipped)
        {
            BuyBtn = CommonUtils.GetChildComponent<Button>(rect, "middle/task/BuyBtn");
            Text t = BuyBtn.transform.FindChild("Text").GetComponent<Text>();
            t.text = "Equipment";
            BuyBtn.gameObject.SetActive(false);
            //costTxt.transform.parent.gameObject.SetActive(false);
            costTxt.transform.parent.gameObject.SetActive(false);
            upgradeBtn.gameObject.SetActive(false);
        }
    }

    void readGunData()
    {

        //设置枪的名字
        CommonUtils.SetChildText(rect, "middle/guntitle/name", weapon.Name);

        //设置power

        powerAttr = weapon.GetAttributeById(0);

        CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr1/currentvalue").color = Color.white;
        CommonUtils.SetChildText(rect, "middle/task/attr1/maxValue", string.Format("({0})", powerAttr.MaxValue.ToString()));
        if (currentAttr == 0 && !powerAttr.IsMaxLevel())
        {
            CommonUtils.SetChildText(rect, "middle/task/attr1/currentvalue", powerAttr.CurrentValue.ToString() + "+" + powerAttr.LevelsInfo[0].IncreaseValue);
            CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr1/currentvalue").color = Color.green;
            
         //   CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr1/currentvalue").fontSize = 18;

        }
        else
        {
            CommonUtils.SetChildText(rect, "middle/task/attr1/currentvalue", powerAttr.CurrentValue.ToString());
         //   CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr1/currentvalue").fontSize = 24;
        }

        //设置 Stability

        stabilityAttr = weapon.GetAttributeById(2);

        CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr2/currentvalue").color = Color.white;

        if (currentAttr == 2 && !stabilityAttr.IsMaxLevel())
        {
            CommonUtils.SetChildText(rect, "middle/task/attr2/currentvalue", stabilityAttr.CurrentValue.ToString() + "%" + "+" + stabilityAttr.LevelsInfo[0].IncreaseValue + "%");
            CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr2/currentvalue").color = Color.green;
           
           // CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr2/currentvalue").fontSize = 18;
        }
        else
        {
            CommonUtils.SetChildText(rect, "middle/task/attr2/currentvalue", stabilityAttr.CurrentValue.ToString() + "%");
         //   CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr2/currentvalue").fontSize = 24;
        }



        //设置 Infrared

        CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr3/value").color = Color.white;

        GDEWeaponAttributeData infraredAttr = weapon.GetAttributeById(4);

        CommonUtils.SetChildText(rect, "middle/task/attr3/value", infraredAttr.CurrentValue.ToString());

        //设置 MaxZoom

        maxZoomAttr = weapon.GetAttributeById(1);

        CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr4/currentvalue").color = Color.white;

        if (currentAttr == 1 && !maxZoomAttr.IsMaxLevel())
        {
            Debug.Log(maxZoomAttr.LevelsInfo[0].IncreaseValue);

            CommonUtils.SetChildText(rect, "middle/task/attr4/currentvalue", "X" + maxZoomAttr.CurrentValue.ToString() + "+" + maxZoomAttr.LevelsInfo[0].IncreaseValue);
            CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr4/currentvalue").color = Color.green;
            
         //   CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr4/currentvalue").fontSize = 18;
        }
        else
        {
            CommonUtils.SetChildText(rect, "middle/task/attr4/currentvalue", "X" + maxZoomAttr.CurrentValue.ToString());
           // CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr4/currentvalue").fontSize = 24;
        }

        //设置 Capacity	

        capacityAttr = weapon.GetAttributeById(3);
        CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr5/currentvalue").color = Color.white;

        if (currentAttr == 3 && !capacityAttr.IsMaxLevel())
        {
            CommonUtils.SetChildText(rect, "middle/task/attr5/currentvalue", capacityAttr.CurrentValue.ToString() + "+" + capacityAttr.LevelsInfo[0].IncreaseValue);
            CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr5/currentvalue").color = Color.green;
           
          //  CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr5/currentvalue").fontSize = 18;
        }
        else
        {
            CommonUtils.SetChildText(rect, "middle/task/attr5/currentvalue", capacityAttr.CurrentValue.ToString());
           // CommonUtils.GetChildComponent<Text>(rect, "middle/task/attr5/currentvalue").fontSize = 24;
        }
        if (currentAttr == -1)
        {
            selected.rectTransform.anchoredPosition = selectTmpPosition;
        }
        else if(currentAttr == 0)
        {
            selected.transform.position = PowerBtn.transform.position;
        }
        else if (currentAttr == 1)
        {
            selected.transform.position = MaxZoomBtn.transform.position;
        }
        else if (currentAttr == 2)
        {
            selected.transform.position = StabilityBtn.transform.position;
        }
        else if (currentAttr == 3)
        {
            selected.transform.position = CapacityBtn.transform.position;
        }
        if (currentAttr != -1)
        {
            GDEWeaponAttributeData currentAttrdata = weapon.GetAttributeById(currentAttr);
            if (currentAttrdata != null)
            {
                if (currentAttrdata.IsMaxLevel())
                {
                    upgradeBtn.gameObject.SetActive(false);
                    costTxt.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    upgradeBtn.gameObject.SetActive(true);
                    costTxt.transform.parent.gameObject.SetActive(true);
                    costTxt.text = currentAttrdata.GetUpgradeCost().ToString();
                }
            }

        }



    }


    public void OnClickBtnFunction(string obj)
    {
        switch (obj)
        {
            case "PowerBtn":
                {
                    currentAttr = 0;

                    readGunData();



                }
                break;
            case "StabilityBtn":
                {
                    currentAttr = 2;
                    readGunData();


                }
                break;
            case "MaxZoomBtn":
                {
                    currentAttr = 1;
                    readGunData();

                }
                break;
            case "CapacityBtn":
                {

                    currentAttr = 3;
                    readGunData();


                }
                break;
            case "upgradeBtn":
                {
                    if (weapon.WeaponAttributes[currentAttr].CanUpgrade)
                    {
                        if (Player.CurrentUser.Money >= weapon.GetAttributeById(currentAttr).GetUpgradeCost())
                        {
                            Player.CurrentUser.UseMoney(weapon.GetAttributeById(currentAttr).GetUpgradeCost());
                            //weapon.WeaponAttributes[currentAttr].CurrentValue += weapon.WeaponAttributes[currentAttr].LevelsInfo[0].IncreaseValue;
                            weapon.GetAttributeById(currentAttr).CurrentLevel += 1;
                            readGunData();
                        }
                    }
                }
                break;
            case "BuyBtn":
                {
                    onBuy();
                }
                break;

            case "leftBtn":
                Debug.Log("leftBtn");

                //if (currentGun != 0)
                //{
                    currentGun = currentGun - 1;
                    updateWeapon(currentGun);
                //}


                //

                break;
            case "rightBtn":
                Debug.Log("rightBtn");

                //if (currentGun < MaxGun - 1)
               // {
                    currentGun = currentGun + 1;
                    updateWeapon(currentGun);
               // };

                break;
        }

    }
    public void onBuy()
    {
        if (!weapon.Owned)
        {
            //买
            if (Player.CurrentUser.Money >= ConvertUtil.ToInt32(weapon.Price))
            {
                Player.CurrentUser.UseMoney(ConvertUtil.ToInt32(weapon.Price));
                weapon.Owned = true;
            }
        }
        else
        {
            //装备
            WeaponManager.Instance.EqWeaon(weapon.Id);
            //weapon.Equipped = true;

        }
        updateBuyBtn();
    }


}
