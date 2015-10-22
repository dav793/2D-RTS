using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 *	Class: GameObjectPool
 *
 *	Data structure which instantiates and stores <size> number of GameObjects in a pool. The GameObjects it creates
 *	are exact copies of <reference_object>.
 *	
 *	The renderer can retrieve (pop) a GameObject and use it for rendering a new object.
 *	
 *	When the renderer needs to unrender the object, it may return (push) it to the pool for later use.
 *	
 *	This way, the renderer does not have to instantiate new GameObjects whenever it needs to render a new object.
 *	Instead it may use a previously instantiated GameObject from the pool, thus avoiding GameObject instantiation
 *	on runtime, which is a very expensive operation.
 *
 */
public class GameObjectPool : MonoBehaviour {

	public GameObject reference_object;
	Stack<GameObject> pool;
	int size;
	string obj_name = "Unassigned";
	bool initialized = false;

	public void Init(int pool_size) {
		checkIntegrity ();
		size = pool_size;
		initialize (gameObject);
	}

	public void Init(int pool_size, string default_obj_name) {
		obj_name = default_obj_name;
		Init (pool_size);
	}

	public GameObject pop() {
		checkInitialized ();
		if (pool.Count > 0) {
			GameObject obj = pool.Pop();
			obj.SetActive(true);
			return obj;
		}
		return null;
	}
	
	public void push(GameObject obj) {
		checkInitialized ();
		obj.name = obj_name;
		obj.transform.position = gameObject.transform.position;
		obj.SetActive(false);
		pool.Push (obj);
	}

	public int getCount() {
		checkInitialized ();
		return pool.Count;
	}

	void initialize(GameObject parent_obj) {
		pool = new Stack<GameObject> ();
		for (int i = 0; i < size; ++i) {
			GameObject obj = GameObject.Instantiate(reference_object) as GameObject;
			obj.name = obj_name;
			obj.transform.parent = parent_obj.transform;
			obj.transform.position = gameObject.transform.position;
			obj.SetActive(false);
			pool.Push(obj);
		}
		initialized = true;
	}

	void checkInitialized() {
		if (!initialized) {
			throw new InvalidOperationException("GameObject pool not initialized.");
		}
	}

	void checkIntegrity() {
		if (reference_object == null) {
			throw new InvalidOperationException("GameObject pool reference object not assigned.");
		}
	}

}
