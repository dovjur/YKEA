using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureHandler : MonoBehaviour
{
    public FurnitureType furnitureType;
    public FurnitureData furnitureData;

    void Start()
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
        if (string.IsNullOrEmpty(furnitureData.id))
        {
            furnitureData.id = System.DateTime.Now.ToLongTimeString() + Random.Range(0, int.MaxValue).ToString();
            furnitureData.type = furnitureType;
            SaveData.current.furnitures.Add(furnitureData);
        }
    }

    void Update()
    {
        furnitureData.SetPosition(transform.parent.transform.position);
        furnitureData.SetRotation(transform.rotation);
        furnitureData.SetColor(transform.GetComponent<Renderer>().material.color);  
    }

    private void OnDestroy()
    {
        SaveData.current.furnitures.Remove(furnitureData);
    }
}
