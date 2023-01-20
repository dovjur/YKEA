using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeleteCommand : IAction
{
    private GameObject gameObject;
    public DeleteCommand(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    public void ExecuteCommand()
    {
        Undo.DestroyObjectImmediate(gameObject);
    }

    public void UndoCommand()
    {
        Undo.PerformUndo();
    }
}
