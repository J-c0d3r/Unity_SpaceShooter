using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;

    private void Awake()
    {
        if (gm)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    IEnumerator FirstScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    public void Menu()
    {
        StartCoroutine(FirstScene());
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
