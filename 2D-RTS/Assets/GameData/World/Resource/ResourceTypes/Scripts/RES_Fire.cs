using UnityEngine;
using System.Collections;

public class RES_Fire : Resource {

	// Class instantiator (replaces the class constructor)
	public static RES_Fire GetNew() {
		return new RES_Fire ();
	}
	
	public RES_Fire() {
		
		Name = "Fire";
		
	}

}
