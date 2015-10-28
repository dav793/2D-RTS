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

	float _max_quantity = 100.0f;
	float _quantity;
	float _rate_of_change;

	// coefficients for rate of growth function
	float _qty_pivot_point = 50f;
	float _roc_peak = 5f;

	// predefined constants for rate of growth function
	float a = 100;
	float b1 = 10;
	float b2 = 10;
	float c = 50;

	public static ResourceCellData GetNew() {
		ResourceCellData temp = new ResourceCellData ();		// instantiate
		temp.Init ();											// initialize
		return temp;
	}

	// Max quantity of resource contained in cell
	public float MaxQuantity {
		get { return _max_quantity; }
		set { _max_quantity = value; }
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

	public float QtyPivot {
		get { return _qty_pivot_point; }
		set { _qty_pivot_point = value; }
	}
	
	public float ROCPeak {
		get { return _roc_peak; }
		set { _roc_peak = value; }
	}

	void Init() {
		Quantity = 1f;
		RateOfChange = 0f;
	}

	public void updateQuantity() {
		Quantity = Quantity + RateOfChange;
		Mathf.Clamp (Quantity, 0f, MaxQuantity);
	}

	public void updateROC() {
		RateOfChange = getBaseROCValue (Quantity);
	}

	float getBaseROCValue(float quantity) {

		float numerator = 0f;
		float denominator = 1f;

		if (quantity >= MaxQuantity) {
			// max quantity reached
			return 0f;
		}

		if (quantity <= QtyPivot) {
			numerator = ROCPeak * a;
			denominator = Mathf.Pow( Mathf.Sqrt( Mathf.Pow(quantity - QtyPivot, 2) ) + b1, 2 );
		} 
		else {
			numerator = (ROCPeak / 2) * a;
			denominator = Mathf.Pow( Mathf.Sqrt(quantity - QtyPivot) + b2, 2 ) - c;
		}

		return numerator / denominator;

	}

}
