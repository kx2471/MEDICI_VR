using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ڵ�Ʈ���Ÿ� ������ �ݰ� 0.5M���� �浹ü���� ��������ʹ�.
// �� �浹ü �߿� ���� �� �ִ� ��ü�� �ִٸ� ���ʹ�.
// �׷����ʰ� �ڵ�Ʈ���Ÿ� ���� 
// ����ִ� ��ü�� �ִٸ� ����ʹ�.
public class MyGrabber : MyGrabberBase
{
    // Start is called before the first frame update
    void Start()
    {

    }

    GameObject grabObject = null;
    public float radius = 0.5f;
    public OVRInput.Controller hand;
    public float kAdujstForce = 3;
    void Update()
    {
        // ���� ������ü�� ���ٸ�
        if (grabObject == null)
        {
            // ���� �ڵ�Ʈ���Ÿ� ������ �ݰ� 0.5M���� �浹ü���� ��������ʹ�.
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, hand))
            {
                print(hand.ToString());
                // �� �浹ü �߿� ���� �� �ִ� ��ü�� �ִٸ� ���ʹ�.
                Collider[] cols = Physics.OverlapSphere(transform.position, radius);
                for (int i = 0; i < cols.Length; i++)
                {
                    MyGrabbable mg = cols[i].GetComponent<MyGrabbable>();
                    if (mg)
                    {
                        // �����߻�!!
                        // ��ü�� ������� �ݴ��ʼ��� ����ִ� ��ü��� �� �տ��� ������� �ؾ���!!
                        if (mg.hand)
                        {
                            mg.hand.ForgotGrabObject();
                        }
                        grabObject = cols[i].gameObject;
                        grabObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabObject.transform.parent = transform;
                        mg.hand = this;
                        break;
                    }
                }
                // �׷����ʰ� �ڵ�Ʈ���Ÿ� ���� 
                // ����ִ� ��ü�� �ִٸ� ����ʹ�.

            }
        }
        else // else if (grabObject != null)
        {
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, hand))
            {
                grabObject.transform.parent = null;
                Rigidbody rb = grabObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * kAdujstForce;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);

                MyGrabbable mg = grabObject.GetComponent<MyGrabbable>();
                if (mg)
                {
                    mg.Catch(null);
                }

                grabObject = null;
            }
        }
    }

    override public void ForgotGrabObject()
    {
        // ����ϰ��ִ� grabObject�� �ؾ���Ѵ�..
        grabObject = null;
    }
}
