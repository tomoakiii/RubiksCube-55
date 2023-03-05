using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public GameObject CameraTarget;
    [SerializeField, Range(1f, 50f)] private float wheelSpeed = 100f;
    [SerializeField, Range(0.1f, 0.5f)] private float rotateSpeed = 1f;
    private Vector3 preMousePosition;
    
    private void Start()
    {
        LookCameraTarget();
    }

    private void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if(scrollWheel != 0.0f)
        {
            MouseWheel(scrollWheel);
        }
 
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            preMousePosition = Input.mousePosition;
        }
        MouseDrag(Input.mousePosition);
 
        return;
    }

    private void MouseWheel(float delta)//前進/後退
    {
        transform.position += transform.forward * delta * wheelSpeed;
        return;
    }

    private void MouseDrag(Vector3 mousePosition)
    {
        Vector3 diff = mousePosition - preMousePosition;
        if (diff.magnitude < Vector3.kEpsilon)
        {
            return;
        }
        float d = distance();
        if (Input.GetMouseButton(1))//回転移動
        {
            transform.Translate(-diff * Time.deltaTime * rotateSpeed * d);
            LookCameraTarget();
            transform.position += transform.forward * ((transform.position - CameraTarget.transform.position).magnitude - d);//直線移動と曲線移動の誤差修正
        }
        preMousePosition = mousePosition;
        return;
    }

    private float distance()
    {
        return (transform.position - CameraTarget.transform.position).magnitude;
    }
 
    private void LookCameraTarget()
    {
        transform.LookAt(CameraTarget.transform);
        //this.transform.LookAt(new Vector3(0,0,0));
        //this.transform.rotation = CameraTarget.transform.rotation + new Quaternion.Euler(145, 45, 45);
        return;
    }
}
