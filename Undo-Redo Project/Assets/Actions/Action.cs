using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    protected GameObject module;

    public abstract void UndoAction();
    public abstract void RedoAction();

    public GameObject GetModule()
    {
        return module;
    }
    public void DestroyModule()
    {
        Destroy(module);
    }
}
