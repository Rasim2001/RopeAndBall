using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropObjectEvents : EventTrigger {

    // Object Setting
    DragAndDropObjectSettings OS;

	// DDM Game Object
	DragDropManager DDM;

	void Start () {
		// Getting the Object Setting that assigned to this GameObject
		OS = GetComponent<DragAndDropObjectSettings> (); 

		// Getting DDM GameObject
		DDM = GameObject.Find ("DDM").GetComponent<DragDropManager> (); 
	}

	public override void OnPointerDown (PointerEventData eventData) {
		if (!OS.OnReturning) {
			if (DDM.TargetPlatform == DragDropManager.Platforms.PC) {
				// for PC
				if (eventData.button == PointerEventData.InputButton.Left) {
					OS.PointerDown ("User", "");
				}
			} else {
				// for Mobile
				if (Input.touchCount == 1) {
					OS.PointerDown ("User", "");
				}
			}
		}
	}

	public override void OnPointerUp (PointerEventData eventData) {
		if (!OS.OnReturning) {
			if (DDM.TargetPlatform == DragDropManager.Platforms.PC) {
				// for PC
				if (eventData.button == PointerEventData.InputButton.Left) {
					OS.PointerUp ();
				}
			} else {
				// for Mobile
				if (Input.touchCount == 1) {
					OS.PointerUp ();
				}
			}
		}
	}
}
