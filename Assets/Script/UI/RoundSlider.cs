using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoundSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    Vector3 startPos;
    public void OnDrag(PointerEventData eventData)
    {
        // throw new NotImplementedException();
        transform.RotateAround(new Vector3(Screen.width/2,Screen.height/2,0),Vector3.forward,eventData.delta.y/5);
       
        Debug.Log(eventData.delta);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
        Debug.Log(1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }
}
