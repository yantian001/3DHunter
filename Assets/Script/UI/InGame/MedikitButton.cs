using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MedikitButton : MonoBehaviour
{

    public Button btn;

    RectTransform theRect;
    // Use this for initialization
    void Start()
    {
        if (!btn)
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(OnButtonClick);
        }
        theRect = GetComponent<RectTransform>();
        UpdateCountDisplay();
    }
    
    void OnButtonClick()
    {
        if(Player.CurrentUser.Medikit > 0)
        {
            Player.CurrentUser.BuyGunAmmo(101, -1);
            LeanTween.dispatchEvent((int)Events.USEMEDIKIT);
            UpdateCountDisplay();
        }
    }

    void UpdateCountDisplay()
    {
        CommonUtils.SetChildText(theRect, "Count", Player.CurrentUser.Medikit.ToString());

    }
}
