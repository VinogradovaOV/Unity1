using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject PlayMenu;
    public GameObject SettMenu;
    bool start;

    void Start()
    {
        start = true;
        SettMenu.SetActive(!start);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    
    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        SettMenu.SetActive(start);
        PlayMenu.SetActive(!start);
        start = !start;
    }


}
