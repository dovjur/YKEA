using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour
{
    [SerializeField]
    private Button undoButton;
    private Stack<IAction> historyStack = new Stack<IAction>();

    private void Update()
    {
        if (historyStack.Count > 0)
        {
            undoButton.interactable = true;
        }
        else
        {
            undoButton.interactable = false;
        }
    }
    public void ExecuteCommand(IAction action)
    {
        action.ExecuteCommand();
        historyStack.Push(action);
    }

    public void UndoCommand()
    {
        if (historyStack.Count > 0)
        {
            historyStack.Pop().UndoCommand();
        }
    }
}
