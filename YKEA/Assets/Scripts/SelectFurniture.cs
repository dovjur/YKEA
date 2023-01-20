using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class SelectFurniture : MonoBehaviour
{
    public GameObject selectedFurniture;

    private CommandManager commandManager;
    private BuildingManager buildingManager;
    private Canvas canvas;
    
    void Start()
    {
        commandManager = GetComponent<CommandManager>();
        buildingManager = GetComponent<BuildingManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !buildingManager.IsMoving())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Furniture"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedFurniture != null)
        {
            Deselect();
        }
    }

    private void Select(GameObject obj)
    {
        if (obj == selectedFurniture)
        {
            return;
        }
        if (selectedFurniture != null)
        {
            Deselect();
        }
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null)
        {
            obj.AddComponent<Outline>();
        }
        else
        {
            outline.enabled = true;
        }
        selectedFurniture = obj;
        canvas = selectedFurniture.GetComponentInChildren<Canvas>();
        canvas.GetComponent<FurnitureUI>().furnitureName.text = selectedFurniture.name;
        canvas.enabled = true;
    }

    private void Deselect()
    {
        canvas.enabled = false;
        selectedFurniture.GetComponent<Outline>().enabled = false;
        selectedFurniture = null;
    }

    public void Move()
    {
        commandManager.ExecuteCommand(new MoveCommand(selectedFurniture.transform.parent.gameObject));
        canvas.enabled = false;
        selectedFurniture.GetComponent<CheckPlacement>().enabled = true;
        buildingManager.selectedFurniture = selectedFurniture.transform.parent.gameObject;
        Deselect();
    }

    public void SetColor()
    {
        commandManager.ExecuteCommand(new SelectColorCommand(selectedFurniture.GetComponent<Renderer>()));
    }

    public void Delete()
    {
        GameObject objToDestroy = selectedFurniture.transform.parent.gameObject;
        BuildingManager.furnituresInScene.Remove(objToDestroy);
        Deselect();
        commandManager.ExecuteCommand(new DeleteCommand(objToDestroy));
    }
}
