using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 *	Class: WorldRenderer
 *
 *	Constructs the game world visualization in the Unity scene.
 *
 */
public class WorldRenderer : MonoBehaviour {

	public static WorldRenderer WRENDERER;

	[HideInInspector] public bool initialized = false;
	bool debug_ui_active = false;

	public GameObjectPool CellPool;

	RenderedCells rendered_cells;

	void Awake() {
		if (WRENDERER == null) {
			WRENDERER = this;
		} 
		else if (WRENDERER != this) {
			Destroy(gameObject);
		}
	}

	void FixedUpdate () {
		if (!initialized) {
			// Renderer not yet initialized
			if (checkWorldInitialization ()) {
				Init ();			//  Renderer only initializes if world has already been initialized
			}
		} 
		else {
			// Renderer initialized
			// update rendered cells
		}
	}
	
	void Init() {
		checkIntegrity ();
		CellPool.Init (GameData_Config.CONFIG.WORLD_CELLS_X * GameData_Config.CONFIG.WORLD_CELLS_Y);
		rendered_cells = new RenderedCells (GameData_Config.CONFIG.WORLD_CELLS_X, GameData_Config.CONFIG.WORLD_CELLS_Y);

		rendered_cells.setCellRange (
			new Coordinates(0, 0), 
			new Coordinates(GameData_Config.CONFIG.WORLD_CELLS_X - 1, GameData_Config.CONFIG.WORLD_CELLS_Y - 1)
		);

		/*rendered_cells.setCellRange (
			new Coordinates(0, 0), 
			new Coordinates(1, 1)
		);

		rendered_cells.setCellRange ( 
			new Coordinates(1, 1),
		    new Coordinates(2, 2)
		);*/

		initialized = true;
	}

	bool checkWorldInitialization() {
		if (World.GWORLD.initialized)
			return true;
		else
			return false;
	}

	void checkIntegrity() {
		if (CellPool == null) 
			throw new InvalidOperationException("Renderer integrity check failed: No cell pool assigned.");
	}

	public void renderCell(Coordinates cell_coords) {
		WorldCell cell = World.GWORLD.getCell (cell_coords);
		renderCell (cell);
	}

	public void renderCell(WorldCell cell) {
		if (!cell.isRendered ()) {
			//Debug.Log("Rendering cell ("+cell.X+", "+cell.Y+")");
			cell.render (CellPool.pop ());
			placeCellGameObject (cell);
		}
	}

	public void unrenderCell(Coordinates cell_coords) {
		WorldCell cell = World.GWORLD.getCell (cell_coords);
		unrenderCell (cell);
	}

	public void unrenderCell(WorldCell cell) {
		if (cell.isRendered ()) {
			//Debug.Log("Unrendering cell ("+cell.X+", "+cell.Y+")");
			CellPool.push (cell.getRenderedGameObject ());
			cell.unrender ();
		}
	}

	void placeCellGameObject(WorldCell cell) {
		GameObject cellRObj = cell.getRenderedGameObject ();
		cellRObj.transform.position = new Vector3 (
			cell.X * GameData_Config.CONFIG.CELL_LENGTH, 
			cell.Y * GameData_Config.CONFIG.CELL_LENGTH,
			cellRObj.transform.position.z
		);
	}

	public bool debugUIModeIsActive() {
		return debug_ui_active;
	}

	public void toggleDebugUIMode() {
		if (initialized) {
			if(debug_ui_active) {
				deactivateDebugUIMode();
			}
			else {
				activateDebugUIMode();
			}
		}
	}

	void activateDebugUIMode() {

		List<WorldCell> active_cells = rendered_cells.getActiveCells ();

		for (int i = 0; i < active_cells.Count; ++i) {
			active_cells[i].activateUI ();
		}

		debug_ui_active = true;
		//Debug.Log ("debug mode activated");
	}

	void deactivateDebugUIMode() {

		List<WorldCell> active_cells = rendered_cells.getActiveCells ();

		for (int i = 0; i < active_cells.Count; ++i) {
			active_cells[i].deactivateUI ();
		}

		debug_ui_active = false;
		//Debug.Log ("debug mode deactivated");
	}

}
