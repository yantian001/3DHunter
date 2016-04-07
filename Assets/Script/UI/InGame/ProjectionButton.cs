using UnityEngine;
using System.Collections;

public class ProjectionButton : MonoBehaviour
{

    bool isInProjection;
    // Use this for initialization
    public void OnProjectionClick()
    {
        isInProjection = !isInProjection;
        LeanTween.dispatchEvent((int)Events.PROJECTION, isInProjection);
    }
}
