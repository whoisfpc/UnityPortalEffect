using UnityEngine;

public class PortalSender : MonoBehaviour {

    public GameObject receiver;
    public GameObject receiverRoom;

    private bool playerOverlapping = false;
    private GameObject enterObj = null;

    void Update()
    {
        if (playerOverlapping && enterObj != null) {
            var currentDot = Vector3.Dot(transform.forward, enterObj.transform.position - transform.position);

            if (currentDot > 0) // only transport the player once he's moved across plane
            {
                // transport him to the equivalent position in the other portal
                // 调整朝向
                float rotDiff = -Quaternion.Angle(transform.rotation, receiver.transform.rotation);
                rotDiff += 180;
                enterObj.transform.Rotate(Vector3.up, rotDiff);

                // 调整位置
                Vector3 positionOffset = enterObj.transform.position - transform.position;
                positionOffset = Quaternion.Euler(0, rotDiff, 0) * positionOffset;
                var newPosition = receiver.transform.position + positionOffset;
                enterObj.transform.position = newPosition;

                playerOverlapping = false;

                if(receiverRoom != null)
                    receiverRoom.SendMessage("EnterRoom");
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOverlapping = true;
            enterObj = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOverlapping = false;
            enterObj = null;
        }
    }
}
