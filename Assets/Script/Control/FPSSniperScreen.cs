using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSSniperScreen : MonoBehaviour
{

    private GunHanddle gunHandler;

    public GameObject InScreen;
    public Slider ScreenSlider;

    float currentDelta = 0;

    bool zoomed = false;

    // Use this for initialization
    void Start()
    {
        //gunHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<GunHanddle>();
        gunHandler = FindObjectOfType ( typeof(GunHanddle) ) as GunHanddle;
        if (ScreenSlider != null && gunHandler.CurrentGun != null)
        {
            ScreenSlider.maxValue = gunHandler.CurrentGun.ZoomFOVLists.Length-1;
          
            //ScreenSlider.onValueChanged.AddListener(SliderDelta);
        }
        ScreenSlider.gameObject.SetActive(zoomed);
    }

   public void SliderDelta(float delta)
    {
        int plus = 0;
        plus = delta > currentDelta ? 1 : -1;
        currentDelta = delta;
        gunHandler.CurrentGun.ZoomDelta(plus);
    }

    public void SliderDelta(int delta)
    {
        int plus = 0;
        plus = delta > currentDelta ? 1 : -1;
        currentDelta = delta;
        gunHandler.CurrentGun.ZoomDelta(plus);
    }

    public void OnDisable()
    {
        ScreenSlider.onValueChanged.RemoveListener(SliderDelta);
    }



    // Update is called once per frame
    void Update()
    {
        if (gunHandler == null || gunHandler.CurrentGun == null)
            return;

       
        if (gunHandler.CurrentGun.Zooming)
        {
            if(zoomed == false)
            {
                InScreen.SetActive(true);
                ScreenSlider.maxValue = gunHandler.CurrentGun.ZoomFOVLists.Length - 1;
                if(gunHandler.CurrentGun.ZoomFOVLists.Length > 1)
                {
                    ScreenSlider.gameObject.SetActive(true);
                }
                zoomed = true;
            }
          
            //ScreenSlider.value = 0;
            //currentDelta = 0;
        }
        else
        {
            if(zoomed)
            {
                InScreen.SetActive(false);
                ScreenSlider.gameObject.SetActive(false);

                currentDelta = 0;
                zoomed = false;
            }
           

        }
    }
}
