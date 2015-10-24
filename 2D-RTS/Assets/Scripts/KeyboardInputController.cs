using UnityEngine;
using System.Collections;

public class KeyboardInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.U)) {
			toggleDebugUIMode();
		}
	}

	void toggleDebugUIMode() {
		WorldRenderer.WRENDERER.toggleDebugUIMode();
	}

}
