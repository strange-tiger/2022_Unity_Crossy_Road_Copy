using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum TileType
    {
        Grass0,
        Grass1,
        Grass2,
        Grass3,
        Grass4,
        Grass5,
        Grass6,
        Grass7,
        Grass8,
        Grass9,
        Road,
        Rail,
        River0,
        River1,
        Max
    }

    public ScoreText score;
    public PlayerInput input;
    public const int TilesNum = (int)TileType.Max;
    public GameObject[] _tiles = new GameObject[TilesNum];
    public const float TileWidth = 1.5f;
    public const int SafeTileNum = 10;

    private List<GameObject> _tilesList = new List<GameObject>();
    private Vector3 _currentPosition = new Vector3(0f, 0f, -12f);
    private int _tileCount = 0;
    private int _maxTileCount = 60;
    private int _prevScore = 0;

    private void Start()
    {
        for (int i = 0; i < SafeTileNum; ++i)
        {
            GameObject tile;
            tile = Instantiate(_tiles[0], _currentPosition, Quaternion.identity);
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
        if (score.playerMovement.transform.position.z < 6f)
        {
            return;
        }

        if (score.score > _prevScore)
        {
            SpawnTile();
            _prevScore = score.score;
        }
    }

    private int randomMin = (int)TileType.Grass0;
    private int randomMax = (int)TileType.Max;
    public void SpawnTile()
    {
        _currentPosition.x = score.playerMovement.transform.position.x;
        float direction = Random.value;
        GameObject tile;
        if (direction < 0.5f)
        {
            tile = Instantiate(_tiles[Random.Range(randomMin, randomMax)], _currentPosition, Quaternion.identity);
        }
        else
        {
            tile = Instantiate(_tiles[Random.Range(randomMin, randomMax)], _currentPosition, Quaternion.EulerRotation(0f, -1 * Mathf.PI, 0f));
        }

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
