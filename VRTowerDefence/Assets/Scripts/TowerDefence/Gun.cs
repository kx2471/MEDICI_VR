using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스 왼쪽 버튼을 누르면 hand에서 hand의 앞방향으로 Ray를 쏘고 부딪힌 곳에 총알흔적공장에서 총알흔적을 만들어서 배치하고싶다.
public class Gun : MonoBehaviour
{
    public Transform hand;
    public GameObject bulletImpactFactory;
    public LineRenderer lr;
    public Transform crosshair;
    // 크로스헤어의 크기를 태어날때 기억하고싶다.
    // 살아가면서 거리에 맞게 크기를 자동으로 조정하게 하고싶다.
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
        // 2. hand에서 hand의 앞방향으로 Ray를 만들고
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        // 3. 바라보고 부딪힌 곳이 있다면
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            crosshair.position = hitInfo.point;
            crosshair.forward = hitInfo.normal;
            crosshair.localScale = crosshairOriginScale * hitInfo.distance * kAdjudst;
            // 1. 마우스 왼쪽 버튼을 누르면
            if (Input.GetButtonDown("Fire1"))
            {
                // 4. 총알흔적공장에서 총알흔적을 만들어서 
                GameObject bi = Instantiate(bulletImpactFactory);
                // 5. 배치하고싶다. -> 위치, 회전
                bi.transform.position = hitInfo.point;
                bi.transform.forward = hitInfo.normal;
            }
        }
        else
        {
            // 허공
            lr.SetPosition(1, ray.origin + ray.direction * 100);
            crosshair.position = ray.origin + ray.direction * 100;
            crosshair.forward = -ray.direction;
            crosshair.localScale = crosshairOriginScale * 100 * kAdjudst;
        }

    }
}
