using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ڰ� ���콺�� �����̸� x,y���� �������� ȸ���ϰ�ʹ�.
public class CameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float rx, ry;
    float rotSpeed = 200;
    // Update is called once per frame
    void Update()
    {
        // ����ڰ� ���콺�� �����̸�
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        // x,y���� �������� ȸ���ϰ�ʹ�.
        rx = Mathf.Clamp(rx, -90, 90);
        // x���� ȸ���� -90~90���� �����ϰ�ʹ�.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
