using System.Collections.Generic;
using UnityEngine;

public class DragDropManager : MonoBehaviour
{

    // platforms
    public enum Platforms { PC, Mobile };
    public Platforms TargetPlatform;

    // dragging modes
    public enum DragModes { ChangeToMousePos, DoNotChange };

    static DragDropManager DDM;

    // panels
    public DragAndDropPanelSettings[] AllPanels;

    // objects
    public DragAndDropObjectSettings[] AllObjects;
    [HideInInspector]
    public List<DragAndDropObjectSettings> ObjectsOrder;

    // canvases
    public Canvas FirstCanvas;
    public Canvas SecondCanvas;

    [Header("Save scene states")]
    public bool SaveStates = false;

    [Header("Only UI")]
    public bool OnlyUI = true;

    [Header("Object dragging modes")]
    public DragModes DraggingModes;

    [HideInInspector]
    // using for Panel Object detection
    public List<string> IdManager;

    void Awake()
    {
        // Getting current DDM GameObject
        DDM = GameObject.Find("DDM").GetComponent<DragDropManager>();
    }

    public void InitIdManager()
    {
        // setup IdManager List
        for (int i = 0; i < AllPanels.Length; i++)
        {
            IdManager.Add("");
        }
    }

    public void Init()
    {
        InitIdManager();

        OrderObjects();

        for (int i = 0; i < AllObjects.Length; i++)
        {
            // Setting the first position of objects
            AllObjects[i].FirstPos = AllObjects[i].GetComponent<RectTransform>().position;

            // Setting the first scale of objects
            AllObjects[i].FirstScale = AllObjects[i].GetComponent<RectTransform>().localScale;

            SetupDefaultPanel(i);
        }
        if (SaveStates)
        { // Setup Save system
            LoadSavedPositions();
        }
    }

    void OrderObjects()
    {
        // Order objects in array by sibling index
        for (int i = 0; i < AllObjects.Length; i++)
        {
            if (i == 0)
            {
                ObjectsOrder.Add(AllObjects[0]);
            }
            else
            {
                int FinalIndex = 0;
                for (int j = ObjectsOrder.Count - 1; j >= 0; j--)
                {
                    if (AllObjects[i].GetComponent<RectTransform>().GetSiblingIndex() > ObjectsOrder[j].GetComponent<RectTransform>().GetSiblingIndex())
                    {
                        if (FinalIndex <= j)
                        {
                            FinalIndex = j + 1;
                        }
                    }
                    if (AllObjects[i].GetComponent<RectTransform>().GetSiblingIndex() < ObjectsOrder[j].GetComponent<RectTransform>().GetSiblingIndex())
                    {
                        if (FinalIndex > j)
                        {
                            FinalIndex = j - 1;
                        }
                    }
                }
                ObjectsOrder.Insert(FinalIndex, AllObjects[i]);
            }
        }
    }

    void SetupDefaultPanel(int i)
    {
        for (int j = 0; j < AllPanels.Length; j++)
        {
            // Check if there is any object on any panel (Default Panel)
            if (RectTransformUtility.RectangleContainsScreenPoint(AllPanels[j].GetComponent<RectTransform>(), ObjectsOrder[i].GetComponent<RectTransform>().position))
            {
                ObjectsOrder[i].DefaultPanel = true;

                if (SaveStates && PlayerPrefs.HasKey(ObjectsOrder[i].Id + "X"))
                {
                    // the Id of object should be setted from LoadSavedPositions method
                    return;
                }

                if (ObjectsOrder[i].ScaleOnDropped)
                {
                    ObjectsOrder[i].GetComponent<RectTransform>().localScale = AllPanels[j].GetComponent<RectTransform>().localScale;
                }

                // Setting the Id of object for Panel Object detection
                if (AllPanels[j].ObjectReplacement == DragAndDropPanelSettings.ObjectReplace.MultiObjectMode)
                {
                    AllPanels[j].SetMultiObject(ObjectsOrder[i].Id);
                }
                ObjectsOrder[i].Dropped = true;
                SetPanelObject(j, ObjectsOrder[i].Id);
            }
        }
    }

    void LoadSavedPositions()
    {
        // Changing the positions of the objects to its last saved positions
        for (int i = 0; i < AllObjects.Length; i++)
        {
            if (AllObjects[i].Id != "")
            {
                if (PlayerPrefs.HasKey(AllObjects[i].Id + "X"))
                {
                    // Setting the position of object to last saved position
                    AllObjects[i].GetComponent<RectTransform>().position = new Vector3(PlayerPrefs.GetFloat(AllObjects[i].Id + "X"), PlayerPrefs.GetFloat(AllObjects[i].Id + "Y"), AllObjects[i].GetComponent<RectTransform>().position.z);

                    for (int j = 0; j < AllPanels.Length; j++)
                    {
                        // check if the object is on any panel
                        if (RectTransformUtility.RectangleContainsScreenPoint(AllPanels[j].GetComponent<RectTransform>(), AllObjects[i].GetComponent<RectTransform>().position))
                        {
                            if (AllObjects[i].ScaleOnDropped)
                            {
                                AllObjects[i].GetComponent<RectTransform>().localScale = AllPanels[j].GetComponent<RectTransform>().localScale;
                            }

                            // Setting the Id of object for Panel Object detection
                            SetPanelObject(j, AllObjects[i].Id);
                            AllObjects[i].Dropped = true;
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Set the Id of <" + AllObjects[i].gameObject.name + "> Object to use save system with it!");
            }
        }

        // Load Positions of Multi Objects
        for (int i = 0; i < AllPanels.Length; i++)
        {
            if (AllPanels[i].ObjectReplacement == DragAndDropPanelSettings.ObjectReplace.MultiObjectMode)
            {
                AllPanels[i].LoadObjectsList();
            }
        }
    }

    public void SetPanelObject(int PanelIndex, string ObjectId)
    {
        IdManager[PanelIndex] = ObjectId;
    }

    public static void Reset()
    {
        // Reset Objects
        for (int i = 0; i < DDM.AllObjects.Length; i++)
        {
            DDM.AllObjects[i].Dropped = false;
            DDM.ObjectsOrder[i].GetComponent<RectTransform>().SetAsLastSibling();
            DDM.AllObjects[i].GetComponent<RectTransform>().position = DDM.AllObjects[i].FirstPos;
            if (DDM.SaveStates)
            {
                PlayerPrefs.SetFloat(DDM.AllObjects[i].Id + "X", DDM.AllObjects[i].FirstPos.x);
                PlayerPrefs.SetFloat(DDM.AllObjects[i].Id + "Y", DDM.AllObjects[i].FirstPos.y);
            }
            if (DDM.AllObjects[i].ScaleOnDropped)
                DDM.AllObjects[i].GetComponent<RectTransform>().localScale = DDM.AllObjects[i].FirstScale;
        }
        // Reset Panels
        for (int i = 0; i < DDM.AllPanels.Length; i++)
        {
            if (DDM.AllPanels[i].ObjectReplacement == DragAndDropPanelSettings.ObjectReplace.MultiObjectMode)
            {
                DDM.AllPanels[i].PanelIdManager.Clear();
            }
        }

        DDM.IdManager.Clear();
        DDM.ObjectsOrder.Clear();

        DDM.Init();
    }

    public static string GetPanelObject(string PanelId)
    {
        string IdStatus = "";

        for (int i = 0; i < DDM.AllPanels.Length; i++)
        {
            if (PanelId == DDM.AllPanels[i].Id)
            {
                IdStatus = DDM.IdManager[i];
            }
        }

        return IdStatus;
    }

    public static string GetObjectPanel(string ObjectId)
    {
        string IdStatus = "";

        for (int i = 0; i < DDM.AllPanels.Length; i++)
        {
            if (DDM.AllPanels[i].ObjectReplacement != DragAndDropPanelSettings.ObjectReplace.MultiObjectMode)
            {
                if (ObjectId == DDM.IdManager[i])
                {
                    IdStatus = DDM.AllPanels[i].Id;
                }
            }
            else
            {
                for (int j = 0; j < DDM.AllPanels[i].PanelIdManager.Count; j++)
                {
                    if (ObjectId == DDM.AllPanels[i].PanelIdManager[j])
                    {
                        IdStatus = DDM.AllPanels[i].Id;
                    }
                }
            }
        }

        return IdStatus;
    }

    public static string[] GetPanelObjects(string PanelId)
    {
        List<string> IdStatus = new List<string>(1);

        for (int i = 0; i < DDM.AllPanels.Length; i++)
        {
            if (PanelId == DDM.AllPanels[i].Id)
            {
                IdStatus = new List<string>(DDM.AllPanels[i].PanelIdManager.Count);

                IdStatus = DDM.AllPanels[i].PanelIdManager;
            }
        }

        return IdStatus.ToArray();
    }
}