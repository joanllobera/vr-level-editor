using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleErase : Action
{

    public ModuleErase(GameObject _module)
    {
        module = _module;
    }

    public override void UndoAction()
    {
        module.SetActive(true);
    }

    public override void RedoAction()
    {
        module.SetActive(false);
    }
}
