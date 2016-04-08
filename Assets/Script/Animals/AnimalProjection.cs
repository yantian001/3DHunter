using UnityEngine;
using System.Collections;

public class AnimalProjection : MonoBehaviour
{

    public Material projectionMat;

    public Material normalMat;

    public SkinnedMeshRenderer skinMeshRender;

    // Use this for initialization
    void Start()
    {
        if (skinMeshRender == null)
        {
            skinMeshRender = GetComponent<SkinnedMeshRenderer>();
            if (skinMeshRender)
            {
                Debug.LogError("miss SkinMeshRender");
            }
        }

        if (normalMat == null)
        {
            normalMat = skinMeshRender.sharedMaterial;
        }
    }

    public void Awake()
    {
        LeanTween.addListener((int)Events.PROJECTION, OnProjection);
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.PROJECTION, OnProjection);
    }

    void OnProjection(LTEvent evt)
    {
        if (skinMeshRender == null)
            return;

        bool isInProjection = ConvertUtil.ToBool(evt.data);
        if (isInProjection && projectionMat)
        {
            skinMeshRender.sharedMaterial = projectionMat;
           
        }
        else
        {
            skinMeshRender.sharedMaterial = normalMat;
        }
        
    }
}
