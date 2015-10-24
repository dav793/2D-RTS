using UnityEngine;
using System.Collections;

public class KeyboardInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.U)) {
			WorldRenderer.WRENDERER.toggleDebugUIMode ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			WorldRenderer.WRENDERER.triggerActiveCellUpdate ();
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			World.GWORLD.executeTick ();
			WorldRenderer.WRENDERER.triggerActiveCellUpdate ();
		}

	}

}
