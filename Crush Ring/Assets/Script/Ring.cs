using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ring : MonoBehaviour
{
    public ColorType colorType;
    public SpriteType spriteType;
    public SpriteRenderer spriteRendererOfRing;
    
    
    public void SetColor(int colorindex)
    {
        spriteRendererOfRing.color = GameManager.Instance.ringData.colorData[colorindex].color;
        colorType = GameManager.Instance.ringData.colorData[colorindex].colorType;
       
    }
    public void SetSize(int sizeIndex)
    {
        spriteRendererOfRing.sprite = GameManager.Instance.ringData.GetSpritThroughIndex(sizeIndex);
        spriteType = GameManager.Instance.ringData.GetSpritTypeThroughIndex(sizeIndex);
    }
    public void DestroyMe()
    {
        UIManager.Instance.gamePlayView.score++;
        this.GetComponentInParent<Cell>().rings.Remove(this);
        Destroy(gameObject);
    }
}

