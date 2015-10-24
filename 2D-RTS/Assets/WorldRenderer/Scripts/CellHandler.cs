using UnityEngine;
using System.Collections;

public class CellHandler : MonoBehaviour {

	CellUIHandler UIHandler;
	
	public void Init() {
		UIHandler = GetComponent<CellUIHandler> ();
		UIHandler.Init ();
	}

	public void activateUI() {
		UIHandler.activate ();
	}

	public void deactivateUI() {
		UIHandler.deactivate ();
	}

	public void updateUIData(CellResourcesData contained_resources) {
		UIHandler.updateUIData (contained_resources);
	}

}
