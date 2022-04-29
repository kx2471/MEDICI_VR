using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ε���Ʈ���Ÿ� ������ �ݰ� 0.5M���� �浹ü���� ��������ʹ�.
// �� �浹ü �߿� ���� �� �ִ� ��ü�� �ִٸ� ���ʹ�.
// �׷����ʰ� �ε���Ʈ���Ÿ� ���� 
// ����ִ� ��ü�� �ִٸ� ����ʹ�.
public class MyGrabberIndexTrigger : MyGrabberBase
{
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
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
            Ray ray = new Ray(transform.position, transform.forward);
            lr.SetPosition(0, ray.origin);
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(ray, out hitInfo);
            if (isHit)
            {
                lr.SetPosition(1, hitInfo.point);
            }
            else
            {
                lr.SetPosition(1, ray.origin + ray.direction * 100);
            }

            // ���� �ε���Ʈ���Ÿ� ������ 
            if (isHit && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, hand))
            {
                MyGrabbable mg = hitInfo.transform.GetComponent<MyGrabbable>();
                if (mg)
                {
                    // �����߻�!!
                    // ��ü�� ������� �ݴ��ʼ��� ����ִ� ��ü��� �� �տ��� ������� �ؾ���!!
                    mg.Release();
                    grabObject = hitInfo.transform.gameObject;
                    grabObject.GetComponent<Rigidbody>().isKinematic = true;
                    grabObject.transform.parent = transform;
                    mg.Catch(this);
                    lr.enabled = false;
                }
            }
        }
        else // else if (grabObject != null)
        {
            grabObject.transform.position = Vector3.Lerp(grabObject.transform.position, transform.position, Time.deltaTime * 10);

            grabObject.transform.rotation = Quaternion.Lerp(grabObject.transform.rotation, transform.rotation, Time.deltaTime * 10);

            // Button.One�� ������ ����ִ� ��ü�� TODO����� ȣ���ϰ�ʹ�.
            if (OVRInput.GetDown(OVRInput.Button.One, hand))
            {
                MyGrabbable mg = grabObject.GetComponent<MyGrabbable>();
                if (mg)
                    mg.TODO();
            }
            // Button.Two�� ������ ����ִ� ��ü�� Restore����� ȣ���ϰ�ʹ�.
            if (OVRInput.GetDown(OVRInput.Button.Two, hand))
            {
                MyGrabbable mg = grabObject.GetComponent<MyGrabbable>();
                if (mg)
                    mg.Restore();
            }

            // �׷����ʰ� �ε���Ʈ���Ÿ� ���� 
            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, hand))
            {
                // ����ִ� ��ü�� �ִٸ� ����ʹ�.
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
                lr.enabled = true;
            }
        }
    }

    override public void ForgotGrabObject()
    {
        // ����ϰ��ִ� grabObject�� �ؾ���Ѵ�..
        grabObject = null;
        lr.enabled = true;
    }
}
