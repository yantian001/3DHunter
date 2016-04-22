using UnityEngine;
using UnityEngine.UI;

public class ButtonFlash : MonoBehaviour
{

    public Button btn;

    public float interval = 0.1f;

    public RawImage flashImg;

    float currentInterval = 0;

    // Use this for initialization
    void Start()
    {
        if (btn == null)
        {
            btn = GetComponent<Button>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (btn && btn.interactable)
        {
            currentInterval += Time.deltaTime;
            if (currentInterval >= interval)
            {
                if(flashImg)
                {
                    flashImg.enabled = !flashImg.enabled;

                }
                currentInterval -= interval;
            }
        }
    }
}
