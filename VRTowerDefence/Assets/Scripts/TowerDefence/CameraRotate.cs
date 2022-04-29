using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 마우스를 움직이면 x,y축의 방향으로 회전하고싶다.
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
        // 사용자가 마우스를 움직이면
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        // x,y축의 방향으로 회전하고싶다.
        rx = Mathf.Clamp(rx, -90, 90);
        // x축의 회전은 -90~90으로 제한하고싶다.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
