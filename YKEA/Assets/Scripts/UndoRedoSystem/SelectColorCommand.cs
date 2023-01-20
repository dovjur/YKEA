using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColorCommand : IAction
{
    private Renderer renderer;
    private Color color;
    public SelectColorCommand(Renderer renderer)
    {
        this.renderer = renderer;
    }
    public void ExecuteCommand()
    {
        color = renderer.material.color;
    }

    public void UndoCommand()
    {
        renderer.material.color = color;
    }
}
