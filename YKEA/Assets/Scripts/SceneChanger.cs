using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public void LoadRoom()
    {
        SceneManager.LoadScene("Room");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
