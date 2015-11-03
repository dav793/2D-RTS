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

	public bool paused = false;
	public float tick_speed = 1.0f;

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
		World.GWORLD.Init ();
		StartCoroutine("executeTick");
	}

	public void togglePause() {
		paused = !paused;
	}

	public void increaseSpeed() {
		if(tick_speed < 10f) {
			tick_speed += 1.0f;
			//Debug.Log(tick_speed+": "+(GameData_Config.CONFIG.TICK_LENGTH*0.001f)/tick_speed);
		}
	}

	public void decreaseSpeed() {
		if(tick_speed >= 2.0f) {
			tick_speed -= 1.0f;
			//Debug.Log(tick_speed+": "+(GameData_Config.CONFIG.TICK_LENGTH*0.001f)/tick_speed);
		}
	}

	IEnumerator executeTick() {

		while(true)
		{
			if (!paused) {
				World.GWORLD.executeTick ();
				WorldRenderer.WRENDERER.triggerActiveCellUpdate ();
			}
			yield return new WaitForSeconds((GameData_Config.CONFIG.TICK_LENGTH*0.001f)/tick_speed);
		}

	}

}
