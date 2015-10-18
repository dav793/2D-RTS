using UnityEngine;
using System.Collections;

/*
 *	Class: GameData
 *
 *	Game supervisor. Initializes game world and controls game progression (ticks).
 *
 */
public class GameData : MonoBehaviour {

	public static GameData GDATA;

	void Awake() {
		if (GDATA == null) {
			GDATA = this;
			//DontDestroyOnLoad (GDATA);
		} 
		else if (GDATA != this) {
			Destroy(gameObject);
		}
	}
	
	void Start () {
	
	}

	void Update () {
	
	}

}
