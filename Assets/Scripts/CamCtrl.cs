using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    private Transform target;
    private Transform rotTarget;
    private Vector3 lastPos;

    private float sensitivity = 0.25f;

    // Start is called before the first frame update
    private void Awake()
    {
        rotTarget = transform.parent;
        target = rotTarget.transform.parent;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(target);
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
        }
        Orbit();
    }

    private void Orbit()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastPos;
            float angleY = -delta.y * sensitivity;
            float angleX = delta.x * sensitivity;
            //X AXIS
            Vector3 angles = rotTarget.transform.eulerAngles;
            angles.x += angleY;
            angles.x = ClampAngle(angles.x, -85f, 85f);    //top view problems

            rotTarget.transform.eulerAngles = angles;

            //Y AXIS
            target.RotateAround(target.position, Vector3.up, angleX);
            lastPos = Input.mousePosition;
        }
    }

    // top view problems  negitive angle
    private float ClampAngle(float angle, float from, float to)

    {
        if (angle < 0) angle = 360 + angle;

        if (angle > 180f) return Mathf.Max(angle, 360 + from);

        return Mathf.Min(angle, to);
    }
}