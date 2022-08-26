using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

public class DragAndDropPanelSettings : MonoBehaviour {
	DragDropManager DDM;
	
	// the Id of this panel
	public string Id;

	// Enums
	public enum ObjectPosStates { UseObjectSettings, DroppedPosition, PanelPosition };
	public enum ObjectLockStates { UseObjectSettings, LockObject, DoNotLockObject  };
	public enum ObjectReplace { Allowed, NotAllowed, MultiObjectMode  };
	//

	// Customization Tools
	[Header ("Object Position")]
	[Tooltip ("Customize the position of object when dropped on this panel")]
	public ObjectPosStates ObjectPosition;

	[Header ("Lock Object")]
	[Tooltip ("Customize Object Locking")]
	public ObjectLockStates LockObject;

	[Header ("Replacement & Multi Object")]
	[Tooltip ("Allow Object to Replace & Switch or use Multi Object Mode")]
	public ObjectReplace ObjectReplacement;
	//

	[Header ("Events Management")]
	[Tooltip ("When any object dropped on the panel, the functions that you added to this event trigger will be called")]
	public UnityEvent OnObjectDropped;

	[HideInInspector]
	// using for Multi Object tool
	public List<string> PanelIdManager;
	
	void Start () {
		// Getting current DDM GameObject
		DDM = GameObject.Find ("DDM").GetComponent<DragDropManager> ();
	}

	public void SetupPanelEvents () {
		// Events Management
		if (OnObjectDropped != null) {
			OnObjectDropped.Invoke ();
		}
	}

	public void SetMultiObject (string ObjectId) {
		// Adding new object to the list of dropped objects
		PanelIdManager.Add (ObjectId);

		if (DDM.SaveStates) {
			SaveObjectsList ();
		}
	}

	public void RemoveMultiObject (string ObjectId) {
		// Removing an object from list of dropped objects
		if (DDM.SaveStates) {
			PlayerPrefs.DeleteKey (Id + "&&" + (PanelIdManager.Count - 1).ToString ());
		}

		PanelIdManager.Remove (ObjectId);
	}

	void SaveObjectsList () {
		for (int i = 0; i < PanelIdManager.Count; i++) {
			PlayerPrefs.SetString (Id + "&&" + i.ToString (), PanelIdManager [i]);
		}
	}

	public void LoadObjectsList () {
		// loading list of dropped objects
		for (int i = 0; i < DDM.AllObjects.Length; i++) {
			if (PlayerPrefs.HasKey (Id + "&&" + i.ToString ())) {
				PanelIdManager.Add (PlayerPrefs.GetString (Id + "&&" + i.ToString ()));
			}
		}

		for (int i = 0; i < PanelIdManager.Count; i++) {
			for (int j = 0; j < DDM.AllObjects.Length; j++) {
				
				if (DDM.AllObjects [j].Id == PanelIdManager [i]) {
					DDM.AllObjects [j].GetComponent <RectTransform> ().SetAsLastSibling ();

					for (int k = 0; k < DDM.AllPanels.Length; k++) {
						if (DDM.AllPanels [k].Id == Id) {
							DDM.SetPanelObject (k, DDM.AllObjects [j].Id);
						}
					}
				}
			}
		}
	}
}