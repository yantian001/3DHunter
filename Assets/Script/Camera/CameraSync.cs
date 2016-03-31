using UnityEngine;
using System.Collections;

public class CameraSync : MonoBehaviour
{
    public Camera mainCamera;
    public Camera self;
    // Use this for initialization
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = transform.parent.GetComponent<Camera>();
        }
        if (self == null)
        {
            self = GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera && self)
        {
            self.fieldOfView = mainCamera.fieldOfView;
        }
    }
}
