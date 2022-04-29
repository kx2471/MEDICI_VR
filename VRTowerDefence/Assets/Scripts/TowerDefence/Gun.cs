using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���콺 ���� ��ư�� ������ hand���� hand�� �չ������� Ray�� ��� �ε��� ���� �Ѿ��������忡�� �Ѿ������� ���� ��ġ�ϰ�ʹ�.
public class Gun : MonoBehaviour
{
    public Transform hand;
    public GameObject bulletImpactFactory;
    public LineRenderer lr;
    public Transform crosshair;
    // ũ�ν������ ũ�⸦ �¾�� ����ϰ�ʹ�.
    // ��ư��鼭 �Ÿ��� �°� ũ�⸦ �ڵ����� �����ϰ� �ϰ�ʹ�.
    Vector3 crosshairOriginScale;
    public float kAdjudst = 1;
    // Start is called before the first frame update
    void Start()
    {
        crosshairOriginScale = crosshair.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // 2. hand���� hand�� �չ������� Ray�� �����
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        // 3. �ٶ󺸰� �ε��� ���� �ִٸ�
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            crosshair.position = hitInfo.point;
            crosshair.forward = hitInfo.normal;
            crosshair.localScale = crosshairOriginScale * hitInfo.distance * kAdjudst;
            // 1. ���콺 ���� ��ư�� ������
            if (Input.GetButtonDown("Fire1"))
            {
                // 4. �Ѿ��������忡�� �Ѿ������� ���� 
                GameObject bi = Instantiate(bulletImpactFactory);
                // 5. ��ġ�ϰ�ʹ�. -> ��ġ, ȸ��
                bi.transform.position = hitInfo.point;
                bi.transform.forward = hitInfo.normal;
            }
        }
        else
        {
            // ���
            lr.SetPosition(1, ray.origin + ray.direction * 100);
            crosshair.position = ray.origin + ray.direction * 100;
            crosshair.forward = -ray.direction;
            crosshair.localScale = crosshairOriginScale * 100 * kAdjudst;
        }

    }
}
