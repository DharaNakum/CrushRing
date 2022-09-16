using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GamePlayView gamePlayView;
    public PauseView pauseView;
    public GameOverView gameOverView;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gamePlayView.ShowView();
        gameOverView.HideView();
        pauseView.HideView();
    }
    public void OnGameover()
    {
        gameOverView.ShowView();
        gamePlayView.HideView();
        Time.timeScale = 0;
    }
}
