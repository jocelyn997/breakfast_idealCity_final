using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZhuanDong : MonoBehaviour
{
    public float turnSpeed1, turnSpeed2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        turnSpeed1 = Input.GetAxis("Left Trigger");
        turnSpeed2 = Input.GetAxis("Right Trigger");

        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed1 * 30 - Vector3.up * Time.deltaTime * turnSpeed2 * 30);
    }
}