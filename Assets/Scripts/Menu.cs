using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Pressed Enter");
            SceneManager.LoadScene(1);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
