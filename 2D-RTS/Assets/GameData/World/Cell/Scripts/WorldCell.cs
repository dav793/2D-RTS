﻿using UnityEngine;
using System;
using System.Collections;

/*
 *	Class: WorldCell
 *
 *	Data structure which represents a single cell in the world.
 *	It keeps track of all resources in the cell, as well as their values within it (quantities and rates of change).
 *
 */
public class WorldCell {

	int _x;
	int _y;
	CellResourcesData _contained_resources;		

	// Renderer vars
	GameObject renderedGameObject = null;
	CellHandler handler;

	// Class instantiator (replaces the class constructor)
	public static WorldCell GetNew(int index_x, int index_y) {
		WorldCell temp = new WorldCell ();				// instantiate
		temp.Init (index_x, index_y);					// initialize
		return temp;
	}

	// X coordinate of cell in the world
	public int X {
		get { return _x; }
		private set { _x = value; }
	}

	// Y coordinate of cell in the world
	public int Y {
		get { return _y; }
		private set { _y = value; }
	}

	// Contains references for all resources in the cell and their values within it
	public CellResourcesData ContainedResources {
		get { return _contained_resources; }
		private set { _contained_resources = value; }
	}

	void Init(int index_x, int index_y) {
		X = index_x;
		Y = index_y;
		ContainedResources = CellResourcesData.GetNew();
	}

	public void executeTick() {
		//Debug.Log ("executing tick: ("+X+","+Y+")");
		ContainedResources.updateResourceValues ();
	}

	// RENDERER PROCEDURES

	public bool isRendered() {
		if(renderedGameObject != null) {
			return true;
		}
		return false;
	}

	public void render(GameObject rendererObj) {
		if(!isRendered()) {
			renderedGameObject = rendererObj;
			renderedGameObject.name = "Cell";
			handler = renderedGameObject.GetComponent<CellHandler> ();
			handler.Init ();

			if(WorldRenderer.WRENDERER.debugUIModeIsActive()) {
				activateUI();
			}
		}
	}

	public void unrender() {
		deactivateUI ();
		handler = null;
		renderedGameObject = null;
	}

	public GameObject getRenderedGameObject() {
		if (isRendered()) {
			return renderedGameObject;
		}
		return null;
	}

	public void updateUI () {
		if (isRendered ()) {
			handler.updateUIData(ContainedResources);
		}
	}

	public void activateUI() {
		if (isRendered ()) {
			handler.activateUI();
			updateUI ();
		}
	}

	public void deactivateUI() {
		if (isRendered ()) {
			handler.deactivateUI();	
		}
	}
}
