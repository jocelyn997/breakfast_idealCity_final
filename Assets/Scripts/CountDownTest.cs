using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTest : MonoBehaviour

{
    public float totalTime1 = 100;

    public float totalTime2 = 100;
    private float intervalTime = 1;

    public Text CountDown1Text;
    public Text CountDown2Text;

    private void Start()

    {
        CountDown1Text.text = string.Format("{0:D2} : {1:D2}",
             (int)totalTime1 / 60, (int)totalTime1 % 60);
        CountDown2Text.text = string.Format("{0:D2} : {1:D2}",
            (int)totalTime2 / 60, (int)totalTime2 % 60);

        StartCoroutine((string)CountDown1());
    }

    private IEnumerable CountDown1()
    {
        while (totalTime1 > 0)
        {
            yield return new WaitForSeconds(1);
            totalTime1--;
            CountDown1Text.text = string.Format("{0:D2} : {1:D2}",
              (int)totalTime1 / 60, (int)totalTime1 % 60);
        }
    }

    private void Update()
    {
        if (totalTime2 > 0)
        {
            intervalTime -= Time.deltaTime;
            if (intervalTime <= 0)
            {
                intervalTime += 1;
                totalTime2--;
                CountDown2Text.text = string.Format("{0:D2} : {1:D2}",
               (int)totalTime2 / 60, (int)totalTime2 % 60);
            }
        }
    }
}