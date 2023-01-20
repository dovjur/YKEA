using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] furnitures;
    public GameObject selectedFurniture;
    public static List<GameObject> furnituresInScene = new List<GameObject>();
    public bool canPlace;

    [SerializeField]
    private float gridSize;
    [SerializeField]
    private float snapDistance = 1f;
    [SerializeField]
    private LayerMask layerMask;
    private RaycastHit hit;
    private Vector3 pos;
    private CommandManager commandManager;
    private bool gridOn;
    private bool snapOn;

    void Start()
    {
        commandManager = GetComponent<CommandManager>();
    }

    void Update()
    {
        if (selectedFurniture != null)
        {
            if (snapOn)
            {
                for (int i = 0; i < furnituresInScene.Count; i++)
                {
                    if (selectedFurniture != furnituresInScene[i])
                    {
                        Collider myCollider = selectedFurniture.GetComponentInChildren<Collider>();
                        Collider targetCollider = furnituresInScene[i].GetComponentInChildren<Collider>();
                        Vector3 myClosestPoint = myCollider.ClosestPoint(targetCollider.transform.position);
                        Vector3 targetClosestPoint = targetCollider.ClosestPoint(myClosestPoint);
                        Vector3 offset = targetClosestPoint - myClosestPoint;
                        if (offset.magnitude < snapDistance)
                        {
                            pos += offset;
                        }
                    }
                    selectedFurniture.transform.position = pos;
                }
            }
            else if (gridOn)
            {
                selectedFurniture.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x),
                    RoundToNearestGrid(pos.y),
                    RoundToNearestGrid(pos.z));
            }
            else
            {
                selectedFurniture.transform.position = pos;
            }
           
            if (Input.GetMouseButton(0) && canPlace)
            {
                selectedFurniture.GetComponentInChildren<CheckPlacement>().enabled = false;
                selectedFurniture = null;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Rotate();
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectFurniture(int index)
    {
        commandManager.ExecuteCommand(new InstantiateCommand(this,furnitures[index], pos, transform.rotation));
        selectedFurniture.GetComponentInChildren<CheckPlacement>().enabled = true;
        furnituresInScene.Add(selectedFurniture);
    }

    private float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }

    public bool IsMoving()
    {
        if (selectedFurniture != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Rotate()
    {
        selectedFurniture.GetComponentsInChildren<Transform>()[1].Rotate(new Vector3(0, 45, 0));
    }

    public void ToggleGrid()
    {
        if (gridOn)
        {
            gridOn = false;
        }
        else
        {
            gridOn = true;
        }
    }

    public void ToggleSnap()
    {
        if (snapOn)
        {
            snapOn = false;
        }
        else
        {
            snapOn = true;
        }
    }
}
