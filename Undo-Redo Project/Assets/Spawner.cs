using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject cubeHand;

    public List<GameObject> objectsSpawned;

    //"Action" collections
    List<Action> actionsDone;
    Stack<Action> actionsUndo;

    void Start()
    {
        actionsDone = new List<Action>();
        actionsUndo = new Stack<Action>();
    }

    // Update is called once per frame
    void Update()
    {
        #region CONTROL Z && CONTROL Y
        if (Input.GetKeyDown(KeyCode.Z))
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

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (actionsUndo.Count > 0)
            {
                Action a = actionsUndo.Pop();
                a.RedoAction();
                actionsDone.Add(a);
            }
        }

        #endregion

        #region Input Simulation
        //Test Intantiate cube with out VR
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject g = Instantiate(cubePrefab, cubeHand.transform.position, cubeHand.transform.rotation);
            actionsDone.Add(new ModuleCreate(g));
            objectsSpawned.Add(g);

            DeleteRedoStack();
        }

        //Funcio de move test
        if (Input.GetKeyDown(KeyCode.M))
        {           
            DeleteRedoStack();

            GameObject testGameObject = objectsSpawned[objectsSpawned.Count - 1];

            //Before moving, safe the previous position
            Vector3 lastPosition = testGameObject.transform.position;

            //Move the object
            testGameObject.transform.position += new Vector3(2, 0, 0);

            actionsDone.Add(new ModuleMove(testGameObject, lastPosition));
        }

        //Funcio de rotate test
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject testGameObject = objectsSpawned[objectsSpawned.Count - 1];

            //Before moving, safe the previous rotation
            Quaternion lastRotation = testGameObject.transform.rotation;

            //rotate the object
            testGameObject.transform.Rotate(new Vector3(45, 0, 45));

            actionsDone.Add(new ModuleRotate(testGameObject, lastRotation));
            DeleteRedoStack();
        }

        //Erase the last element
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject testGameObject = objectsSpawned[objectsSpawned.Count - 1];

            //El que hagi de fer lo de borrar cubs només ha de fer un SetActive(false), jo des de les accions m'encarrego de borrar els gameObjects després
            testGameObject.SetActive(false);
            actionsDone.Add(new ModuleErase(testGameObject));
            DeleteRedoStack();
        }

        #endregion
    }

    //This function is called when we do an action to delete the previous stack of actions.
    //We want to delete it to put new redo actions to the stack
    public void DeleteRedoStack()
    {
        if (actionsUndo.Count > 0)
        {
            foreach (Action a in actionsUndo)
            {
                if (a.GetType() == typeof(ModuleCreate) || a.GetType() == typeof(ModuleErase))
                {
                    a.DestroyModule();
                    objectsSpawned.Remove(a.GetModule());
                }
            }

            actionsUndo.Clear();
        }
    }
}