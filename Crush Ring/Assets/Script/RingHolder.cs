using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHolder : MonoBehaviour
{
    public SubRingHolder GetSubRingHolder()
    {
        if (gameObject.transform.childCount == 1)
        {
            return gameObject.transform.GetChild(0).gameObject.GetComponent<SubRingHolder>();
        }
        else
        {
            return null;
        }
    }
}
