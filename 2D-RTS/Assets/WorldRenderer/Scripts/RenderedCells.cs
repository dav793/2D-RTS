using UnityEngine;
using System.Collections;

public class RenderedCells {

	CellRange range;

	public RenderedCells(int cells_size_x, int cells_size_y) {
		range = new CellRange ();
	}

	public void setCellRange (Coordinates new_bottom_left, Coordinates new_top_right) {
		if (checkCoordinatesIntegrity (new_bottom_left, new_top_right) && !checkRangesEqual(new_bottom_left, new_top_right)) {
			// New coordinates are valid and different from current coordinates

			if(!checkRangeSizeEquality(new_bottom_left, new_top_right)) {
				// Range size has changed. Revert range to default to avoid errors. Unrender all rendered cells and render all cells in range.
				unrenderAllRenderedCells();
				range = new CellRange();
				renderAllCellsInRange(new_bottom_left, new_top_right);
			}
			else {
				// Range size is still the same. Unrender cells no longer in range and render new cells in range.
				unrenderCellsNoLongerInRange(new_bottom_left, new_top_right);
				renderNewCellsInRange(new_bottom_left, new_top_right);

			}
			
			// Set new cell range
			range.setBoundary (CellRangeBoundaries.BOTTOM_LEFT, new_bottom_left);
			range.setBoundary (CellRangeBoundaries.TOP_RIGHT, new_top_right);
			
		}
	}

	bool checkCoordinatesIntegrity(Coordinates bottom_left, Coordinates top_right) {
		if (bottom_left.x <= top_right.x && bottom_left.y <= top_right.y) {
			if(bottom_left.x >= 0 && top_right.x < GameData_Config.CONFIG.WORLD_CELLS_X && bottom_left.y >= 0 && top_right.y < GameData_Config.CONFIG.WORLD_CELLS_Y) {
				return true;
			}
		}
		Debug.LogError ("Invalid cell range.");
		return false;
	}

	bool checkRangesEqual(Coordinates bottom_left, Coordinates top_right) {
		if(range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).isEqual(bottom_left) && range.getBoundary(CellRangeBoundaries.TOP_RIGHT).isEqual(top_right)) {
			return true;
		}
		return false;
	}

	bool checkRangeSizeEquality(Coordinates new_bottom_left, Coordinates new_top_right) {

		int old_range_size_x = CellRange.GetRangeSize_X(range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT), range.getBoundary(CellRangeBoundaries.TOP_RIGHT));
		int old_range_size_y = CellRange.GetRangeSize_Y(range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT), range.getBoundary(CellRangeBoundaries.TOP_RIGHT));
		int new_range_size_x = CellRange.GetRangeSize_X(new_bottom_left, new_top_right);
		int new_range_size_y = CellRange.GetRangeSize_Y(new_bottom_left, new_top_right);
		
		if(old_range_size_x == new_range_size_x && old_range_size_y == new_range_size_y) {
			return true;
		}

		return false;

	}

	void renderAllCellsInRange(Coordinates bottom_left, Coordinates top_right) {
		for (int y = bottom_left.y; y <= top_right.y; ++y) {
			for (int x = bottom_left.x; x <= top_right.x; ++x) {
				// render cell in current (x, y)
				WorldRenderer.WRENDERER.renderCell(new Coordinates(x, y));
			}
		}
	}

	void unrenderAllRenderedCells() {
		for (int y = range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).y; y <= range.getBoundary(CellRangeBoundaries.TOP_RIGHT).y; ++y) {
			for (int x = range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).x; x <= range.getBoundary(CellRangeBoundaries.TOP_RIGHT).x; ++x) { 
				// unrender cell in current (x, y)
				WorldRenderer.WRENDERER.unrenderCell(new Coordinates(x, y));
			}
		}
	}

	void unrenderCellsNoLongerInRange(Coordinates new_bottom_left, Coordinates new_top_right) {
		
		for(int old_y = range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).y; old_y <= range.getBoundary(CellRangeBoundaries.TOP_RIGHT).y; ++old_y) {
			
			int? old_x_left = null;
			int? old_x_right = null;
			
			if(old_y < new_bottom_left.y || old_y > new_top_right.y) {
				// no overlapping between old and new ranges on current Y
				old_x_left = range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).x;
				old_x_right = range.getBoundary(CellRangeBoundaries.TOP_RIGHT).x;
			}
			else {
				// old and new ranges overlap on some portion of current Y
				if(range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).x > new_top_right.x || range.getBoundary(CellRangeBoundaries.TOP_RIGHT).x < new_bottom_left.x) {
					// no overlapping between old and new ranges on any X in current Y
					old_x_left = range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).x;
					old_x_right = range.getBoundary(CellRangeBoundaries.TOP_RIGHT).x;
				} 
				else {
					// old and new ranges overlap on some portion of X in current Y
					if(range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).x < new_bottom_left.x) {
						old_x_left = range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).x;
						old_x_right = new_bottom_left.x - 1;
					} else if (range.getBoundary(CellRangeBoundaries.TOP_RIGHT).x > new_top_right.x) {
						old_x_left = new_top_right.x + 1;
						old_x_right = range.getBoundary(CellRangeBoundaries.TOP_RIGHT).x;
					}
				}
			}
			
			if(old_x_left != null && old_x_right != null) {
				for(int old_x = (int)old_x_left; old_x <= (int)old_x_right; ++old_x) {
					// unrender cell in current (x, y)
					WorldRenderer.WRENDERER.unrenderCell(new Coordinates(old_x, old_y));
				}
			} 
			else {
				Debug.LogError("Something went wrong here.");
			}
			
		}
		
	}
	
	void renderNewCellsInRange(Coordinates new_bottom_left, Coordinates new_top_right) {
		
		for(int new_y = new_bottom_left.y; new_y <= new_top_right.y; ++new_y) {

			int? new_x_left = null;
			int? new_x_right = null;

			if(new_y < range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).y || new_y > range.getBoundary(CellRangeBoundaries.TOP_RIGHT).y) {
				// no overlapping between old and new ranges on current Y
				new_x_left = new_bottom_left.x;
				new_x_right = new_top_right.x;
			}
			else {
				// old and new ranges overlap on some portion of current Y
				if(new_bottom_left.x > range.getBoundary(CellRangeBoundaries.TOP_RIGHT).x || new_top_right.x < range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).x) {
					// no overlapping between old and new ranges on any X in current Y
					new_x_left = new_bottom_left.x;
					new_x_right = new_top_right.x;
				} 
				else {
					// old and new ranges overlap on some portion of X in current Y
					if(new_bottom_left.x < range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).x) {
						new_x_left = new_bottom_left.x;
						new_x_right = range.getBoundary(CellRangeBoundaries.BOTTOM_LEFT).x - 1;
					} else if (new_top_right.x > range.getBoundary(CellRangeBoundaries.TOP_RIGHT).x) {
						new_x_left = range.getBoundary(CellRangeBoundaries.TOP_RIGHT).x + 1;
						new_x_right = new_top_right.x;
					}
				}
			}

			if(new_x_left != null && new_x_right != null) {
				for(int new_x = (int)new_x_left; new_x <= (int)new_x_right; ++new_x) {
					// render cell in current (x, y)
					WorldRenderer.WRENDERER.renderCell(new Coordinates(new_x, new_y));
				}
			} 
			else {
				Debug.LogError("Something went wrong here.");
			}
			
		}
		
	}

}

public class CellRange {

	Coordinates[] boundaries;

	public CellRange() {
		boundaries = new Coordinates[2];
		setBoundary (CellRangeBoundaries.BOTTOM_LEFT, Coordinates.Zero()); 
		setBoundary (CellRangeBoundaries.TOP_RIGHT, Coordinates.Zero()); 
	}

	public void setBoundary(CellRangeBoundaries boundary, Coordinates coords) {
		boundaries [(int)boundary] = coords;
	}

	public Coordinates getBoundary(CellRangeBoundaries boundary) {
		return boundaries [(int)boundary];
	}

	public static int GetRangeSize_X(Coordinates bottom_left, Coordinates top_right) {
		return Mathf.Abs (top_right.x - bottom_left.x);
	}

	public static int GetRangeSize_Y(Coordinates bottom_left, Coordinates top_right) {
		return Mathf.Abs (top_right.y - bottom_left.y);
	}

}

public class Coordinates {

	public int x;
	public int y;

	public Coordinates(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public bool isEqual(Coordinates coordinate) {
		if(coordinate.x == x && coordinate.y == y) {
			return true;
		}
		return false;
	}

	public override string ToString () {
		return "Coordinate: ("+x+","+y+")";
	}

	public static Coordinates Zero() {
		return new Coordinates (0, 0);
	}

}

public enum CellRangeBoundaries { BOTTOM_LEFT, TOP_RIGHT };