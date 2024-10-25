using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class MapGenerate : MonoBehaviour
{

    [Header("My TileMap")]
    [SerializeField] private Tilemap _myTileMap;

    [Header("TileBase marks Tile for Drawing")]
    [SerializeField] private TileBase _wallTile;
    [SerializeField] private TileBase _blankTile;
    [SerializeField] private TileBase _superPelletTile;

    [Header("GameObjects to Generate")]
    [SerializeField] private GameObject _wallPreference;
    [SerializeField] private GameObject _smallPellet;
    [SerializeField] private GameObject _superPellet;

    [Header("Floating height of pellet")]
    [SerializeField] private float _up;

    [Header("for updating UI")]
    [SerializeField] private PelletCounter _pelletCounter;


    private void Start()
    {
        GenerateWorld();
    }


    void GenerateWorld()
    {
        foreach (Vector3Int position in _myTileMap.cellBounds.allPositionsWithin)    //check through all tiles
        {
            TileBase tilebase = _myTileMap.GetTile(position);                       //get tilemap pos
            Vector3 cellPosition = _myTileMap.GetCellCenterWorld(position);         //switch tilemap pos -> worldmap pos


            if (tilebase != _blankTile)
            {
                if (tilebase == _wallTile)
                {
                    Instantiate(_wallPreference, cellPosition + new Vector3(0, 0.5f, 0), Quaternion.identity);
                }
                else if (tilebase == _superPelletTile)
                {
                    Instantiate(_superPellet, cellPosition + new Vector3(0, _up, 0), Quaternion.identity);
                    UpdatePowerPelletCounter();
                }
                else
                {
                    
                    Instantiate(_smallPellet, cellPosition + new Vector3(0, _up, 0), Quaternion.identity);
                    UpdateSmallPelletCounter();
                }

            }



        }

        void UpdateSmallPelletCounter()
        {
            _pelletCounter.AddOneSmallPelletToCounter();
        }

        void UpdatePowerPelletCounter()
        {
            _pelletCounter.AddOnePowerPelletToCounter();
        }



    }
}
