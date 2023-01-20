using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public TMP_InputField saveName;

    [SerializeField]
    private GameObject[] furnitures;

    private void Start()
    {
        BuildingManager.furnituresInScene = new List<GameObject>();
        if (!string.IsNullOrEmpty(SelectRoom.loadName))
        {
            OnLoad(SelectRoom.loadName);
        }
    }

    public void OnSave()
    {
        if (!string.IsNullOrEmpty(saveName.text))
        {
            SaveLoadSystem.Save(saveName.text, SaveData.current);
        }
    }

    public void OnLoad(string loadName)
    {
        SaveData.current = (SaveData)SaveLoadSystem.Load(Application.persistentDataPath + "/saves/" + loadName);

        for (int i = 0; i < SaveData.current.furnitures.Count; i++)
        {
            FurnitureData currentFurniture = SaveData.current.furnitures[i];
            GameObject obj = Instantiate(furnitures[(int)currentFurniture.type]);
            FurnitureHandler furnitureHandler = obj.GetComponentInChildren<FurnitureHandler>();
            furnitureHandler.furnitureData = currentFurniture;
            furnitureHandler.transform.parent.transform.position = currentFurniture.GetPosition();
            furnitureHandler.transform.rotation = currentFurniture.GetRotation();
            furnitureHandler.GetComponentInChildren<Renderer>().material.color = currentFurniture.GetColor();

            BuildingManager.furnituresInScene.Add(obj);
        }
    }
}
