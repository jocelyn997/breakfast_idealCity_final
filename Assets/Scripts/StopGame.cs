using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGame : MonoBehaviour
{
    private bool isStop = true;
    public GameObject option;

    private void Start()
    {
        option.SetActive(false);
    }

    private void Update()
    {
        if (isStop == true)
        {
            if (Input.GetButtonDown("Right Bumper"))
            {
                Time.timeScale = 0;
                isStop = false;
                option.SetActive(true);
            }
        }
        else
        {
            if (Input.GetButtonDown("Right Bumper"))
            {
                Time.timeScale = 1;
                isStop = true;
                option.SetActive(false);
            }
        }
    }
}