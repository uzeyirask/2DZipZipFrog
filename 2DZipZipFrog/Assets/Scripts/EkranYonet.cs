using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI kütüphanesini ekleyin

public class StartMenu : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSceneMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadSceneNextLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}