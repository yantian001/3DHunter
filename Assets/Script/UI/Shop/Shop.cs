using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameDataEditor;
using System.Collections.Generic;
public class Shop : MonoBehaviour {

	 

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
	GDEWeaponAttributeData  powerAttr;
	GDEWeaponAttributeData  stabilityAttr; 
	GDEWeaponAttributeData  maxZoomAttr;
	GDEWeaponAttributeData  capacityAttr;
	GDEWeaponAttributeData  infraredAttr ;



	List<string> gunlist;

	int currentAttr = 0;

	int currentGun = 0;

	int MaxGun=0;

	
	GameObject[] weapons;

	Text costTxt;
	// Use this for initialization
	void Start () {

		GDEDataManager.Init("gde_data");
		rect = GetComponent<RectTransform>();
		FunctionBtnsetUp();
 		
		Debug.Log(this.gameObject.GetComponent<Transform>().Find("Q-1"));

		weapons = new GameObject[3];

		weapons[0] = this.gameObject.GetComponent<Transform>().Find("Q-0").gameObject;

		weapons[1] = this.gameObject.GetComponent<Transform>().Find("Q-1").gameObject;

		weapons[2] = this.gameObject.GetComponent<Transform>().Find("Q-2").gameObject;

		 

		GDEDataManager.GetAllDataKeysBySchema("Weapon",out gunlist);


		MaxGun = gunlist.Count;

		updateWeapon(0);

	}

	void updateWeapon(int value)
	{


		Debug.Log(value);


		getWeapon(value);
		readGunData();
		updateBuyBtn();
		for(int i= 0 ; i < 3;i++)
		{
			weapons[i].SetActive(false);
		}
		weapons[value].SetActive(true);
	}

	GDEWeaponData getWeapon(int value)
	{
		if (!GDEDataManager.DataDictionary.TryGetCustom(gunlist[value], out weapon))
		{
			weapon = null;
		}

		return weapon;
	}


	void FunctionBtnsetUp()
	{
		if(!selected)
		{
			selected = CommonUtils.GetChildComponent<RawImage>(rect,"bottom/selected");
		}

		if(!leftBtn)
		{
			leftBtn = CommonUtils.GetChildComponent<Button>(rect,"middle/leftBtn");
			
			leftBtn.onClick.AddListener(delegate(){this.OnClickBtnFunction("leftBtn");});
		}

		if(!rightBtn)
		{
			rightBtn = CommonUtils.GetChildComponent<Button>(rect,"middle/rightBtn");
			
			rightBtn.onClick.AddListener(delegate(){this.OnClickBtnFunction("rightBtn");});
		}

		if(!upgradeBtn)
		{
			upgradeBtn = CommonUtils.GetChildComponent<Button>(rect,"middle/upgradebg/upgradeBtn");
			
			upgradeBtn.onClick.AddListener(delegate(){this.OnClickBtnFunction("upgradeBtn");});
		}

		if(!PowerBtn)
		{
			PowerBtn = CommonUtils.GetChildComponent<Button>(rect,"bottom/PowerBtn");

			PowerBtn.onClick.AddListener(delegate(){this.OnClickBtnFunction("PowerBtn");});
		}

		if(!StabilityBtn)
		{
			StabilityBtn = CommonUtils.GetChildComponent<Button>(rect,"bottom/StabilityBtn");
			StabilityBtn.onClick.AddListener(delegate(){this.OnClickBtnFunction("StabilityBtn");});
		}

		if(!MaxZoomBtn)
		{
			MaxZoomBtn = CommonUtils.GetChildComponent<Button>(rect,"bottom/MaxZoomBtn");
			MaxZoomBtn.onClick.AddListener(delegate(){this.OnClickBtnFunction("MaxZoomBtn");});
		}

		if(!CapacityBtn)
		{
			CapacityBtn = CommonUtils.GetChildComponent<Button>(rect,"bottom/CapacityBtn");
			CapacityBtn.onClick.AddListener(delegate(){this.OnClickBtnFunction("CapacityBtn");});
		}

		if(!BuyBtn)
		{
			BuyBtn = CommonUtils.GetChildComponent<Button>(rect,"middle/task/BuyBtn");
			BuyBtn.onClick.AddListener(delegate(){this.OnClickBtnFunction("BuyBtn");});
		}
 		
		if(!costTxt)
		{
			costTxt = CommonUtils.GetChildComponent<Text>(rect,"middle/upgradebg/Cost/Text");
		}

	}

	void updateBuyBtn()
	{
		if(!BuyBtn) return;

		if(weapon.Owned && !weapon.Equipped)
		{
			Text t = BuyBtn.transform.FindChild("Text").GetComponent<Text>();
			
			t.text = "Equipment";
			
		}
		
		if(weapon.Equipped)
		{
			BuyBtn = CommonUtils.GetChildComponent<Button>(rect,"middle/task/BuyBtn");
			
			Text t = BuyBtn.transform.FindChild("Text").GetComponent<Text>();
			
			t.text = "Equipment";
			
			BuyBtn.interactable = false;
			
		}
	}

	void readGunData()
	{
	
		//设置枪的名字
		CommonUtils.SetChildText(rect,"middle/task/guntitle/name",weapon.Name);

		//设置power

		powerAttr =  weapon.WeaponAttributes[0];

		CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr1/currentvalue").color = Color.white;

		if(currentAttr == 0)
		{
			CommonUtils.SetChildText(rect,"middle/task/attr1/currentvalue",powerAttr.CurrentValue.ToString() + "+" + powerAttr.LevelsInfo[0].IncreaseValue);
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr1/currentvalue").color = Color.green;
			selected.transform.position = PowerBtn.transform.position;
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr1/currentvalue").fontSize = 18;



		}
		else
		{
			CommonUtils.SetChildText(rect,"middle/task/attr1/currentvalue",powerAttr.CurrentValue.ToString());
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr1/currentvalue").fontSize = 24;
		}
 		
		//设置 Stability

		stabilityAttr =  weapon.WeaponAttributes[1];

		CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr2/currentvalue").color = Color.white;

		if(currentAttr == 1)
		{
			CommonUtils.SetChildText(rect,"middle/task/attr2/currentvalue",stabilityAttr.CurrentValue.ToString()+"%"+ "+" + stabilityAttr.LevelsInfo[0].IncreaseValue+"%");
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr2/currentvalue").color = Color.green;
			selected.transform.position = StabilityBtn.transform.position;
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr2/currentvalue").fontSize = 18;
		}
		else
		{
			CommonUtils.SetChildText(rect,"middle/task/attr2/currentvalue",stabilityAttr.CurrentValue.ToString()+"%");
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr2/currentvalue").fontSize = 24;
		}

		 

		//设置 Infrared

		CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr3/value").color = Color.white;

		GDEWeaponAttributeData  infraredAttr =  weapon.WeaponAttributes[2];
		
		CommonUtils.SetChildText(rect,"middle/task/attr3/value",infraredAttr.CurrentValue.ToString());

		//设置 MaxZoom
		
		maxZoomAttr =  weapon.WeaponAttributes[3];

		CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr4/currentvalue").color = Color.white;

		if(currentAttr == 3)
		{	
			Debug.Log(maxZoomAttr.LevelsInfo[0].IncreaseValue);

			CommonUtils.SetChildText(rect,"middle/task/attr4/currentvalue","X"+maxZoomAttr.CurrentValue.ToString()+ "+" + maxZoomAttr.LevelsInfo[0].IncreaseValue);
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr4/currentvalue").color = Color.green;
			selected.transform.position = MaxZoomBtn.transform.position;
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr4/currentvalue").fontSize = 18;
		}
		else
		{
			CommonUtils.SetChildText(rect,"middle/task/attr4/currentvalue","X"+maxZoomAttr.CurrentValue.ToString());
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr4/currentvalue").fontSize = 24;
		}

		//设置 Capacity	

		capacityAttr =  weapon.WeaponAttributes[4];

		CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr5/currentvalue").color = Color.white;

		if(currentAttr == 4)
		{
			CommonUtils.SetChildText(rect,"middle/task/attr5/currentvalue",capacityAttr.CurrentValue.ToString()+ "+" + capacityAttr.LevelsInfo[0].IncreaseValue);
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr5/currentvalue").color = Color.green;
			selected.transform.position = CapacityBtn.transform.position;
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr5/currentvalue").fontSize = 18;
		}
		else
		{
			CommonUtils.SetChildText(rect,"middle/task/attr5/currentvalue",capacityAttr.CurrentValue.ToString());
			CommonUtils.GetChildComponent<Text>(rect,"middle/task/attr5/currentvalue").fontSize = 24;
		}



		costTxt.text =  weapon.WeaponAttributes[currentAttr].LevelsInfo[0].Cost.ToString();

	}


	public void OnClickBtnFunction(string obj)
	{
		switch(obj)
		{
			case "PowerBtn":
			{
				currentAttr = 0;
				 
				readGunData();



			}
				break;
			case "StabilityBtn":
			{
				currentAttr = 1;
				readGunData();

				 
			}
				break;
			case "MaxZoomBtn":
			{
				currentAttr = 3;
				readGunData();

			}
				break;
			case "CapacityBtn":
			{
				 
				currentAttr = 4;
				readGunData();

				 
			}
				break;
			case "upgradeBtn":
			{
				if(weapon.WeaponAttributes[currentAttr].CanUpgrade)
				{
					if(Player.CurrentUser.Money >weapon.WeaponAttributes[currentAttr].LevelsInfo[0].Cost)
					{
						Player.CurrentUser.UseMoney(weapon.WeaponAttributes[currentAttr].LevelsInfo[0].Cost);
						weapon.WeaponAttributes[currentAttr].CurrentValue += weapon.WeaponAttributes[currentAttr].LevelsInfo[0].IncreaseValue;
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

			if(currentGun != 0)
			{
				currentGun=currentGun-1;
				updateWeapon(currentGun);
			}


			//

			break;
		case "rightBtn":
			Debug.Log("rightBtn");

			if(currentGun < MaxGun-1)
			{
				currentGun=currentGun+1;
				updateWeapon(currentGun);
			};

			break;
		}

	}
	public void onBuy()
	{
		if(!weapon.Owned)
		{
			//买
			if(Player.CurrentUser.Money >int.Parse(weapon.Price))
			{
				Player.CurrentUser.UseMoney(int.Parse(weapon.Price));
				weapon.Owned = true;
			}
		}
		else
		{
			//装备

			weapon.Equipped = true;

		}
		updateBuyBtn();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
