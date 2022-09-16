using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRingHolder : MonoBehaviour
{
    private float zcoordinate;
    public bool isEmpty;
    List<SpriteType> spriteTypes=new List<SpriteType>();
    List<Ring> childRings = new List<Ring>();
    Ring child;
    private void Awake()
    {
        isEmpty = true;
    }
    public void MoveRingHolder()
    {
        transform.position = GetMousePositions();
    }
    private Vector3 GetMousePositions()
    {
        zcoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zcoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint); 
    }

    public List<SpriteType> GetChildren()
    {

        if (gameObject.transform.childCount == Constant.ONE_CHILD)
        {
            GetAChild(0);
            spriteTypes.Add(child.gameObject.GetComponent<Ring>().spriteType);
        }
        else if (gameObject.transform.childCount == Constant.TWO_CHILD)
        {
            GetAChild(0);
            spriteTypes.Add(child.gameObject.GetComponent<Ring>().spriteType);
            GetAChild(1);
            spriteTypes.Add(child.gameObject.GetComponent<Ring>().spriteType);
        }
        return spriteTypes;
    }

  

    public List<Ring> SetRingPosition()
    {
        if (gameObject.transform.childCount == Constant.ONE_CHILD)
        {
            GetAChild(0);

            childRings.Add(child);

        }
        else if (gameObject.transform.childCount == Constant.TWO_CHILD)
        {
            GetAChild(0);
            childRings.Add(child);
            GetAChild(1);
            childRings.Add(child);
        }
        isEmpty = true;
        return childRings;
    }

    internal void PlaceHolder()
    {
        gameObject.transform.position = gameObject.GetComponentInParent<RingHolder>().transform.position;
    }

    private void GetAChild(int i)
    {
        child = gameObject.transform.GetChild(i).gameObject.GetComponent<Ring>();
    }


    public void ClearList()
    {
        childRings.Clear();
        spriteTypes.Clear();
    }
}   





