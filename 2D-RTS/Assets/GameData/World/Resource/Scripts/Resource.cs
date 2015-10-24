using UnityEngine;
using System;
using System.Collections;

/*
 *	Class: Resource
 *
 *	Parent class for all resources in the game.
 *	Every resource must inherit from this class.
 *
 */
public class Resource {

	string _name;

	public string Name 
	{
		get 
		{ 
			if (string.IsNullOrEmpty(_name))
				throw new InvalidOperationException("Undefined Name. Did you instanciate the Resource class directly?");

			return _name; 
		}

		set { _name = value; }
	
	}

	/*public static Resource findResourceByName(string name) {
		switch (name.ToLower()){
		case "wood":
			return RES_Wood.GetNew();
		case "grass":
			return RES_Grass.GetNew();
		case "stone":
			return RES_Stone.GetNew();
		case "fire":
			return RES_Fire.GetNew();
		case "water":
			return RES_Water.GetNew();
		}
		return null;
	}*/

}
