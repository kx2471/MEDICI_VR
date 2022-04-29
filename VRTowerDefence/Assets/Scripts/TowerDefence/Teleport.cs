using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스 오른쪽 버튼을 누르고있으면 조준하고
// 떼면 그곳으로 이동하고싶다.
public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public Transform hand;
    bool canTeleport;
    public LineRenderer lr;
    void Update()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool isSuccess = false;
        if (canTeleport)
        {
            Ray ray = new Ray(hand.position, hand.forward);
            lr.SetPosition(0, ray.origin);
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 만약 부딪힌 것이 타워라면 조준 성공
                lr.SetPosition(1, hitInfo.point);
                if (hitInfo.transform.CompareTag("Tower"))
                {
                    isSuccess = true;
                }
            }
            else
            {
                // 조준 실패
                lr.SetPosition(1, ray.origin + ray.direction * 100);
            }
        }
        // 마우스 오른쪽 버튼을 누르고있으면 조준하고
        //if (Input.GetButtonDown("Fire2"))
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            canTeleport = true;
            lr.enabled = true;
        }
        // 떼면 그곳으로 이동하고싶다.
        //else if (Input.GetButtonUp("Fire2"))
        else if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            canTeleport = false;
            lr.enabled = false;

            // 만약 조준 성공했다면 그곳으로 이동하고싶다.
            if (isSuccess)
            {
                transform.position = hitInfo.transform.position;
            }
        }

    }
}
