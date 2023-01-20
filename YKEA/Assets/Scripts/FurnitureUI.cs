using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureUI : MonoBehaviour
{
    public TextMeshProUGUI furnitureName;

    private SelectFurniture selectFurniture;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button setColorButton;
    
    void Start()
    {
        selectFurniture = GameObject.Find("GameManager").GetComponent<SelectFurniture>();

        moveButton.onClick.AddListener(selectFurniture.Move);
        deleteButton.onClick.AddListener(selectFurniture.Delete);
        setColorButton.onClick.AddListener(selectFurniture.SetColor);
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
