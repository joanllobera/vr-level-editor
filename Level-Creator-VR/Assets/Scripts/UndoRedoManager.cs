 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedoManager : MonoBehaviour
{
    //"Action" collections
    List<ModuleAction> actionsDone;
    Stack<ModuleAction> actionsUndo;

    public UndoRedoManager()
    {
        actionsDone = new List<ModuleAction>();
        actionsUndo = new Stack<ModuleAction>();
    }

    public void Undo()
    {
        if (actionsDone.Count > 0)
        {
            //fem el push de l'ultim element que s'ha posat a la llista
            actionsUndo.Push(actionsDone[actionsDone.Count - 1]);
            //Borrar l'element de la llista
            actionsDone.RemoveAt(actionsDone.Count - 1);
            //Desactivar el objecte per que no es vegi
            actionsUndo.Peek().UndoAction();
        }
    }

    public void Redo()
    {
        if (actionsUndo.Count > 0)
        {
            ModuleAction a = actionsUndo.Pop();
            a.RedoAction();
            actionsDone.Add(a);
        }
    }

    //La llista objects es una llista que has de crer tu, que ha de tenir tots els objectes que instanties
    //Després de cada AddAction(a,objects) hi ha d'haver un Add() a la teva llista de objects
    //Aquest segon paràmetre es podria posar per esborrar els gameObjects de l'array que proablament tindrà l'usuari
    public void AddAction(ModuleAction a,List<GameObject> objects)
    {
        DeleteRedoStack(objects);
        actionsDone.Add(a);
    }

    //This function is called when we do an action to delete the previous stack of actions.
    //We want to delete it to put new redo actions to the stack
    void DeleteRedoStack(List<GameObject> objects)
    {
        if (actionsUndo.Count > 0)
        {
            foreach (ModuleAction a in actionsUndo)
            {
                if (a.GetType() == typeof(ModuleCreate) || a.GetType() == typeof(ModuleErase))
                {
                    a.DestroyModule();
                    objects.Remove(a.GetModule());
                }
            }

            actionsUndo.Clear();
        }
    }

}
