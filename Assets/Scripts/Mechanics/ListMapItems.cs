using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New ListMapItem", menuName = "Scriptable Object/List Map Item", order = 54)]
public class ListMapItems : ScriptableObject
{
    public float ShiftY;
    public List<RoomItem> ListRoomItems;
}

[System.Serializable]
public struct RoomItem
{
    public int Id;
    public Vector3 Position;
}
