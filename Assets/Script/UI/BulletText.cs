using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletText : MonoBehaviour
{
    public GunHanddle gunHandle;

    public Text bulletText;
    // Use this for initialization
    void Start()
    {
        if (gunHandle == null)
        {
            gunHandle = FindObjectOfType(typeof(GunHanddle)) as GunHanddle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gunHandle == null || bulletText == null || gunHandle.CurrentGun == null)
        {
            return;
        }
        bulletText.text = string.Format("{0}  / {1}", gunHandle.CurrentGun.Clip, gunHandle.CurrentGun.AmmoPack);
    }
}
