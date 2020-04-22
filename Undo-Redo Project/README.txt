****** INTRO ******
Aquesti projecte conté:
-Clase Action i derivades
-Clase UndoRedoManager
-Clase Spawner
-Escena de prova

El projecte te 4 tipus d'accions però es pot ampliar fent mès derivades de la classe "Action".
Les accions que hi ha per defecte son:
-Create
-Erase
-Mose
-Rotate


******* SET UP ******
-Crear un objecte de UndoRedoManager

-Després de cada acció s'ha de cridar la funció AddAction(action). 

-Com a paràmetre se li ha de passar un objecte de un derivat de action (la classe action per si sola es abstracta).

-Els derivats "ModuleMove" i "ModuleRotate" necessiten 2 paràmetres en el seu constructor: 
	-"ModuleMove": Un Vector3 (la última posició)  
	-"ModuleRotate": Un Quaternion (la última rotació)


******* COM FER SERVIR UNDO-REDO *******
Crides la funció Undo() i Redo() a on es fassin les accions.


******* EXEMPLE DE CADA CAS *********

//*** Instantiate object ***
GameObject _myObject = Instantiate(cubePrefab, cubeHand.transform.position, cubeHand.transform.rotation);
undoRedoManager.AddAction(new ModuleCreate(_myObject));
objectsSpawned.Add(_myObject);

//*** Delete object ***
_myObject.SetActive(false);
undoRedoManager.AddAction(new ModuleErase(_myObject));

//*** Move object ***
//Before moving, safe the previous position
Vector3 lastPosition = _myObject.transform.position;
_myObject.transform.position += new Vector3(2, 0, 0);
undoRedoManager.AddAction(new ModuleMove(_myObject, lastPosition));

//*** Rotate object ***
//Before moving, safe the previous rotation
Quaternion lastRotation = _myObject.transform.rotation;
_myObject.transform.Rotate(new Vector3(45, 0, 45));
undoRedoManager.AddAction(new ModuleRotate(_myObject, lastRotation));


******* COSES A TENIR A EN COMPTE *******
Quan es fa un AddAction() internament s'esborren tots els GameObjects que estàven guardats per fer el redo. 
Si es manté un control d'aquests objectes, per exemple quan fas un:

GameObject _myObject = Instantiate(...) 
undoRedoManager.AddAction(new ModuleCreate(_myObject));
objectsSpawned.Add(_myObject);

Quan tornis a fer un AddAction() es possble que alguns gameObjects de "objectsSpawned" es quedin a null