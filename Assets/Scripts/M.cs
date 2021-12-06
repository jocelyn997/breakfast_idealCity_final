using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MICHEL : MonoBehaviour
{
    //变量
    public string Name;

    public int Age;
    public bool isBoy;

    // Start is called before the first frame update
    private void Start()
    {
        MyInfo(Name, Age, isBoy);
        Age = YearOfBurn(Age);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + 1, 0);
        }
    }

    private void MyInfo(string name, int age, bool isBoy)
    {
        if (isBoy)
        {
            Debug.Log("Name:" + name + "Age:" + age + ",He is a boy!");
            Debug.Log("He was born in " + YearOfBurn(age));
        }
        else
        {
            Debug.Log("Name:" + name + "Age" + age + ",She is a girl!");
        }
    }

    private int YearOfBurn(int number)
    {
        int result;
        result = 2019 - number;
        return result;
    }
}