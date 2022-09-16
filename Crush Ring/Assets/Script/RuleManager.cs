using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    public static RuleManager Instance;
    public static int spawnRingCount;
    public RingSpawner ringSpawner;
    public Grid grid;
    public List<Ring> destroyRing = new List<Ring>();
    

    private void Awake()
    {
        Instance = this;
        spawnRingCount = 0;
    }

    
    void Update()
    {
        if (spawnRingCount == 0)
        {
            grid.PlaceableRingSize();
            ringSpawner.SpawnRing();

        }
    }

    public List<Ring> CheckMetchColorInCell(List<int> lists, int collidedCellId, ColorType currentcolorType)
    {
        if(grid.GetCell(collidedCellId).rings.FindAll(x=>x.colorType==currentcolorType).Count>=Constant.MAX_RING_IN_CELL)
        {
            return grid.GetCell(collidedCellId).rings.FindAll(x => x.colorType == currentcolorType);
        }
        else
        {
            return null;
        }
    }

    public List<Ring> CheckMetchThreeColor(List<int> list,  int collidedCellId, ColorType currentcolorType)
    {
        destroyRing.Clear();
        if (list.FindAll(x => x % Constant.COL_OFFSET == collidedCellId % Constant.COL_OFFSET).Count == Constant.MAX_RING_IN_CELL)
        {
            destroyRing.AddRange(CollectDestroyRing(list.FindAll(x => x % Constant.COL_OFFSET == collidedCellId % Constant.COL_OFFSET), collidedCellId, currentcolorType));
        }
        if (list.FindAll(x => x % Constant.FIRST_DIAGONAL_OFFSET== collidedCellId % Constant.FIRST_DIAGONAL_OFFSET).Count == Constant.MAX_RING_IN_CELL)
        {
            destroyRing.AddRange(CollectDestroyRing(list.FindAll(x => x % Constant.FIRST_DIAGONAL_OFFSET == collidedCellId % Constant.FIRST_DIAGONAL_OFFSET), collidedCellId, currentcolorType));
        }
        if (list.FindAll(x => x % Constant.SECOND_DIAGONAL_OFFSET == collidedCellId % Constant.SECOND_DIAGONAL_OFFSET).Count == Constant.MAX_RING_IN_CELL)
        {
            destroyRing.AddRange(CollectDestroyRing(list.FindAll(x => x % Constant.SECOND_DIAGONAL_OFFSET == collidedCellId % Constant.SECOND_DIAGONAL_OFFSET), collidedCellId, currentcolorType));
        }
        if (list.FindAll(x => x - collidedCellId <= Constant.ROW_OFFSET && x - collidedCellId >= -Constant.ROW_OFFSET).Count == Constant.MAX_RING_IN_CELL)
        {
            destroyRing.AddRange(CollectDestroyRing(list.FindAll(x => x - collidedCellId <= Constant.ROW_OFFSET && x - collidedCellId >= -Constant.ROW_OFFSET), collidedCellId, currentcolorType));
        }
        return destroyRing;      
    }
    List<Ring> CollectDestroyRing(List<int> list, int collidedCellId, ColorType currentcolorType)
    {
        List<Ring> ringsDestroy = new List<Ring>();
        for (int j = 0; j < list.Count; j++)
        {
            Cell cell = grid.GetCell(list[j]);
            ringsDestroy.AddRange(cell.rings.FindAll(y => y.colorType == currentcolorType));
        }
        return ringsDestroy;
    }

    public void GameOver()
    {
        UIManager.Instance.OnGameover();
    }
}
