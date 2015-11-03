using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *	Class: World
 *
 *	Data structure which contains all cells, and operates on them every tick to modify their resource quantities 
 *	and recalculate their resource rates of change.
 *
 */
public class World : MonoBehaviour {

	public static World GWORLD;

	[HideInInspector] public bool initialized = false;

	WorldCell[,] cells;

	void Awake() {
		if (GWORLD == null) {
			GWORLD = this;
		} 
		else if (GWORLD != this) {
			Destroy(gameObject);
		}
	}

	public void Init() {
		InitCells ();
		initialized = true;
	}

	void InitCells() {
		cells = new WorldCell[GameData_Config.CONFIG.WORLD_CELLS_X, GameData_Config.CONFIG.WORLD_CELLS_Y];
		for (int x = 0; x < GameData_Config.CONFIG.WORLD_CELLS_X; ++x) {
			for (int y = 0; y < GameData_Config.CONFIG.WORLD_CELLS_Y; ++y) {
				cells[x, y] = WorldCell.GetNew(x, y);
			}
		}
	}

	public void executeTick() {
		//Debug.Log ("Executing world tick");
		if (initialized) {
			for (int x = 0; x < GameData_Config.CONFIG.WORLD_CELLS_X; ++x) {
				for (int y = 0; y < GameData_Config.CONFIG.WORLD_CELLS_Y; ++y) {
					cells [x, y].executeTick ();
				}
			}
		}
	}

	public WorldCell getCell(Coordinates cell_coords) {
		if (checkCoordinateIntegrity (cell_coords)) {
			return cells[cell_coords.x, cell_coords.y];
		}
		return null;
	}

	bool checkCoordinateIntegrity(Coordinates cell_coords) {
		if(cell_coords.x >= 0 && cell_coords.x < cells.GetLength(0) && cell_coords.y >= 0 && cell_coords.y < cells.GetLength(1)) {
			return true;
		}
		Debug.LogError ("Invalid cell range.");
		return false;
	}

	void debug() {
		//Debug.Log (cells.GetLength(0) + ", " + cells.GetLength(1));
		//Resource res = RES_Wood.GetNew ();
		//Debug.Log (res.Name);
		//Debug.Log (cells[15,30].X + ", " + cells[15,30].Y);

		/*List<KeyValuePair<Resource, ResourceCellData>> entries = cells [0, 0].ContainedResources.getAllEntries ();
		for (int i = 0; i < entries.Count; ++i) {
			Debug.Log(entries[i].Key.Name + " -> qty:" + entries[i].Value.Quantity + ", roc: " + entries[i].Value.RateOfChange);
		}*/


	}

}
