using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{



    private void Start()
    {
        SoundController.instance.PlayBackgroundMusic(0);
    }


    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SoundController.instance.PlayBackgroundMusic(1);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit the game.");
    }
    public void OpenURL()
    {
        Application.OpenURL("https://github.com/emirsinanyoldas/ZombieShooterGame");
    }

}