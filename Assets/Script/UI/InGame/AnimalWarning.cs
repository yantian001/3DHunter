using UnityEngine;
using UnityEngine.UI;

public class AnimalWarning : MonoBehaviour
{

    public Image image;

    public float totalTime = 10f;

    float currentTime;

    bool isNotified = false;

    public void Awake()
    {
        LeanTween.addListener((int)Events.FIRED, OnFired);
    }

    public void OnDestroy()
    {
        LeanTween.removeListener((int)Events.FIRED, OnFired);

    }

    void OnFired(LTEvent evt)
    {
        currentTime = totalTime;
    }

    // Use this for initialization
    void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
            image.fillAmount = 0;
        }
        totalTime = GameValue.s_currentObjective.vigilanceTime;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (currentTime < totalTime)
        {
            if (GameValue.staus == GameStatu.InGame)
            {
                currentTime += Time.deltaTime;
                isNotified = false;
            }

        }
        else
        {
            if (!isNotified)
            {
                isNotified = true;
                LeanTween.dispatchEvent((int)Events.ANIMALWARNED);
                LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 0.8f, 0.1f).setOnComplete(() =>
                {
                    LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 1f, 0.1f);
                }).setLoopType(LeanTweenType.pingPong);
            }
        }
        image.fillAmount = currentTime / totalTime;
    }
}
