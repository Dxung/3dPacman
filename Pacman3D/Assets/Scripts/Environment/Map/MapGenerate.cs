using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class MapGenerate : MonoBehaviour
{

    [Header("My TileMap")]
    [SerializeField] private Tilemap _myTileMap;

    [Header("TileBase marks Tile for Drawing")]
    [SerializeField] private TileBase _wallTile;
    [SerializeField] private TileBase _blankTile;
    [SerializeField] private TileBase _powerPelletTile;

    [Header("GameObjects to Generate")]
    [SerializeField] private GameObject _wallPreference;
    [SerializeField] private GameObject _smallPellet;
    [SerializeField] private GameObject _powerPellet;

    [Header("Floating height of pellet")]
    [SerializeField] private float _up;

    [Header("For updating UI")]
    [SerializeField] private GameCommunicationSystem _gameCommunicationSystem;

    [Header("For NavMesh Surfaces")]
    [SerializeField] private NavMeshSurface[] surfaces;

    private void Start()
    {
        _gameCommunicationSystem = GameCommunicationSystem.Instance;   
        GenerateWorld();
    }


    void GenerateWorld()
    {
        foreach (Vector3Int position in _myTileMap.cellBounds.allPositionsWithin)    //check through all tiles
        {
            TileBase tilebase = _myTileMap.GetTile(position);                       //get tilemap pos
            Vector3 cellPosition = _myTileMap.GetCellCenterWorld(position);         //switch tilemap pos -> worldmap pos


            if (tilebase == _blankTile)
            {

            }
            else if (tilebase == _wallTile)
            {
                Instantiate(_wallPreference, cellPosition + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
            else if (tilebase == _powerPelletTile)
            {
                //tao power pellet
                Instantiate(_powerPellet, cellPosition + new Vector3(0, _up, 0), Quaternion.identity);

                //tang them 1 power pellet tren counter
                _gameCommunicationSystem.PowerPelletToCounter();
            }
            else
            {
                //tao normal pellet
                Instantiate(_smallPellet, cellPosition + new Vector3(0, _up, 0), Quaternion.identity);

                //tang them 1 normal pellet tren counter
                _gameCommunicationSystem.NormalPelletToCounter();
            }
        }

        InitNavMesh();
    }

        void InitNavMesh()
        {
            for (int i = 0; i < surfaces.Length; i++)
            {

                surfaces[i].BuildNavMesh();
            }
        }

    
}
