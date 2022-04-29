using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGun : MyGrabbable
{
    LineRenderer lr;
    public Transform firePositon;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(firePositon.position, firePositon.forward);
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

    }

    public GameObject bulletImpactFactory;
    public int maxBulletCount = 20;
    int bulletCount;
    override public void TODO()
    {
        if (bulletCount < maxBulletCount)
        {
            Ray ray = new Ray(firePositon.position, firePositon.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject bi = Instantiate(bulletImpactFactory);
                bi.transform.position = hitInfo.point;
                bi.transform.forward = hitInfo.normal;
                bulletCount++;
            }
        }
    }
    override public void Restore()
    {
        bulletCount = 0;
    }

    override public void Catch(MyGrabberBase whereHand)
    {
        base.Catch(whereHand);
        lr.enabled = true;
    }

    // ³õ¾Ò´Ù.
    override public void Release()
    {
        base.Release();
        lr.enabled = false;
    }
}
