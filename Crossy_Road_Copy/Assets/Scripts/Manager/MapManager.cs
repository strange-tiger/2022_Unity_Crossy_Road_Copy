using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum TileType
    {
        Grass0,
        Grass1,
        Road,
        Rail,
        River0,
        River1,
        Max
    }

    public PlayerScore score;
    public PlayerInput input;
    public GameObject[] _tiles = new GameObject[TilesNum];
    public const int TilesNum = (int)TileType.Max;
    public const float TileWidth = 1.0f;
    public const int SafeTileNum = 15;

    private List<GameObject> _tilesList = new List<GameObject>();
    private Vector3 _currentPosition = new Vector3(0f, 0f, -10f);
    private int _tileCount = 0;
    private int _maxTileCount = 50;

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
        }
    }

    private int _prevScore = 0;
    void Update()
    {
        if (score.transform.position.z < 6f)
        {
            return;
        }

        if (score.score > _prevScore)
        {
            SpawnTile();
            ChangeTileRandomWeight();
            _prevScore = score.score;
        }

    }

    private int _prevTileIndex = (int)TileType.Max;
    public void SpawnTile()
    {
        _currentPosition.x = score.transform.position.x;
        float direction = Random.value;
        int tileIndex = GetTileRandom();
        if (_prevTileIndex == (int)TileType.River1)
        {
            if (_prevTileIndex == tileIndex)
            {
                return;
            }
        }

        GameObject tile;
        if (direction < 0.5f)
        {
            tile = Instantiate(_tiles[tileIndex], _currentPosition, Quaternion.identity);
        }
        else
        {
            tile = Instantiate(_tiles[tileIndex], _currentPosition, Quaternion.Euler(0f, 180f, 0f));
        }
        _prevTileIndex = tileIndex;

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

    private int _nextStageNum = 50;
    private float[] _tileRandomWeight = new float[(int)TileType.Max] { 20f, 40f, 20f, 5f, 10f, 5f };
    private float _randomWeightSum = 100f;
    private void ChangeTileRandomWeight()
    {
        if (score.score % _nextStageNum == 0)
        {
            _tileRandomWeight[(int)TileType.Road] += 10f;
            _tileRandomWeight[(int)TileType.Rail] += 10f;
            _tileRandomWeight[(int)TileType.River0] += 10f;
            _tileRandomWeight[(int)TileType.River1] += 10f;

            _randomWeightSum += 40f;
        }
    }

    private int GetTileRandom()
    {
        int tileTypeNum = 0;
        float randomPivot = 0f;

        randomPivot = Random.Range(0f, _randomWeightSum);

        for (int i = 0; i < (int)TileType.Max; ++i)
        {
            if(CheckTilePivot(i, ref randomPivot))
            {
                tileTypeNum = i;
                break;
            }
        }

        return tileTypeNum;
    }

    private bool CheckTilePivot(int i, ref float pivot)
    {
        if(_tileRandomWeight[i] > pivot)
        {
            return true;
        }
        else
        {
            pivot -= _tileRandomWeight[i];
            return false;
        }
    }
}
