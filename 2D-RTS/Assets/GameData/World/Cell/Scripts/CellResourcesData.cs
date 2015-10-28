using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 *	Class: CellResourcesData
 *
 *	Data structure which contains every resource in one specific cell in the world, as well as their values within said cell.
 *
 *	Every cell in the world holds exactly one instance of this object. Said instance contains the data for every resource
 *	within the cell, and the values of that resource within the cell.
 *	
 *	For any cell which contains any number of resources, said cell would keep track of its contained resources as well as 
 *	their quantities and rates of change by holding that data within an instance of this data structure.
 *
 */

public class CellResourcesData {

	Dictionary<Resource, ResourceCellData> data;		// has one entry for every resource contained in the cell

	// Class instantiator (replaces the class constructor)
	public static CellResourcesData GetNew() {
		CellResourcesData temp = new CellResourcesData ();		// instantiate
		temp.Init ();											// initialize
		return temp;
	}

	public List< KeyValuePair<Resource, ResourceCellData> > getAllEntries() {
		List< KeyValuePair<Resource, ResourceCellData> > entries = new List< KeyValuePair<Resource, ResourceCellData> > ();
		foreach (KeyValuePair<Resource, ResourceCellData> entry in data) {
			entries.Add (entry);
		}
		return entries;
	}

	public Dictionary<Resource, ResourceCellData> getData() {
		return data;
	}

	void Init() {
		data = new Dictionary<Resource, ResourceCellData> ();
		data.Add (RES_Fire.GetNew(), ResourceCellData.GetNew());
		data.Add (RES_Water.GetNew(), ResourceCellData.GetNew());
		data.Add (RES_Wood.GetNew(), ResourceCellData.GetNew());
		data.Add (RES_Grass.GetNew(), ResourceCellData.GetNew());
		data.Add (RES_Stone.GetNew(), ResourceCellData.GetNew());
	}

	public void updateResourceValues() {
		foreach (KeyValuePair<Resource, ResourceCellData> entry in data) {
			entry.Value.updateQuantity ();
			entry.Value.updateROC ();
			//float new_qty = entry.Value.Quantity + entry.Value.RateOfChange;
			//entry.Value.Quantity = new_qty; 
		}
	}

}
