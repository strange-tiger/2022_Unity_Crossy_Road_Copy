using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum TileType
    {
        Grass,
        Road,
        River,
        Max
    }

    public GameObject GrassTile;
    public GameObject RoadTile;
    public GameObject RiverTile;

    private GameObject[] _tile = new GameObject[3];
    private void Awake()
    {
        _tile[0] = GrassTile;
        _tile[1] = RoadTile;
        _tile[2] = RiverTile;
    }

}
