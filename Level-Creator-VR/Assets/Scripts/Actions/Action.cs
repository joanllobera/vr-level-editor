using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public enum ActionType { CREATE, DELETE, MOVE};

    public abstract void UndoAction();
    public abstract void RedoAction();
}
