using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Grid : MonoBehaviour
{
    public List<Cell> cells = new List<Cell>();
    public List<int> colorMatchCell = new List<int>();
    public List<int> placeableRingSize = new List<int>();
    public bool canPlaceRingInCell;
    
    private void Start()
    {
        canPlaceRingInCell = false;
        DefinePlaceableRingSize();
    }
    public Cell GetCell(int cellId)
    {
        return cells.Find(x => x.cellID == cellId); 
    }

    public void CheckRingPlaceInGrid(List<SpriteType> spriteTypes,SubRingHolder currentSubholder)
    {
        canPlaceRingInCell = false;
        for (int i=0;i<cells.Count;i++)
        {
            if(cells[i].CanPlaceRing(spriteTypes))
            {
                canPlaceRingInCell = true;
                break;
            }
        }
        
        if(canPlaceRingInCell==false)
        {
            if (IsRingHolderEmpty(currentSubholder))
            {
                Debug.Log("No moves");
                RuleManager.Instance.GameOver();
            }
            else
                Debug.Log("place another");
        }
    }

    private bool IsRingHolderEmpty(SubRingHolder currentSubholder)
    {
        return GameManager.Instance.ringSpawner.subRingHolder.FindAll(x => x.isEmpty == true && x != currentSubholder).Count == 0 || GameManager.Instance.ringSpawner.subRingHolder.FindAll(x => x.isEmpty == true && x != currentSubholder) != null;
    }

    public void MatchThree(int cellId, ColorType currentColorType)
    {
        colorMatchCell.Clear();
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].rings.Find(y => y.colorType == currentColorType))
                colorMatchCell.Add(cells[i].cellID);
        }
        CellContaInSameRing(cellId, currentColorType);
        GridContaInSameRing(cellId, currentColorType);
    }

    void CellContaInSameRing(int cellId, ColorType currentColorType)
        {
            if (GetCell(cellId).rings.Count == Constant.MAX_RING_IN_CELL)
            {
                List<Ring> destroyRingInCell = RuleManager.Instance.CheckMetchColorInCell(colorMatchCell, cellId, currentColorType);
                if (destroyRingInCell != null && destroyRingInCell.Count >= Constant.MAX_RING_IN_CELL)
                {
                    UpdateGrid(destroyRingInCell, currentColorType);
                }
            }
        }
    void GridContaInSameRing(int cellId, ColorType currentColorType)
    {
        if (colorMatchCell.Count >= Constant.MAX_RING_IN_CELL)
        {
            if (colorMatchCell.Count >= Constant.MAX_RING_IN_CELL)
            {
                List<Ring> destroyRing = RuleManager.Instance.CheckMetchThreeColor(colorMatchCell, cellId, currentColorType);
                if (destroyRing != null && destroyRing.Count >= Constant.MAX_RING_IN_CELL)
                {
                    UpdateGrid(destroyRing, currentColorType);
                }
            }
        }
    }

    private void UpdateGrid(List<Ring> lists, ColorType currentColorType)
    {
        for(int i=0;i<lists.Count;i++)
        {
            lists[i].DestroyMe();
        }
    }

    private void DefinePlaceableRingSize()
    {
        for (int i = 0; i < cells.Count; i++)
            placeableRingSize.AddRange(GameManager.Instance.ringData.ringsSize);
    }

    public void PlaceableRingSize()
    {
        placeableRingSize.Clear();
        for(int i=0;i<cells.Count;i++)
        {
            if(cells[i].rings.Count == Constant.MAX_RING_IN_CELL)
            {
            }
            else if (cells[i].rings.Count == Constant.TWO_CHILD)
            {
                placeableRingSize.AddRange(FindPlaceableRings(i));
            }
            else if (cells[i].rings.Count == Constant.ONE_CHILD)
            {
                placeableRingSize.AddRange(GameManager.Instance.ringData.ringsSize.FindAll(x => x % (int)cells[i].rings[0].spriteType != 0));
            }
            else
            {
                placeableRingSize.AddRange(GameManager.Instance.ringData.ringsSize);
            }
        }
        if(placeableRingSize.Count==0 || placeableRingSize==null)
        {
            Debug.Log("Game Over");
            RuleManager.Instance.GameOver();
        }
    }

    private List<int> FindPlaceableRings(int i)
    {
        return GameManager.Instance.ringData.ringsSize.FindAll(x => x % (int)cells[i].rings[0].spriteType != 0);
    }
}
