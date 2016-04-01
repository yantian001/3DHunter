using UnityEngine;
using System.Collections;

[System.Serializable]
public class ShopItem  {

    public int Id = 0;

    public string Name;

    public Texture2D Icon;

    public bool IsGun = false;

    [Range(1,3)]
    public int Stability = 1;

    [Range(1, 3)]
    public int MaxZoom = 1;
    /// <summary>
    /// 购买价格
    /// </summary>
    public int Price = 500;

    public bool NeedBuySubMat = false;

    public int SubMatPackageSize = 1;

    public int SubMatPackagePrice = 500;

    public Texture2D MatPackageIcon;
}
