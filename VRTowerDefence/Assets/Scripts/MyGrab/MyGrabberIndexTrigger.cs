using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 만약 인덱스트리거를 누르면 반경 0.5M안의 충돌체들을 가져오고싶다.
// 그 충돌체 중에 잡을 수 있는 물체가 있다면 잡고싶다.
// 그렇지않고 인덱스트리거를 떼면 
// 잡고있는 물체가 있다면 놓고싶다.
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
        // 만약 잡은물체가 없다면
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

            // 만약 인덱스트리거를 누르면 
            if (isHit && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, hand))
            {
                MyGrabbable mg = hitInfo.transform.GetComponent<MyGrabbable>();
                if (mg)
                {
                    // 문제발생!!
                    // 물체를 잡았을때 반대쪽손이 잡고있던 물체라면 그 손에게 놓으라고 해야함!!
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

            // Button.One을 누르면 잡고있는 물체의 TODO기능을 호출하고싶다.
            if (OVRInput.GetDown(OVRInput.Button.One, hand))
            {
                MyGrabbable mg = grabObject.GetComponent<MyGrabbable>();
                if (mg)
                    mg.TODO();
            }
            // Button.Two를 누르면 잡고있는 물체의 Restore기능을 호출하고싶다.
            if (OVRInput.GetDown(OVRInput.Button.Two, hand))
            {
                MyGrabbable mg = grabObject.GetComponent<MyGrabbable>();
                if (mg)
                    mg.Restore();
            }

            // 그렇지않고 인덱스트리거를 떼면 
            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, hand))
            {
                // 잡고있는 물체가 있다면 놓고싶다.
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
        // 기억하고있던 grabObject를 잊어야한다..
        grabObject = null;
        lr.enabled = true;
    }
}
