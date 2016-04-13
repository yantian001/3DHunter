using UnityEngine;
using UnityEngine.UI;
public class TargetText : MonoBehaviour
{

    public Text text;
    // Use this for initialization
    void Start()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
        if (text)
        {
            text.text = GameValue.s_currentObjective.GetObjString();
        }
    }

}
