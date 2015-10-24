using UnityEngine;
using System.Collections;

public class RES_Water : Resource {

	// Class instantiator (replaces the class constructor)
	public static RES_Water GetNew() {
		return new RES_Water ();
	}
	
	public RES_Water() {
		
		Name = "Water";
		
	}

}
