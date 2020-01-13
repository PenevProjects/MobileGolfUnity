using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    public void ToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("_Scenes/Game");
        AddHit.hitCount = 0;
        TimeCounter.timeElapsed = 0;
    }
    public void ToExit()
    {
        Application.Quit();
    }
    public void ToEnd()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("_Scenes/End");
    }
    public void ToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("_Scenes/Menu");
    }
}
