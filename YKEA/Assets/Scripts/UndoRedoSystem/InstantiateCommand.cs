using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCommand : IAction
{
    private  BuildingManager buildingManager;

    private GameObject prefab;
    private Vector3 position;
    private Quaternion rotation;

    private GameObject spawnedObject;
    public InstantiateCommand(BuildingManager buildingManager,GameObject prefab, Vector3 position, Quaternion rotation)
    {
        this.buildingManager = buildingManager;
        this.prefab = prefab;
        this.position = position;
        this.rotation = rotation;
    }
    public void ExecuteCommand()
    {
        spawnedObject = GameObject.Instantiate(prefab,position,rotation);
        buildingManager.selectedFurniture = spawnedObject;
    }

    public void UndoCommand()
    {
        BuildingManager.furnituresInScene.Remove(spawnedObject);
        GameObject.Destroy(spawnedObject);
    }
}
