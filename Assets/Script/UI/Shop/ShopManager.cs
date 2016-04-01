using UnityEngine;
using System.Collections;
using System;

public class ShopManager : MonoBehaviour
{

    public ShopItem[] Items;

    public RectTransform theDisplay = null;

    int currentIndex = 0;

    ShopItem currentItem = null;
    // Use this for initialization
    void Start()
    {
        if (theDisplay == null)
        {
            theDisplay = GetComponent<RectTransform>();
        }

        
        UpdateItemDisplay();
    }

    void UpdateItemDisplay()
    {
        currentIndex = Mathf.Abs(currentIndex) % Items.Length;
        currentItem = Items[currentIndex];
        if (currentItem != null)
        {
            CommonUtils.SetChildText(theDisplay, "NameBg/Name", currentItem.Name.ToUpper());
            CommonUtils.SetChildActive(theDisplay, "InfoBg", currentItem.IsGun);
            CommonUtils.SetChildRawImage(theDisplay, "Bg/ProductIcon", currentItem.Icon);
            if (currentItem.IsGun)
            {
                for (int i = 1; i < 4; i++)
                {
                    CommonUtils.SetChildActive(theDisplay, string.Format("InfoBg/Stab/Star{0}", i), currentItem.Stability >= i);
                    CommonUtils.SetChildActive(theDisplay, string.Format("InfoBg/Zoom/Star{0}", i), currentItem.MaxZoom >= i);
                }
                if (Player.CurrentUser.IsGunActived(currentItem.Id))
                {

                    CommonUtils.SetChildActive(theDisplay, "BuyGun", false);
                    if (currentItem.NeedBuySubMat)
                    {
                        CommonUtils.SetChildActive(theDisplay, "Buy", true);
                        CommonUtils.SetChildRawImage(theDisplay, "Buy/PriceBg/ItemIcon", currentItem.MatPackageIcon);
                        CommonUtils.SetChildText(theDisplay, "Buy/PriceBg/Count", string.Format("x {0}", currentItem.SubMatPackageSize));
                        CommonUtils.SetChildText(theDisplay, "Buy/PriceBg/Price", string.Format("${0}", currentItem.SubMatPackagePrice));

                        CommonUtils.SetChildRawImage(theDisplay, "Buy/IHave/ItemIcon", currentItem.MatPackageIcon);
                        CommonUtils.SetChildText(theDisplay, "Buy/IHave/Count", string.Format("x {0}", Player.CurrentUser.GetMaterialCount(currentItem.Id)));


                        CommonUtils.SetChildButtonCallBack(theDisplay, "Buy/Button", OnBuyItem);
                    }
                    else
                    {
                        CommonUtils.SetChildActive(theDisplay, "Buy", false);
                    }
                }
                else
                {
                    CommonUtils.SetChildActive(theDisplay, "Buy", false);
                    CommonUtils.SetChildActive(theDisplay, "BuyGun", true);
                    CommonUtils.SetChildText(theDisplay, "BuyGun/PriceBg/Price", "$"+currentItem.Price.ToString());
                    CommonUtils.SetChildButtonCallBack(theDisplay, "BuyGun/Button", OnBuyGun);
                }
            }
            else
            {
                CommonUtils.SetChildActive(theDisplay, "Buy", true);
                CommonUtils.SetChildActive(theDisplay, "BuyGun", false);
                CommonUtils.SetChildRawImage(theDisplay, "Buy/PriceBg/ItemIcon", currentItem.MatPackageIcon);
                CommonUtils.SetChildText(theDisplay, "Buy/PriceBg/Count", string.Format("x {0}", currentItem.SubMatPackageSize));
                CommonUtils.SetChildText(theDisplay, "Buy/PriceBg/Price", string.Format("${0}", currentItem.SubMatPackagePrice));

                CommonUtils.SetChildRawImage(theDisplay, "Buy/IHave/ItemIcon", currentItem.MatPackageIcon);
                CommonUtils.SetChildText(theDisplay, "Buy/IHave/Count", string.Format("x {0}", Player.CurrentUser.GetMaterialCount(currentItem.Id)));
                CommonUtils.SetChildButtonCallBack(theDisplay, "Buy/Button", OnBuyItem);

            }


        }

    }

    private void OnBuyGunMaterial()
    {
        //  throw new NotImplementedException();
        if (currentItem == null)
            return;
        if(Player.CurrentUser.Money >= currentItem.SubMatPackagePrice)
        {
            Player.CurrentUser.UseMoney(currentItem.SubMatPackagePrice);
            Player.CurrentUser.BuyGunAmmo(currentItem.Id, currentItem.SubMatPackageSize);
            UpdateItemDisplay();
        }
    }

    private void OnBuyGun()
    {
        // throw new NotImplementedException();
        if (currentItem == null)
            return;
        if (Player.CurrentUser.Money >= currentItem.Price)
        {
            Player.CurrentUser.UseMoney(currentItem.Price);
            Player.CurrentUser.BuyGun(currentItem.Id);
            UpdateItemDisplay();
        }
    }

    private void OnBuyItem()
    {
        if (currentItem == null)
            return;
        if (Player.CurrentUser.Money >= currentItem.SubMatPackagePrice)
        {
            Player.CurrentUser.UseMoney(currentItem.SubMatPackagePrice);
            Player.CurrentUser.BuyGunAmmo(currentItem.Id, currentItem.SubMatPackageSize);
            UpdateItemDisplay();
        }
    }

    public void OnPrevClicked()
    {
        currentIndex--;
        UpdateItemDisplay();
    }

    public void OnNextClicked()
    {
        currentIndex++;
        UpdateItemDisplay();
    }
}
