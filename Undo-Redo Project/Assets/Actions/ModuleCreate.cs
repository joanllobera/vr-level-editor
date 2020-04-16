using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCreate : Action
{
    public ModuleCreate(GameObject _module)
    {
        module = _module;
    }

    public override void UndoAction()
    {
        module.SetActive(false);
    }

    public override void RedoAction()
    {
        module.SetActive(true);
    }

}
