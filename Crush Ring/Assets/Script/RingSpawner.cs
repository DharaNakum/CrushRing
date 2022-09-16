using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawner : MonoBehaviour
{
    //public static RingSpawner Instance;
    public Ring spawnedRing;
    public List<SubRingHolder> subRingHolder;
    public Ring ring;
    int tempColor, tempSize;

    /*private void Awake()
    {
        Instance = this;
    }*/

    public void SpawnRing()
    {   
        for (int i = 0; i < subRingHolder.Count; i++)
        {
            RuleManager.spawnRingCount++;
            tempSize = Random.Range(0, GameManager.Instance.grid.placeableRingSize.Count);
            GenerateRing(i);
            RemoveRingSizeFromList(i);
            if (GameManager.Instance.grid.placeableRingSize.Count == 0)
            {
                Debug.Log("ruleManager  " + RuleManager.spawnRingCount);
                break;
            }
        }
    }

    private void GenerateRing(int i)
    {
        if (GameManager.Instance.grid.placeableRingSize.Count > 0)
        {
            if (GameManager.Instance.grid.placeableRingSize[tempSize] % (int)SpriteType.SMALL == 0)
            {
                GenerateRingAccordingToSize(i, (int)SpriteType.SMALL);
            }
            if (GameManager.Instance.grid.placeableRingSize[tempSize] % (int)SpriteType.MEDIUM == 0)
            {
                GenerateRingAccordingToSize(i, (int)SpriteType.MEDIUM);
            }
            if (GameManager.Instance.grid.placeableRingSize[tempSize] % (int)SpriteType.LARGE == 0)
            {
                GenerateRingAccordingToSize(i, (int)SpriteType.LARGE);
            }
            subRingHolder[i].isEmpty = false;
        }
    }

    void RemoveRingSizeFromList(int i)
    {
        for (int g = 0; g < subRingHolder[i].transform.childCount; g++)
        {
            int tempsizeIndex = ((int)subRingHolder[i].transform.GetChild(g).gameObject.GetComponent<Ring>().spriteType);
            RemoveSpawnRIngFromList(tempsizeIndex, Constant.MAX_SPRITETYPES - g);
        }
    }

    void GenerateRingAccordingToSize(int i,int indexOfSpriteType)
    {
        ring = Instantiate(spawnedRing, subRingHolder[i].transform.parent.transform.position, Quaternion.identity);
        tempColor = Random.Range(0, 7);
        ring.SetColor(tempColor);
        ring.SetSize(indexOfSpriteType);
        ring.transform.parent = subRingHolder[i].transform;
    }
    void RemoveSpawnRIngFromList(int tempsizeIndex, int i)
    {
        for (int f = 0; f < i; f++)
        {
            if (f == 0)
                GameManager.Instance.grid.placeableRingSize.Remove(GameManager.Instance.grid.placeableRingSize.Find(x => x % tempsizeIndex == 0));
            else
                GameManager.Instance.grid.placeableRingSize.Remove(GameManager.Instance.grid.placeableRingSize.Find(x => x % tempsizeIndex == 0 && x > Constant.ONE_RING));
        }
    }

}
