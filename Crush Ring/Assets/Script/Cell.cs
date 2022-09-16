using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell : MonoBehaviour
{
    public List<Ring> rings = new List<Ring>();
    public int cellID;
    
    public bool CanPlaceRing(List<SpriteType> spriteTypes)
    {
        if (rings.FindAll(x => spriteTypes.Contains(x.spriteType)).Count > 0) 
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    internal void SetRing(Ring gameObject)
    {
        rings.Add(gameObject);
    }
}
