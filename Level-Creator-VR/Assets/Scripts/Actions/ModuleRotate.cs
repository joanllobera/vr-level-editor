using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleRotate : ModuleAction
{
    GameObject module;

    public Quaternion lastRotation;

    public ModuleRotate(GameObject _module, Quaternion _lastRotation)
    {
        module = _module;
        lastRotation = _lastRotation;
    }

    //Saps que estarà a la llista d'accions quan es cridi aquesta funció
    public override void UndoAction()
    {
        Quaternion aux = module.transform.rotation;
        module.transform.rotation = lastRotation;
        lastRotation = aux;
    }

    //Saps que estarà a l'stack de accions quan es cridi aquesta funció
    public override void RedoAction()
    {
        Quaternion aux = module.transform.rotation;
        module.transform.rotation = lastRotation;
        lastRotation = aux;
    }   
}
