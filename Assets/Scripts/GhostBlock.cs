using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlock : MonoBehaviour
{
    private GameObject parent;
    private TetrisBlock parentTetris;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(RepositionBlock());
    }

    public void SetParent(GameObject _parent)
    {
        parent = _parent;
        parentTetris = parent.GetComponent<TetrisBlock>();
    }

    private void PositionGhost()
    {
        transform.position = parent.transform.position;
        transform.rotation = parent.transform.rotation;
    }

    private IEnumerator RepositionBlock()
    {
        while (parentTetris.enabled)
        {
            PositionGhost();
            //MOVE DOWNWARDS
            MoveDown();
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
        yield return null;
    }

    private void MoveDown()
    {
        while (CheckVaildMove())
        {
            transform.position += Vector3.down;
        }
        if (!CheckVaildMove())
        {
            transform.position += Vector3.up;
        }
    }

    private bool CheckVaildMove()
    {
        foreach (Transform child in transform)
        {
            Vector3 pos = Playfield.instance.Round(child.position);
            if (!Playfield.instance.CheckInsideGrid(pos))
            {
                return false;
            }
        }

        foreach (Transform child in transform)
        {
            Vector3 pos = Playfield.instance.Round(child.position);
            Transform t = Playfield.instance.GetTransformOnGridPos(pos);

            if (t != null && t.parent == parent.transform)
            {
                return true;
            }
            if (t != null && t.parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}