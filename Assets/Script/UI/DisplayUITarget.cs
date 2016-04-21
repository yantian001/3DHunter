using UnityEngine;
using System.Collections;

public class DisplayUITarget : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Animal animal = GetComponent<Animal>();
        Animal target = GameValue.s_currentObjective.targetObjects.GetComponent<Animal>();
        if (animal && target)
        {
            if (animal.Id != target.Id)
                gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
