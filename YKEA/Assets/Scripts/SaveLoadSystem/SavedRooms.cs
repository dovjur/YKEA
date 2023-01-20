using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SavedRooms : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private Button loadButton;
    public GameObject panelParent;
    private List<string> rooms = new List<string>();
    public List<Sprite> sprites = new List<Sprite>();

    private const string imagePath = "/screenshots";
    private const string filePath = "/saves";
    private void Start()
    {
        GetRooms();
        StartCoroutine(GetImages());
    }
    private void GetRooms()
    {
        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath + filePath);
        FileInfo[] fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            rooms.Add(file.Name);
        }
    }

    private IEnumerator GetImages()
    {
        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath + imagePath);
        FileInfo[] fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            var www = UnityWebRequestTexture.GetTexture("file://" + file.FullName);
            yield return www.SendWebRequest();

            var texture = DownloadHandlerTexture.GetContent(www);

            Rect rec = new Rect(0, 0, texture.width, texture.height);
            sprites.Add(Sprite.Create(texture, rec, new Vector2(0, 0), 1));

            loadButton.interactable = true;
        }
    }

    public void DisplayRooms()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            GameObject obj = Instantiate(buttonPrefab, panelParent.transform);
            obj.transform.SetParent(panelParent.transform,false);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = rooms[i];
            obj.GetComponentsInChildren<Image>()[1].sprite = sprites[i];
        }
    }
}
