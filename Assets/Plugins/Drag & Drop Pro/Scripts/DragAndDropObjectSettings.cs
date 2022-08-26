using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class DragAndDropObjectSettings : MonoBehaviour {

	// DDM Game Object
	DragDropManager DDM;

	// Rect Transform Component of this object
	RectTransform thisRT;

	// Parent of this object
	Transform thisParent;

	// Vector3 variables
	Vector3 CurrentPos;
	Vector3 MousePos;
	[HideInInspector]
	public Vector3 FirstPos;
	[HideInInspector]
	public Vector3 FirstScale;
	

	// Bools
	bool PDown = false;
	bool CheckStatus = false;
	[HideInInspector]
	public bool DefaultPanel = false;
	[HideInInspector]
	public bool Dropped = false;
	[HideInInspector]
	public bool OnReturning = false;
	//

	//using for dragging mode
	float differenceX;
	float differenceY;
	//

	// Customization Tools
	public string Id; 	// the Id of this object

	[Tooltip ("Allow user to control this object")]
	public bool UserControl = true;

	[Tooltip ("Allow AI to control this object")]
	public bool AIControl = true;

	[Header ("Return Object Smoothly (DragDrop Failed)")]
	[Tooltip ("Return Object to its first Position Smoothly When DragDrop Failed")]
	public bool ReturnSmoothly = false;
	[Tooltip ("Returning speed")]
	[Range (0.1f, 2.0f)]
	public float ReturnSpeed = 1.0f;

	[Header ("Scale Object (Dragging)")]
	[Tooltip ("Scale Object When dragging was begun")]
	public bool ScaleOnDrag = false;
	[Tooltip ("Object Scale")]
	public Vector3 DragScale = new Vector3 (1.0f, 1.0f, 1.0f);

	[Header ("Stay Object on dropped position")]
	[Tooltip ("Stay Object on dropped position When the Object dropped successfully")]
	public bool StayDroppedPos = false;

	[Header ("Scale Object (Dropped successfully)")]
	[Tooltip ("Scale Object When dropped successfully")]
	public bool ScaleOnDropped = false;

	[Header ("Locking Object")]
	[Tooltip ("Lock Object When dropped successfully")]
	public bool LockObject = false;

	[Header ("Return Object (Dropped successfully)")]
	[Tooltip ("Return Object to its first position When dropped successfully")]
	public bool ReturnObject = false;

	[Header ("Smooth Replacement")]
	[Tooltip ("Replace Object smoothly When dropped successfully")]
	public bool ReplaceSmoothly = false;
	[Range (0.1f, 2.0f)]
	public float ReplacementSpeed = 1.0f;

	[Header ("Allow to switch Objects")]
	[Tooltip ("Allow to switch Objects between panels")]
	public bool SwitchObjects = false;
	[Tooltip ("Move Object smoothly When it was switching")]
	public bool MoveSmoothly = false;
	[Range (0.1f, 2.0f)]
	public float MovementSpeed = 1.0f;

	[Header ("Filter Panels")]
	[Tooltip ("Allow using Filter Panels tool")]
	public bool FilterPanels = false;
	[Tooltip ("The Ids of the panels that object allowed to drop on them")]
	public string[] AllowedPanels;

	[Header ("Events Management")]
	public UnityEvent OnBeginDragging;
	public UnityEvent OnDragDropFailed;
	public UnityEvent OnDroppedSuccessfully;
	//

	void Start () {
		GetGameObjects ();
        GetComponent<Image>().preserveAspect = true;
    }

	void GetGameObjects () {
		// Getting Rect Transform component of this object
		thisRT = GetComponent<RectTransform> (); 

		// Getting the parent of this object
		thisParent = thisRT.parent;

		// Getting DDM GameObject
		DDM = GameObject.Find ("DDM").GetComponent<DragDropManager> ();
	}

	void Update () {
		DragObject ();
	}

	void DragObject () {
		if (PDown) {
			SetMousePos ();

			if (DDM.DraggingModes == DragDropManager.DragModes.ChangeToMousePos) {
				thisRT.position = new Vector3 (MousePos.x, MousePos.y, thisRT.position.z);
			} else {
				thisRT.position = new Vector3 (MousePos.x + differenceX, MousePos.y + differenceY, thisRT.position.z);
			}
		}
	}

	public void PointerDown (string state, string AIPanel) {
		if (state == "User" && UserControl) {
			PointerDownActions (state, AIPanel);
		} else if (state == "AI" && AIControl) {
			PointerDownActions (state, AIPanel);
		}
	}

	void PointerDownActions (string state, string AIPanel) {
		if (Dropped) {
			// Setup customization tools of the panel
			for (int i = 0; i < DDM.AllPanels.Length; i++) {
				if (DDM.IdManager[i] == Id) {
					if (DDM.AllPanels [i].LockObject != DragAndDropPanelSettings.ObjectLockStates.LockObject) {
						if (!LockObject || DDM.AllPanels [i].LockObject == DragAndDropPanelSettings.ObjectLockStates.DoNotLockObject) {
							BeginDragging (state, AIPanel);
						}
					}
				}
			}
		} else {
			BeginDragging (state, AIPanel);
		}
	}

	void BeginDragging (string state, string AIPanel) {
		CurrentPos = thisRT.position;

		if (ScaleOnDrag) { // Setup ScaleOnDrag tool
			thisRT.localScale = DragScale;
		}

		thisRT.SetParent (DDM.SecondCanvas.GetComponent<RectTransform> ());

		if (state == "User") {

			if (DDM.DraggingModes == DragDropManager.DragModes.DoNotChange) {
				SetMousePos ();

				differenceX = thisRT.position.x - MousePos.x;
				differenceY = thisRT.position.y - MousePos.y;
			}

			PDown = true;
		} else {
			// Setup AI system
			float speed = DDM.GetComponent<AIManager> ().MovementSpeed;

			for (int i = 0; i < DDM.AllPanels.Length; i++) {
				if (AIPanel == DDM.AllPanels [i].Id) {
					StartCoroutine (SmoothMove (state, thisRT, DDM.AllPanels [i].GetComponent<RectTransform> ().position, speed));
				}
			}
		}

		// Events Management
		if (OnBeginDragging != null) {
			OnBeginDragging.Invoke ();
		}
	}

	void SetMousePos () {
		Vector3 screenPoint;

		if (DDM.TargetPlatform == DragDropManager.Platforms.PC) {
			// for PC
			if (DDM.FirstCanvas.renderMode != RenderMode.ScreenSpaceOverlay) {
				// ScreenSpaceCamera & WorldSpace support
				screenPoint = Input.mousePosition;
				screenPoint.z = DDM.FirstCanvas.planeDistance;
				MousePos = Camera.main.ScreenToWorldPoint (screenPoint);
			} else {
				MousePos = Input.mousePosition;
			}
		} else {
			// for Mobile
			if (DDM.FirstCanvas.renderMode != RenderMode.ScreenSpaceOverlay) {
				// ScreenSpaceCamera & WorldSpace support
				screenPoint = Input.touches [0].position;
				screenPoint.z = DDM.FirstCanvas.planeDistance;
				MousePos = Camera.main.ScreenToWorldPoint (screenPoint);
			} else {
				MousePos = Input.touches [0].position;
			}
		}
	}

	public void PointerUp () {
		PointerUpActions ();
	}

	void PointerUpActions () {
		if (Dropped) {
			// Setup customization tools of the panel
			for (int i = 0; i < DDM.AllPanels.Length; i++) {
				if (DDM.IdManager[i] == Id) {
					if (DDM.AllPanels [i].LockObject != DragAndDropPanelSettings.ObjectLockStates.LockObject) {
						if (!LockObject || DDM.AllPanels [i].LockObject == DragAndDropPanelSettings.ObjectLockStates.DoNotLockObject) {
							CheckObjectPos ();
							return;
						}
					}
				}
			}
		} else {
			CheckObjectPos ();
		}
	}

	void CheckObjectPos ()
    {
#if UNITY_ANRDOID || UNITY_IOS
        var lastPosition = Input.GetTouch(0).position;
#else
        var lastPosition = Input.mousePosition;
#endif
        PDown = false;
		CheckStatus = false;

		for (int i = 0; i < DDM.AllPanels.Length; i++) {
            if (DDM.OnlyUI)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(DDM.AllPanels[i].GetComponent<RectTransform>(), thisRT.position))
                {
                    if (FilterPanels)
                    {
                        // setup Filter Panels tool
                        for (int j = 0; j < AllowedPanels.Length; j++)
                        {
                            if (DDM.AllPanels[i].Id == AllowedPanels[j])
                            {
                                PanelDropTools(i);
                            }
                        }
                    }
                    else
                    {
                        PanelDropTools(i);
                    }
                }
            }
            else
            {
                var panelName = DDM.AllPanels[i].gameObject.name;
                var ray = Camera.main.ScreenPointToRay(lastPosition);
                var hits = Physics2D.GetRayIntersectionAll(ray, 1500f);

                foreach (var hit in hits)
                {
                    if(panelName == hit.collider.name)
                    {
                        if (FilterPanels)
                        {
                            // setup Filter Panels tool
                            for (int j = 0; j < AllowedPanels.Length; j++)
                            {
                                if (DDM.AllPanels[i].Id == AllowedPanels[j])
                                {
                                    PanelDropTools(i);
                                }
                            }
                        }
                        else
                        {
                            PanelDropTools(i);
                        }
                    }
                }
            }
		}

		if (!CheckStatus) { // check if drag & drop failed
			if (!ReturnSmoothly) {
				thisRT.position = CurrentPos;

				// Save last position of this object
				if (DDM.SaveStates) {
					PlayerPrefs.SetFloat (Id + "X", CurrentPos.x);
					PlayerPrefs.SetFloat (Id + "Y", CurrentPos.y);
				}
			} else {
				StartCoroutine (SmoothMove ("User", thisRT, CurrentPos, ReturnSpeed));
			}

            if(StayDroppedPos)
			    thisRT.localScale = FirstScale;

			// Events Management
			if (OnDragDropFailed != null)
            {
                thisRT.localScale = FirstScale;
                OnDragDropFailed.Invoke ();
			}
		}

		thisRT.SetParent (thisParent);
	}

	void PanelDropTools (int i) {
		if (DDM.IdManager [i] != "" && DDM.IdManager [i] != Id) {
			for (int j = 0; j < DDM.AllObjects.Length; j++) {
				if (DDM.AllObjects [j].Id == DDM.IdManager [i]) {
					if (DDM.AllObjects [j].OnReturning) {
						return;
					}
				}
			}
			// Setup customization tools of the panel
			if (DDM.AllPanels [i].ObjectReplacement != DragAndDropPanelSettings.ObjectReplace.NotAllowed) {
				CheckStatus = true;
				DropActions (i);
			}
		} else {
			CheckStatus = true;
			DropActions (i);
		}
	}

	void DropActions (int i) {
		bool SwitchStatus = false;
		bool ReplaceDefaultPanel = false;

		// setup ReturnObject tool
		ReturnObjectTool ();

		thisRT.localScale = FirstScale;

		if (!StayDroppedPos && !ReturnObject && DDM.AllPanels [i].ObjectPosition != DragAndDropPanelSettings.ObjectPosStates.DroppedPosition) {
			thisRT.position = new Vector3 (DDM.AllPanels [i].GetComponent <RectTransform> ().position.x, DDM.AllPanels [i].GetComponent <RectTransform> ().position.y, thisRT.position.z);
        }

		// Save last position of this object
		if (DDM.SaveStates) {
			PlayerPrefs.SetFloat (Id + "X", thisRT.position.x);
			PlayerPrefs.SetFloat (Id + "Y", thisRT.position.y);
		}

		// Check if there is another object on target panel
		if (DDM.IdManager [i] != "" && DDM.IdManager [i] != Id) {
			
			if (DDM.AllPanels [i].ObjectReplacement == DragAndDropPanelSettings.ObjectReplace.Allowed) {
				
				for (int j = 0; j < DDM.AllObjects.Length; j++) {
				
					if (DDM.IdManager [i] == DDM.AllObjects [j].Id) {
						if (DDM.AllObjects [j].DefaultPanel) {
							ReplaceDefaultPanel = true;
						}
					}
				}

				// Check if objects should not switch between their panels. So this object will replace with second object
				if (!SwitchObjects && !DefaultPanel && !ReplaceDefaultPanel) {
					ObjectsReplacement (i);
				} else {
					// Objects will switch between their panels (if this object was on any panel)
					if (ObjectsSwitching (i)) {
						SwitchStatus = true;
					} else {
						// This object was not on any panel. So this object will be replaced with second object
						ObjectsReplacement (i);
					}
				}
			}
		}

		// setup Multi Object tool
		for (int j = 0; j < DDM.IdManager.Count; j++) {
			if (DDM.IdManager [j] == Id) {
				if (DDM.AllPanels [j].ObjectReplacement == DragAndDropPanelSettings.ObjectReplace.MultiObjectMode && !SwitchStatus) {
					DDM.AllPanels [j].RemoveMultiObject (Id);
					SetPrevPanelId ();
				} else if (DDM.AllPanels [j].ObjectReplacement == DragAndDropPanelSettings.ObjectReplace.MultiObjectMode) {
					SetPrevPanelId ();
				}
			}
		}

		if (DDM.AllPanels [i].ObjectReplacement == DragAndDropPanelSettings.ObjectReplace.MultiObjectMode) {
			SetPrevPanelId ();
			DDM.AllPanels [i].SetMultiObject (Id);
		}


		// setup ScaleOnDropped tool
		ScaleOnDroppedTool (i);

		if (!SwitchObjects && !DefaultPanel) {
			SetPrevPanelId ();
		} else {
			if (DDM.IdManager [i] == "") {
				SetPrevPanelId ();
			}
		}
			
		DDM.SetPanelObject (i, Id);

		// Setup customization tools of the panel
		if (DDM.AllPanels [i].ObjectPosition == DragAndDropPanelSettings.ObjectPosStates.PanelPosition) {
			thisRT.position = new Vector3 (DDM.AllPanels [i].GetComponent <RectTransform> ().position.x, DDM.AllPanels [i].GetComponent <RectTransform> ().position.y, thisRT.position.z);
		}
		//

		// Panel Events Management
		DDM.AllPanels [i].SetupPanelEvents ();

		// Events Management
		if (OnDroppedSuccessfully != null) {
			OnDroppedSuccessfully.Invoke ();
		}
	}

	void ObjectsReplacement (int i) {
		for (int j = 0; j < DDM.AllObjects.Length; j++) {

			if (DDM.IdManager [i] == DDM.AllObjects [j].Id && DDM.AllObjects [j].Dropped) {
				DDM.AllObjects [j].Dropped = false;

				DDM.AllObjects [j].GetComponent <RectTransform> ().localScale = DDM.AllObjects [j].FirstScale;

				if (!ReplaceSmoothly) {
					DDM.AllObjects [j].GetComponent <RectTransform> ().position = DDM.AllObjects [j].FirstPos;

					// Save last position of second object
					if (DDM.SaveStates) {
						PlayerPrefs.SetFloat (DDM.AllObjects [j].Id + "X", DDM.AllObjects [j].FirstPos.x);
						PlayerPrefs.SetFloat (DDM.AllObjects [j].Id + "Y", DDM.AllObjects [j].FirstPos.y);
					}
				} else {
					StartCoroutine (SmoothMove ("User", DDM.AllObjects [j].GetComponent <RectTransform> (), DDM.AllObjects [j].FirstPos, ReplacementSpeed));
				}
			}
		}
	}

	bool ObjectsSwitching (int i) {
		for (int j = 0; j < DDM.IdManager.Count; j++) {
			if (DDM.IdManager [j] == Id) {
				
				for (int k = 0; k < DDM.AllObjects.Length; k++) {
					if (DDM.IdManager [i] == DDM.AllObjects [k].Id) {

						if (DDM.AllPanels [j].ObjectReplacement == DragAndDropPanelSettings.ObjectReplace.MultiObjectMode) {
							// setup Multi Object tool
							DDM.AllPanels [j].RemoveMultiObject (Id);
							DDM.AllPanels [j].SetMultiObject (DDM.AllObjects [k].Id);
						}

						DDM.SetPanelObject (j, DDM.AllObjects [k].Id);

						DDM.AllObjects [k].GetComponent <RectTransform> ().SetAsLastSibling ();

						if (!MoveSmoothly) {
							DDM.AllObjects [k].GetComponent <RectTransform> ().position = new Vector3 (CurrentPos.x, CurrentPos.y, DDM.AllObjects [k].GetComponent <RectTransform> ().position.z);

							// Save last position of second object
							if (DDM.SaveStates) {
								PlayerPrefs.SetFloat (DDM.AllObjects [k].Id + "X", CurrentPos.x);
								PlayerPrefs.SetFloat (DDM.AllObjects [k].Id + "Y", CurrentPos.y);
							}
						} else {
							StartCoroutine (SmoothMove ("User", DDM.AllObjects [k].GetComponent <RectTransform> (), CurrentPos, MovementSpeed));
						}
					}
				}
				return true;
			}
		}
		return false;
	}
		
	void SetPrevPanelId () {
		for (int j = 0; j < DDM.IdManager.Count; j++) {

			if (DDM.IdManager [j] == Id) {
				if (DDM.AllPanels [j].ObjectReplacement != DragAndDropPanelSettings.ObjectReplace.MultiObjectMode || DDM.AllPanels [j].PanelIdManager.Count == 0) {
					DDM.SetPanelObject (j, "");
				} else {
					DDM.SetPanelObject (j, DDM.AllPanels [j].PanelIdManager [DDM.AllPanels [j].PanelIdManager.Count - 1]);
				}
			}
		}
	}

	void ReturnObjectTool () {
		if (!ReturnObject) {
			Dropped = true;
		} else {
			thisRT.position = CurrentPos;
		}
	}

	void ScaleOnDroppedTool (int i) {
		if (Dropped && ScaleOnDropped) {
            thisRT.localScale = new Vector3(DDM.AllPanels[i].GetComponent<RectTransform>().localScale.x, DDM.AllPanels[i].GetComponent<RectTransform>().localScale.y, thisRT.localScale.z);
        }
    }

	// Smooth Movement tool
	IEnumerator SmoothMove (string state, RectTransform Target, Vector3 TargetPos, float Speed) {
		Target.GetComponent<DragAndDropObjectSettings> ().OnReturning = true;

		float t = 0.0f;

		TargetPos.z = Target.position.z;

		// Save last position of target object
		if (DDM.SaveStates) {
			PlayerPrefs.SetFloat (Target.GetComponent<DragAndDropObjectSettings> ().Id + "X", TargetPos.x);
			PlayerPrefs.SetFloat (Target.GetComponent<DragAndDropObjectSettings> ().Id + "Y", TargetPos.y);
		}

		while (!Mathf.Approximately (Target.position.x, TargetPos.x) || !Mathf.Approximately (Target.position.y, TargetPos.y)) {
			t += Time.deltaTime * Speed;

			Target.position = Vector3.Lerp (Target.position, TargetPos, Mathf.SmoothStep (0.0f, 1.0f, t));

			yield return null;
		}

		Target.GetComponent<DragAndDropObjectSettings> ().OnReturning = false;

		if (state == "AI") {
			PointerUp ();
		}
	}
}