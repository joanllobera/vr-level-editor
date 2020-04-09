using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleAction : Action
{
    GameObject module;
    ActionType actionType;

    public ModuleAction(GameObject _module, ActionType _actionType)
    {
        module = _module;
        actionType = _actionType;
    }

    public override void UndoAction()
    {
        switch (actionType)
        {
            case ActionType.CREATE:
                {

                    break;
                }
            case ActionType.DELETE:
                {

                    break;
                }
            case ActionType.MOVE:
                {

                    break;
                }
            default:
                break;
        }
    }

    public override void RedoAction()
    {
        switch (actionType)
        {
            case ActionType.CREATE:
                {

                    break;
                }
            case ActionType.DELETE:
                { 
                    break;
                }
            case ActionType.MOVE:
                {

                    break;
                }
            default:
                break;
        }
    }
}
