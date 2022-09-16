using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RingData ringData;
    public RingSpawner ringSpawner;
    public Grid grid;
    private void Awake()
    {
        Instance = this;
    }
}
