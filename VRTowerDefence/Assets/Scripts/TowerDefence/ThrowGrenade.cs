using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ��Ʈ�ѷ��� Hand��ư�� ������ ��
// �ڵ��� �ݰ� 0.5M�� ��ź�� �ִٸ� ��ź�� �����ؼ� �տ� ���ʹ�.
// ������ ��Ʈ�ѷ��� Hand��ư�� ����
// ����ִ� ��ü�� ����ʹ�. ��, ��Ʈ�ѷ��� �ӵ�/ȸ���ӵ��� ��ü�� �ݿ����ְ�ʹ�.
// ��ź�� ��� �ε����� ���������� �ϰ�ʹ�. �ݰ� 5M���� Enemy�� �ı��ϰ�ʹ�.
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
        // ��ź�� ����ִ�.
        if (grenade != null)
        {
            // ������ ��Ʈ�ѷ��� Hand��ư�� ����
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                // ����ִ� ��ü�� ����ʹ�. ��, ��Ʈ�ѷ��� �ӵ�/ȸ���ӵ��� ��ü�� �ݿ����ְ�ʹ�.
                grenade.transform.parent = null;
                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.velocity = OVRInput.GetLocalControllerVelocity( OVRInput.Controller.RTouch) * kAdujstForce;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);

                grenade = null;

            }

        }
        // ���� �����.
        else
        {
            // ������ ��Ʈ�ѷ��� Hand��ư�� ������ ��
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                // �ڵ��� �ݰ� 0.5M�� ��ź�� �ִٸ� ��ź�� �����ؼ� �տ� ���ʹ�.
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
