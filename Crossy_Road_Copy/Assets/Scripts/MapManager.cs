using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum TileType
    {
        Grass0,
        Grass1,
        Grass2,
        Road,
        Rail,
        River0,
        River1,
        Max
    }

    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    public GameObject[] _tiles = new GameObject[7];
    public const float TileWidth = 1.5f;
    public const int SafeTileNum = 6;

    private List<GameObject> _tilesList = new List<GameObject>();
    private Vector3 _currentPosition = new Vector3(0f, 0f, -6f);
    private int _tileCount = 0;
    private int _maxTileCount = 50;
    
    private void Start()
    {
        for (int i = 0; i < SafeTileNum; ++i)
        {
            GameObject tile = Instantiate(_tiles[0], _currentPosition, Quaternion.identity);
            tile.transform.SetParent(transform);
            _tilesList.Add(tile);
            _currentPosition.z += TileWidth;
            ++_tileCount;
        }

        while (_tileCount < _maxTileCount)
        {
            SpawnTile();
            ++_tileCount;
        }
    }

    void Update()
    {
        if (playerMovement.transform.position.z < 6f)
        {
            return;
        }

        if (playerInput.VerticalMove > 0f)
        {
            SpawnTile();
        }
    }

    private int randomMin = (int)TileType.Grass0;
    private int randomMax = (int)TileType.Max;
    public void SpawnTile()
    {
        _currentPosition.x = playerMovement.transform.position.x;
        GameObject tile = Instantiate(_tiles[Random.Range(randomMin, randomMax)], _currentPosition, Quaternion.identity);
        tile.transform.SetParent(transform);
        _tilesList.Add(tile);
        _currentPosition.z += TileWidth;
        ++_tileCount;

        if (_tileCount > _maxTileCount)
        {
            Destroy(_tilesList[0]);
            _tilesList.RemoveAt(0);
            --_tileCount;
        }
    }
}
