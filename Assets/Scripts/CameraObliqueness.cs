using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraObliqueness : MonoBehaviour {

    public Transform clipTransform;

    public float offset;

    private Camera theCamera;


    private void Start()
    {
        theCamera = GetComponent<Camera>();
    }

    private void OnPreRender()
    {
        if (clipTransform)
        {
            SetObliqueClip(clipTransform);
        }
    }

    private void SetObliqueClip(Transform clipTrans)
    {
        if (!theCamera)
        {
            return;
        }
        var normal = clipTrans.forward;
        Vector4 planeWorldSpace;
        planeWorldSpace.x = normal.x;
        planeWorldSpace.y = normal.y;
        planeWorldSpace.z = normal.z;

        planeWorldSpace.w = -Vector3.Dot(normal, clipTrans.position - normal * offset);
        Vector4 cameraPos;
        cameraPos.x = theCamera.transform.position.x;
        cameraPos.y = theCamera.transform.position.y;
        cameraPos.z = theCamera.transform.position.z;
        cameraPos.w = 1;

        if (Vector4.Dot(planeWorldSpace, cameraPos) >= 0)
        {
            return;
        }
        Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(theCamera.cameraToWorldMatrix) * planeWorldSpace;
        theCamera.projectionMatrix = theCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
    }
}
