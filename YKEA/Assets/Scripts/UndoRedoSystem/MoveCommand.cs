using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : IAction
{
    private GameObject gameObject;
    private Vector3 posiotion;
    private Quaternion rotation;
    public MoveCommand(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    public void ExecuteCommand()
    {    
        posiotion = gameObject.transform.position;
        rotation = gameObject.transform.rotation;
    }

    public void UndoCommand()
    {
        gameObject.transform.position = posiotion;
        gameObject.transform.rotation = rotation;
    }
}
