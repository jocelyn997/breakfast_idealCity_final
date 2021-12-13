using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    //下落速率
    private float prevTime;

    //数值越大速度越慢-free mode
     private float fallTime = 0.8f;

    // challenge mode
   // private float fallTime = 10f;

    private void Start()
    {
        //游戏结束画面
    }

    private Vector3 nextMove = Vector3.zero;

    private void Update()
    {
        //下落节奏，检查网格剩余空位,/数越大速度越快
        if (Time.time - prevTime > (Input.GetButtonDown("Left Bumper") ? fallTime / 500 : fallTime))
        //  GetButtonDown("Left Bumper")
        {
            transform.position += Vector3.down;

            if (!CheckVaildMove())
            {
                transform.position += Vector3.up;
                //delete layer id possible
                enabled = false;
                //create a new tetris block
                Playfield.instance.SpawnNewBlock();
            }

            //游戏结束
            /*   if (!GameManager.instance.ReadGameIsOver())
               {
                   Playfield.instance.SpawnNewBlock();
               }*/
            else
            {
                //update the grid
                Playfield.instance.UpdatedGrid(this);
            }

            prevTime = Time.time;
        }

        // LEFT RIGHT FORWARD BACK
        if (Input.GetButtonDown("West"))
        {
            SetRotationInput(new Vector3(0, -90, 0));
        }
        if (Input.GetButtonDown("East"))
        {
            SetRotationInput(new Vector3(0, 90, 0));
        }
        if (Input.GetButtonDown("South"))
        {
            SetRotationInput(new Vector3(-90, 0, 0));
        }
        if (Input.GetButtonDown("North"))
        {
            SetRotationInput(new Vector3(90, 0, 0));
        }
        if (Input.GetAxis("Dpad Horizontal") > 0)
        {
            nextMove += Vector3.right * 0.1f;
            //SetInput(Vector3.right * 0.05f);
        }
        if (Input.GetAxis("Dpad Horizontal") < 0)
        {
            nextMove += Vector3.left * 0.1f;
            //SetInput(Vector3.left * 0.05f);
        }
        if (Input.GetAxis("Dpad Vertical") < 0)
        {
            nextMove += Vector3.back * 0.1f;
            //SetInput(Vector3.back * 0.05f);
        }
        if (Input.GetAxis("Dpad Vertical") > 0)
        {
            nextMove += Vector3.forward * 0.1f;
            //SetInput(Vector3.forward * 0.05f);
        }

        float x = nextMove.x;
        float z = nextMove.z;
        if (Mathf.Abs(x) >= 1 || Mathf.Abs(z) >= 1)
        {
            x = Mathf.Round(x);
            z = Mathf.Round(z);
            nextMove = new Vector3(x, 0, z);
            SetInput(nextMove);
            nextMove = Vector3.zero;
        }
        else
        {
            SetInput(Vector3.zero);
        }
    }

    public void SetInput(Vector3 direction)
    {
        transform.position += direction;
        if (!CheckVaildMove())
        {
            transform.position -= direction;
        }
        else
        {
            Playfield.instance.UpdatedGrid(this);
        }
    }

    //rotation
    public void SetRotationInput(Vector3 rotation)
    {
        transform.Rotate(rotation, Space.World);
        if (!CheckVaildMove())
        {
            transform.Rotate(-rotation, Space.World);
        }
        else
        {
            Playfield.instance.UpdatedGrid(this);
        }
    }

    // 是否在网格内部移动，定义了round
    private bool CheckVaildMove()
    {
        foreach (Transform child in transform)
        {
            Vector3 pos = Playfield.instance.Round(child.position);

            //检查是不是在网格内部
            if (!Playfield.instance.CheckInsideGrid(pos))
            {
                return false;
            }
        }

        foreach (Transform child in transform)
        {
            Vector3 pos = Playfield.instance.Round(child.position);
            Transform t = Playfield.instance.GetTransformOnGridPos(pos);
            if (t != null && t.parent != transform)
            {
                return false;
            }
        }

        return true;
    }

    public void SetSpeed()
    {
        fallTime = 0.05f;
    }

    //  public void SetHighSpeed()
    // {
    //    activeTetris.setSpeed();
    //   }
}