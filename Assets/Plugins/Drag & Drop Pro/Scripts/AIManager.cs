using UnityEngine;

public class AIManager : MonoBehaviour {
	// By using this component, you can simulate the drag and drop system.

	[Range (0.1f, 2.0f)]
	public float MovementSpeed = 1.0f;

	static DragDropManager DDM;

	void Start () {
		// Getting DDM GameObject
		DDM = GameObject.Find ("DDM").GetComponent<DragDropManager> ();
	}

	public static void AIDragDrop (string ObjectId, string PanelId) {	
		for (int i = 0; i < DDM.AllObjects.Length; i++) {
			
			if (ObjectId == DDM.AllObjects [i].Id) {
				if (!DDM.AllObjects [i].OnReturning) {
					DDM.AllObjects [i].PointerDown ("AI", PanelId);
				}
			}
		}
	}
}
