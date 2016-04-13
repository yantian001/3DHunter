using System;
using UnityEngine;
using UnityEngine.UI;
public class ProjectionTimeSlider : MonoBehaviour
{

    public GunHanddle gunHandle;

    public Slider slider = null;
    float maxInfrared = 0.0f;

    float remindInfrared = 0.0f;

    bool isInProjection = false;
    // Use this for initialization
    void Start()
    {
        if (gunHandle == null)
        {
            gunHandle = FindObjectOfType<GunHanddle>();
        }
        remindInfrared = maxInfrared = gunHandle.CurrentGun.Infrared;
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        slider.maxValue = maxInfrared;
        slider.value = remindInfrared;
    }

    public void OnEnable()
    {
        LeanTween.addListener((int)Events.PROJECTION, OnProjection);
    }

    private void OnProjection(LTEvent obj)
    {
        //throw new NotImplementedException();
        isInProjection = ConvertUtil.ToBool(obj.data);
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.PROJECTION, OnProjection);
    }



    // Update is called once per frame
    void Update()
    {
        if (isInProjection && GameValue.staus == GameStatu.InGame)
        {

            remindInfrared -= Time.deltaTime;
            slider.value = remindInfrared;
            if (remindInfrared <= 0)
            {
                isInProjection = false;
                LeanTween.dispatchEvent((int)Events.PROJECTIONTIMEOUT);
            }
        }
    }
}
