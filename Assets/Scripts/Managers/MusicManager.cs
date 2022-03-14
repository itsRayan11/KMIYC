using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);

       // DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            Destroy(this.gameObject);
        }
        if (SceneManager.GetActiveScene().name == "Intro")
        {
            Destroy(this.gameObject);
        }
    }
}