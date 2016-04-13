using UnityEngine;
using System.Collections;

public class AnimalProjection : MonoBehaviour
{

    public Material[] projectionMat;
    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public Material[] normalMat;

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


        normalMat = skinMeshRender.sharedMaterials;

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
        if (isInProjection && projectionMat != null)
        {
            skinMeshRender.sharedMaterials = projectionMat;

        }
        else
        {

            skinMeshRender.sharedMaterials = normalMat;
        }

    }
}
