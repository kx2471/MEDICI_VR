using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���콺 ������ ��ư�� ������������ �����ϰ�
// ���� �װ����� �̵��ϰ�ʹ�.
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
                // ���� �ε��� ���� Ÿ����� ���� ����
                lr.SetPosition(1, hitInfo.point);
                if (hitInfo.transform.CompareTag("Tower"))
                {
                    isSuccess = true;
                }
            }
            else
            {
                // ���� ����
                lr.SetPosition(1, ray.origin + ray.direction * 100);
            }
        }
        // ���콺 ������ ��ư�� ������������ �����ϰ�
        //if (Input.GetButtonDown("Fire2"))
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            canTeleport = true;
            lr.enabled = true;
        }
        // ���� �װ����� �̵��ϰ�ʹ�.
        //else if (Input.GetButtonUp("Fire2"))
        else if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            canTeleport = false;
            lr.enabled = false;

            // ���� ���� �����ߴٸ� �װ����� �̵��ϰ�ʹ�.
            if (isSuccess)
            {
                transform.position = hitInfo.transform.position;
            }
        }

    }
}
