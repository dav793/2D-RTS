using UnityEngine;
using System.Collections;

/*
 *	Class: GameData_Config
 *
 *	Game configuration data structure. Holds settings specific to this match.
 *
 */
public class GameData_Config : MonoBehaviour {

	public static GameData_Config CONFIG;

	public int WORLD_CELLS_X;			// Amount of horizontal cells in the world
	public int WORLD_CELLS_Y;			// Amount of vertical cells in the world
	public int CELL_LENGTH;				// Length of a cell side (in pixels)
	public int TICK_LENGTH;				// Length or duration of a game tick (in msecs)

	void Awake() {
		if (CONFIG == null) {
			CONFIG = this;
		} 
		else if (CONFIG != this) {
			Destroy(gameObject);
		}
	}

}
