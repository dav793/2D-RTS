using UnityEngine;
using System.Collections;

/*
 *	Class: ResourceCellData
 *
 *	Data structure which contains a resources values in relation to one specific cell in the world.
 *	
 *	Every cell in the world keeps one instance of this object for every resource it contains. Said instance holds the
 *	data which specifies the values of that resource within the cell.
 *
 *	For example, for a resource A contained in a cell B, this data structure would hold the quantity
 *	of resource A in cell B, and the rate of change for resource A in cell B.
 *
 */
public class ResourceCellData {

	float _quantity;
	float _rate_of_change;

	public static ResourceCellData GetNew() {
		ResourceCellData temp = new ResourceCellData ();		// instantiate
		temp.Init ();											// initialize
		return temp;
	}

	// Quantity of resource contained in cell
	public float Quantity {
		get { return _quantity; }
		set { _quantity = value; }
	}

	// Rate of change of resource in cell
	public float RateOfChange {
		get { return _rate_of_change; }
		set { _rate_of_change = value; }
	}

	void Init() {
		Quantity = 1.5f;
		RateOfChange = 0.5f;
	}

}
