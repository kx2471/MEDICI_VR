using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 만약 핸드트리거를 누르면 반경 0.5M안의 충돌체들을 가져오고싶다.
// 그 충돌체 중에 잡을 수 있는 물체가 있다면 잡고싶다.
// 그렇지않고 핸드트리거를 떼면 
// 잡고있는 물체가 있다면 놓고싶다.
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
        // 만약 잡은물체가 없다면
        if (grabObject == null)
        {
            // 만약 핸드트리거를 누르면 반경 0.5M안의 충돌체들을 가져오고싶다.
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, hand))
            {
                print(hand.ToString());
                // 그 충돌체 중에 잡을 수 있는 물체가 있다면 잡고싶다.
                Collider[] cols = Physics.OverlapSphere(transform.position, radius);
                for (int i = 0; i < cols.Length; i++)
                {
                    MyGrabbable mg = cols[i].GetComponent<MyGrabbable>();
                    if (mg)
                    {
                        // 문제발생!!
                        // 물체를 잡았을때 반대쪽손이 잡고있던 물체라면 그 손에게 놓으라고 해야함!!
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
                // 그렇지않고 핸드트리거를 떼면 
                // 잡고있는 물체가 있다면 놓고싶다.

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
        // 기억하고있던 grabObject를 잊어야한다..
        grabObject = null;
    }
}
