using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPSlider : MonoBehaviour
{

    public DamageManager dm = null;

    public Slider hp;

    public bool IsDead { get; private set; }

    // Use this for initialization
    void Start()
    {
        if (!hp)
        {
            hp = GetComponent<Slider>();
        }
        if (hp == null)
            return;
        if (dm == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                dm = player.GetComponent<DamageManager>();
                if (dm)
                {
                    hp.value = hp.maxValue = dm.hp;
                }
            }
        }
    }

    public void OnEnable()
    {
        LeanTween.addListener((int)Events.USEMEDIKIT, OnUseMedikit);
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.USEMEDIKIT, OnUseMedikit);
    }

    void OnUseMedikit(LTEvent evt)
    {
        dm.hp = Mathf.CeilToInt( hp.maxValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == null || dm == null)
        {
            return;
        }
        hp.value = dm.hp;
        if (hp.value <= 0)
        {
            IsDead = true;
        }
        else
            IsDead = false;
    }
}
