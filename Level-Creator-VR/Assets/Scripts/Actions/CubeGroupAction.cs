using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGroupAction : Action
{
    List<GameObject> cubes;
    ActionType actionType;

    public CubeGroupAction(List<GameObject> _cubes, ActionType _actionType)
    {
        cubes = new List<GameObject>(_cubes);
        actionType = _actionType;
    }

    public override void UndoAction()
    {
        switch(actionType)
        {
            case ActionType.CREATE:
            {
                    foreach(GameObject g in cubes)
                    {
                        g.SetActive(false);
                    }
                break;
            }
            case ActionType.DELETE:
            {
                    foreach (GameObject g in cubes)
                    {
                        g.SetActive(true);
                    }
                    break;
            }
            case ActionType.MOVE:
            {
                    foreach (GameObject g in cubes)
                    {
                        g.SetActive(false);
                    }
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
                    foreach (GameObject g in cubes)
                    {
                        g.SetActive(false);
                    }
                    break;
            }
            case ActionType.DELETE:
            {
                    foreach (GameObject g in cubes)
                    {
                        g.SetActive(false);
                    }
                    break;
            }
            case ActionType.MOVE:
            {
                    foreach (GameObject g in cubes)
                    {
                        g.SetActive(false);
                    }
                    break;
            }
            default:
                break;
        }
    }

}
