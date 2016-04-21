using UnityEngine;
using UnityEngine.UI;
public class TargetText : MonoBehaviour
{

    public Text text;

    public bool needAppendTarget = false;
    // Use this for initialization
    void Start()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
        string strText = "";

        if (text)
        {
            if(needAppendTarget)
            {
                strText = "Target: ";
            }
            strText += GameValue.s_currentObjective.GetObjString();
            text.text = strText;
        }
    }

}
