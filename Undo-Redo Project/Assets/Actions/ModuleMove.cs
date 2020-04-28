using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleMove : Action
{
    GameObject module;

    public Vector3 lastPosition;

    public ModuleMove(GameObject _module, Vector3 _lastPosition)
    {
        module = _module;
        lastPosition = _lastPosition;
    }

    //Saps que estarà a la llista d'accions quan es cridi aquesta funció
    public override void UndoAction()
    {
        Vector3 aux = module.transform.position;
        module.transform.position = lastPosition;
        lastPosition = aux;
    }

    //Saps que estarà a l'stack de accions quan es cridi aquesta funció
    public override void RedoAction()
    {
        Vector3 aux = module.transform.position;
        module.transform.position = lastPosition;
        lastPosition = aux;
    }   
}
