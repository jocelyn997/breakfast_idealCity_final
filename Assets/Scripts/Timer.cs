using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //-------------------------
    //完成关卡的时间 (单位为秒)
    private float timer_f = 0f;

    private int timer_i = 0;

    public Text timeText;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //时间减少
        timer_f += Time.deltaTime;
        timer_i = (int)timer_f;

        timeText.text = string.Format("{0:D2} : {1:D2}",
  (int)timer_i / 60, (int)timer_i % 60);
    }
}