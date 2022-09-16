using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePlayView : BaseView
{
    public Text displayScore;
    public Text highScore;
    public  int score;

    void Start()
    {
        score = 0;
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        displayScore.text = score.ToString();
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.text = score.ToString();
        }
    }

    public void OnPauseButtonClick()
    {
        //PauseManueUI.SetActive(true);
        Time.timeScale = 0;
        UIManager.Instance.pauseView.ShowView();
    }

}
