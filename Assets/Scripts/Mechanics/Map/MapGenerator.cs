using Graf.Pool;
using Graf.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class MapGenerator
{
    public List<GameObject> poolObjectViewList = new List<GameObject>();
   
    private EventManager eventManager;
    private PaletteManager paletteManager;
    private List<ListMapItems> prefabRoomItems;

    [Inject]
    public MapGenerator(EventManager eventManager, PaletteManager paletteManager, List<ListMapItems> prefabRoomItems)
    {
        this.eventManager = eventManager;
        this.paletteManager = paletteManager;
        this.prefabRoomItems = prefabRoomItems;
        InitActions();
    }
    private void InitActions()
    {
        eventManager.OnHookToMainStayEvent.AddListener(CreatePrefabOfList);
    }
    public void GameBegin()
    {
        CreateFirstMainStay(new Vector3(0f, -13, 0f));
        CreateFirstPrefabOfList(new Vector3(0f, -13f, 0f));
    }

    private void CreateFirstPrefabOfList(Vector3 shiftMainStay)
    {
        GameObject mapItemGameObject = null;
        for (int i = prefabRoomItems[0].ListRoomItems.Count - 1; i >= 0; i--)
        {
            var prefabItem = prefabRoomItems[0];
            var mapItem = prefabItem.ListRoomItems[i];
            mapItemGameObject = PoolManager.Get(mapItem.Id).gameObject;
            mapItemGameObject.transform.position = shiftMainStay + mapItem.Position + new Vector3(0f, prefabItem.ShiftY, 0f);
            mapItemGameObject.GetComponent<MapItems>().Init(paletteManager.CurrentColorsConfig);
            poolObjectViewList.Add(mapItemGameObject); //TODO: check
        }
        //CreatePrefabOfList(mapItemGameObject.transform.position);
    }

    private void CreatePrefabOfList(Vector3 shiftMainStay)
    {
        for (int i = 0; i < prefabRoomItems[0].ListRoomItems.Count; i++)
        {
            var prefabItem = prefabRoomItems[0];
            var mapItem = prefabItem.ListRoomItems[i];
            GameObject mapItemGameObject = PoolManager.Get(mapItem.Id).gameObject;
            mapItemGameObject.transform.position = shiftMainStay + mapItem.Position + new Vector3(0f, prefabItem.ShiftY, 0f);
            mapItemGameObject.GetComponent<MapItems>().Init(paletteManager.CurrentColorsConfig);
            poolObjectViewList.Add(mapItemGameObject); //TODO: check
            
        }
    }
    

    public void CreateFirstMainStay(Vector3 currentPosition)
    {
        var mainStayTemp = PoolManager.Get(1).GetComponent<MainStayView>();
        mainStayTemp.transform.position = currentPosition;
        mainStayTemp.Init(paletteManager.CurrentColorsConfig, eventManager,true);
        poolObjectViewList.Add(mainStayTemp.gameObject); // TODO: check
        eventManager.SendMainStayCreated(mainStayTemp);
    }

    public void ContinueGame()
    {


    }
}
