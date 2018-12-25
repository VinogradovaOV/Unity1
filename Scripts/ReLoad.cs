using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Перезагрузка сцен
/// </summary>
public class ReLoad : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
    }

    void Update()
    {
        if (Player == null)
        {
            Invoke("ReLoadScene0", 2);
        }
    }

    void ReLoadScene0()
    {
        SceneManager.LoadScene(0);
    }
}
