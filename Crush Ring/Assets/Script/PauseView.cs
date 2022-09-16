using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseView : BaseView
{
    public GameObject PauseManueUI;
   
    public void OnResumeButtonClick()
    {
       // PauseManueUI.SetActive(false);
        Time.timeScale = 1;
        UIManager.Instance.pauseView.HideView();
    }
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
