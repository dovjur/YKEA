using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectRoom : MonoBehaviour
{
    private Button button;

    public static string loadName;
    void Start()
    {

        button = GetComponent<Button>();
        button.onClick.AddListener(SelectedRoom);
    }

    private void SelectedRoom()
    {
        TextMeshProUGUI textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshProUGUI != null)
        {
            loadName = textMeshProUGUI.text;
        }
        else
        {
            loadName = "";
        }
        SceneManager.LoadScene("Room");
    }
}
