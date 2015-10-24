using UnityEngine;
using System.Collections;

public class RES_Stone : Resource {

	// Class instantiator (replaces the class constructor)
	public static RES_Stone GetNew() {
		return new RES_Stone ();
	}
	
	public RES_Stone() {
		
		Name = "Stone";
		
	}

}
