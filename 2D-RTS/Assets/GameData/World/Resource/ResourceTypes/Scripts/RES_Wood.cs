using UnityEngine;
using System.Collections;

public class RES_Wood : Resource {

	// Class instantiator (replaces the class constructor)
	public static RES_Wood GetNew() {
		return new RES_Wood ();
	}

	public RES_Wood() {

		Name = "Wood";

	}

}
