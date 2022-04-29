using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrabbable : MonoBehaviour
{
    [HideInInspector]
    public MyGrabberBase hand;

    // ��Ҵ�.
    virtual public void Catch(MyGrabberBase whereHand)
    {
        // ���� ���� ����ϰڴ�.
        hand = whereHand;
    }

    // ���Ҵ�.
    virtual public void Release()
    {
        // ���� ����ִ� �վ� ���� �ؾ���
        if (hand)
        {
            hand.ForgotGrabObject();
        }
        hand = null;
    }

    virtual public void TODO()
    {

    }

    virtual public void Restore()
    {

    }


}