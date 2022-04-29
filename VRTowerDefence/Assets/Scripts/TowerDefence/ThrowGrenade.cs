using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오른쪽 컨트롤러의 Hand버튼을 눌렀을 때
// 핸드의 반경 0.5M에 폭탄이 있다면 폭탄을 생성해서 손에 잡고싶다.
// 오른쪽 컨트롤러의 Hand버튼을 떼면
// 잡고있던 물체를 놓고싶다. 단, 컨트롤러의 속도/회전속도를 물체에 반영해주고싶다.
// 폭탄은 어딘가 부딪히면 범위폭발을 하고싶다. 반경 5M내의 Enemy를 파괴하고싶다.
public class ThrowGrenade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public float radius = 0.5f;
    public Transform hand;
    GameObject grenade;
    public float kAdujstForce = 1;
    void Update()
    {
        // 폭탄을 쥐고있다.
        if (grenade != null)
        {
            // 오른쪽 컨트롤러의 Hand버튼을 떼면
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                // 잡고있던 물체를 놓고싶다. 단, 컨트롤러의 속도/회전속도를 물체에 반영해주고싶다.
                grenade.transform.parent = null;
                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = OVRInput.GetLocalControllerVelocity( OVRInput.Controller.RTouch) * kAdujstForce;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);

                grenade = null;

            }

        }
        // 손이 비었다.
        else
        {
            // 오른쪽 컨트롤러의 Hand버튼을 눌렀을 때
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                // 핸드의 반경 0.5M에 폭탄이 있다면 폭탄을 생성해서 손에 잡고싶다.
                Collider[] cols = Physics.OverlapSphere(hand.position, radius);
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].gameObject.name.Contains("Grenade"))
                    {
                        grenade = Instantiate(cols[i].gameObject);
                        grenade.GetComponent<Rigidbody>().isKinematic = true;
                        grenade.transform.parent = hand;
                        break;
                    }
                }
            }
        }

    }
}
