using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{
   public void ShowView()
    {
        gameObject.SetActive(true);
    }
    public void HideView()
    {
        gameObject.SetActive(false);
    }
}

