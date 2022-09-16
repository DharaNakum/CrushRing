using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private RingHolder collidedHolder;
    //public Grid grid;
    public bool isCanPlaceRing;
    private SubRingHolder subRingHolder;
    public List<ColorType> colorTypesOfRings = new List<ColorType>();
    public ColorType colorType;
    Vector3 ray;
    RaycastHit2D hit;
    void Start()
    {
        isCanPlaceRing = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClickEvents();
        }
        if (Input.GetMouseButton(0))
        {
            subRingHolder.MoveRingHolder();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUpEvents();

        }
    }

    private void OnMouseUpEvents()
    {
        GenerateRay(out ray, out hit);
        if (Physics2D.Raycast(ray, new Vector2(0f, 0f)))
        {
            if (hit.collider.tag == "Cell")
            {
                Cell collidedCellWithPlayer = hit.collider.gameObject.GetComponent<Cell>();
                isCanPlaceRing = collidedCellWithPlayer.CanPlaceRing(subRingHolder.GetChildren());
                if (isCanPlaceRing)
                {
                    PlaceRingInCell(collidedCellWithPlayer);
                }
                else
                {
                    subRingHolder.PlaceHolder();
                }

            }
        }
        else
        {
            subRingHolder.PlaceHolder();
        }
    }

    private void OnMouseClickEvents()
    {
        isCanPlaceRing = false;
        GenerateRay(out ray, out hit);
        if (Physics2D.Raycast(ray, new Vector2(0f, 0f)))
        {
            if (hit.collider.tag == "RingHolder")
            {
                FindSubHolder();
                GameManager.Instance.grid.CheckRingPlaceInGrid(subRingHolder.GetChildren(), subRingHolder);
            }

        }
    }

    private void PlaceRingInCell(Cell collidedCellWithPlayer)
    {
        for (int g = 0; g < subRingHolder.transform.childCount; g++)
        {
            colorTypesOfRings.Add(subRingHolder.transform.GetChild(g).gameObject.GetComponent<Ring>().colorType);
        }

        PlaceRing(subRingHolder.SetRingPosition(), subRingHolder, collidedCellWithPlayer);
        isCanPlaceRing = false;
        for (int g = 0; g < colorTypesOfRings.Count; g++)
        {
            GameManager.Instance.grid.MatchThree(collidedCellWithPlayer.cellID, colorTypesOfRings[g]);
        }
        subRingHolder.ClearList();
        subRingHolder = null;
        RuleManager.spawnRingCount--;
    }

    private void FindSubHolder()
    {
        collidedHolder = hit.collider.gameObject.GetComponent<RingHolder>();
        subRingHolder = collidedHolder.GetSubRingHolder();
    }

    private static void GenerateRay(out Vector3 ray, out RaycastHit2D hit)
    {
        ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(ray, new Vector2(0f, 0f));
    }

    private void PlaceRing(List<Ring> lists, SubRingHolder subRingHolder, Cell collidedCell)
    {
        for(int i=0;i<lists.Count;i++)
        {
            lists[i].transform.position = collidedCell.transform.position;
            lists[i].transform.parent = collidedCell.transform;
            collidedCell.SetRing(lists[i]);
            UIManager.Instance.gamePlayView.score++;

        }
        subRingHolder.PlaceHolder();
    }
}
