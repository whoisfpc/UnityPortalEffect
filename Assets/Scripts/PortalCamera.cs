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
    void Update()
    {
        transform.localPosition = otherPortal.transform.InverseTransformPoint(playerCamera.transform.position);
        transform.localRotation = Quaternion.Inverse(otherPortal.transform.rotation) * playerCamera.transform.rotation;
    }
}
