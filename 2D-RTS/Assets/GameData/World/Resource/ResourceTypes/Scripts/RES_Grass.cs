using UnityEngine;
using System.Collections;

public class RES_Grass : Resource {

	// Class instantiator (replaces the class constructor)
	public static RES_Grass GetNew() {
		return new RES_Grass ();
	}
	
	public RES_Grass() {
		
		Name = "Grass";
		
	}

}
