using UnityEngine;

public class PortalCamera : MonoBehaviour {

    public GameObject playerCamera; // 主视角摄像机
    public GameObject portal;       // 当前附着的摄像机需要拍摄的portal
    public GameObject otherPortal;  // 需要同步位置的portal

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main.gameObject;
        }
    }

	// Each frame reposition the camera to mimic the players offset from the other portals position
	void Update ()
    {

        var portalPos = portal.transform.position;
        var otherPortalPos = otherPortal.transform.position;
        var playerCameraPos = playerCamera.transform.position;

        var localOffset = playerCameraPos - otherPortalPos;

        float cosForward = VectorMulti(localOffset, otherPortal.transform.forward) / (localOffset.magnitude * otherPortal.transform.forward.magnitude);
        float cosRight = VectorMulti(localOffset, otherPortal.transform.right) / (localOffset.magnitude * otherPortal.transform.right.magnitude);
        float cosUp = VectorMulti(localOffset, otherPortal.transform.up) / (localOffset.magnitude * otherPortal.transform.up.magnitude);


        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(portalPos);
            Debug.Log(otherPortalPos);
        }
        // adjust position of camera
        // var playerOffsetFromPortal = playerCameraPos - otherPortalPos;
        // transform.position = portalPos + playerOffsetFromPortal;
        transform.position = portalPos +
         localOffset.magnitude * cosForward * portal.transform.forward +
         localOffset.magnitude * cosRight * portal.transform.right +
         localOffset.magnitude * cosUp * portal.transform.up;

        // adjust rotation of camera
        /*
        var anglarDifferenceBetweenPortalRotations = Quaternion.Angle(portal.transform.rotation, otherPortal.transform.rotation);
        var portalRotationalDifference = Quaternion.AngleAxis(anglarDifferenceBetweenPortalRotations, Vector3.up);
        var newFacing = portalRotationalDifference * playerCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(newFacing, Vector3.up);
        */
        
        var rotatationOffset = playerCamera.transform.forward - otherPortal.transform.forward;
        float cosForwardRot = VectorMulti(rotatationOffset, otherPortal.transform.forward) / (rotatationOffset.magnitude * otherPortal.transform.forward.magnitude);
        float cosRightRot = VectorMulti(rotatationOffset, otherPortal.transform.right) / (rotatationOffset.magnitude * otherPortal.transform.right.magnitude);
        float cosUpRot = VectorMulti(rotatationOffset, otherPortal.transform.up) / (rotatationOffset.magnitude * otherPortal.transform.up.magnitude);
        
        transform.forward = portal.transform.forward +
         rotatationOffset.magnitude * cosForwardRot * portal.transform.forward +
         rotatationOffset.magnitude * cosRightRot * portal.transform.right +
         rotatationOffset.magnitude * cosUpRot * portal.transform.up;
    }

    float VectorMulti(Vector3 a, Vector3 b){
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }
}
