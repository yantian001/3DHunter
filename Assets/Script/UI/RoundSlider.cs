using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoundSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    Vector3 startPos;
    RectTransform rect;
    float tempRotationZ = 0.0f;

    float currentRotationZ = 0f;

    float preRotationZ = 0f;

    public float smooth = 5f;

    public float maxEngle = 360f;

    public float minEngle = 300f;

    float scrollEngle = 60f;

    public Text text;

    /// <summary>
    /// 当前倍数
    /// </summary>
    float aimTime = 1.0f;

    float maxTime = 5.0f;

    public GunHanddle gunHanddle = null;

    public void Start()
    {
        if (rect == null)
        {
            rect = GetComponent<RectTransform>();
            tempRotationZ = rect.rotation.z;
        }

        scrollEngle = maxEngle - minEngle;

        if (gunHanddle == null)
        {
            gunHanddle = FindObjectOfType<GunHanddle>();
            maxTime = gunHanddle.CurrentGun.MaxZoom - 2;
            aimTime = gunHanddle.CurrentGun.CurrentZoom;
            text.text = string.Format("{0:##0.0} X", aimTime);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentRotationZ = eventData.delta.y / smooth;
        float rot = ClampEngle(preRotationZ + currentRotationZ, maxEngle, minEngle);
        currentRotationZ = rot - preRotationZ;
        preRotationZ = rot;
        aimTime -= maxTime  * currentRotationZ / scrollEngle;
        gunHanddle.CurrentGun.CurrentZoom = aimTime;
        text.text = string.Format("{0:##0.0} X", aimTime);
        rect.Rotate(new Vector3(0, 0, currentRotationZ));
    }

    public float ClampEngle(float engle, float max, float min)
    {
        return Mathf.Clamp(engle, min, max);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }

    public void Update()
    {
      //  Debug.Log("2");
    }
}
