using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellUIHandler : MonoBehaviour {

	public Canvas Canvas;
	public Transform ContentPanel;
	public GameObject UITextPanel_Prefab;

	Dictionary<string, CellUITextPanelHandler> resource_panels;

	public void Init() {
		RectTransform rect = Canvas.GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2 (GameData_Config.CONFIG.CELL_LENGTH - 2, GameData_Config.CONFIG.CELL_LENGTH - 2);
	}

	void setResourceQty(string resource, string qty) {
		if(!resource_panels.ContainsKey (resource)) {
			addResource(resource);
		}

		CellUITextPanelHandler text_panel;
		resource_panels.TryGetValue (resource, out text_panel);
		text_panel.QtyLabel.text = qty;
	}

	void setResourceROC(string resource, string roc) {
		if(!resource_panels.ContainsKey (resource)) {
			addResource(resource);
		}

		CellUITextPanelHandler text_panel;
		resource_panels.TryGetValue (resource, out text_panel);
		text_panel.ROCLabel.text = roc;
	}

	void removeResource(string resource) {
		CellUITextPanelHandler text_panel;
		resource_panels.TryGetValue (resource, out text_panel);
		if(text_panel != null) {
			resource_panels.Remove(resource);
			Destroy(text_panel.gameObject);
		}
	}

	void addResource(string resource) {
		resource_panels.Add(resource, addTextPanel());

		CellUITextPanelHandler text_panel;
		resource_panels.TryGetValue (resource, out text_panel);
		text_panel.ResourceLabel.text = resource;
	}

	CellUITextPanelHandler addTextPanel() {
		GameObject textPanel = Instantiate (UITextPanel_Prefab) as GameObject;
		textPanel.transform.SetParent (ContentPanel);
		return textPanel.GetComponent<CellUITextPanelHandler>();
	}

	void removeAllTextPanels() {
		foreach (KeyValuePair<string, CellUITextPanelHandler> entry in resource_panels) {
			Destroy(entry.Value.gameObject);
		}
	}

	public void activate() {
		Canvas.gameObject.SetActive (true);
		if(resource_panels == null) {
			resource_panels = new Dictionary<string, CellUITextPanelHandler> ();
		}
	}
	
	public void deactivate() {
		removeAllTextPanels ();
		resource_panels = null;
		Canvas.gameObject.SetActive (false);
	}

	public void updateUIData(CellResourcesData contained_resources) {

		Dictionary<Resource, ResourceCellData> data = contained_resources.getData ();

		//remove old resources
		foreach (KeyValuePair<string, CellUITextPanelHandler> entry in resource_panels) {
			if (!cellContainsResource(contained_resources, entry.Key)) {
				removeResource (entry.Key);
			}
		}

		//update resource values
		foreach (KeyValuePair<Resource, ResourceCellData> entry in data) {
			setResourceQty(entry.Key.Name, entry.Value.Quantity.ToString());
			setResourceROC(entry.Key.Name, entry.Value.RateOfChange.ToString());
		}

	}

	bool cellContainsResource(CellResourcesData contained_resources, string resource) {
		Dictionary<Resource, ResourceCellData> data = contained_resources.getData ();
		foreach (KeyValuePair<Resource, ResourceCellData> entry in data) {
			if(entry.Key.Name == resource) {
				return true;
			}
		}
		return false;
	}

}
