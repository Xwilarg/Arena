using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tilemapRock, _tilemapGrass, _tilemapIce, _tilemapDecoration;

    [SerializeField]
    private Tiles _tiles;

    private void Start()
    {
        var path = Application.dataPath + "/Maps/Base.txt";
        if (!File.Exists(path))
            path = "Maps/Base.txt";

        var map = File.ReadAllLines(path).Reverse().ToArray();
        var tileMap = new Dictionary<Vector2Int, TileType>();
        int startX = -map[0].Length / 2;
        Camera.main.orthographicSize = map[0].Length * 0.15f;
        int startY = -map.Length / 2;
        for (int y = 0; y < map.Length; y++)
        {
            var line = map[y];
            for (int x = 0; x < line.Length; x++)
            {
                var pos = new Vector3Int(startX + x, startY + y, 0);
                switch (line[x])
                {
                    case 'R':
                        _tilemapRock.SetTile(pos, _tiles.Rock);
                        tileMap.Add((Vector2Int)pos, TileType.Normal);
                        break;

                    case 'I':
                        _tilemapIce.SetTile(pos, _tiles.Ice);
                        tileMap.Add((Vector2Int)pos, TileType.Ice);
                        break;

                    case 'G':
                        _tilemapGrass.SetTile(pos, _tiles.RockGrass);
                        _tilemapDecoration.SetTile(pos + new Vector3Int(0, 1, 0), _tiles.Grass);
                        tileMap.Add((Vector2Int)pos, TileType.Grass);
                        break;

                    case 'B':
                        Instantiate(_tiles.Bumper, ((Vector3)pos * .64f) + (Vector3)(Vector2.one * (.64f / 2f)), Quaternion.identity);
                        tileMap.Add((Vector2Int)pos, TileType.Bumper);
                        break;

                    case '-':
                        Tile tile;
                        var prev = line[x - 1];
                        var next = line[x + 1];
                        if (prev != '-' && prev != '.' && next != '-' && next != '.')
                            tile = _tiles.PlateformBoth;
                        else if (prev != '-' && prev != '.')
                            tile = _tiles.PlateformLeft;
                        else if (next != '-' && next != '.')
                            tile = _tiles.PlateformRight;
                        else
                            tile = _tiles.PlateformNone;
                        _tilemapRock.SetTile(pos, tile);
                        tileMap.Add((Vector2Int)pos, TileType.Plateform);
                        break;

                    case '.':
                        tileMap.Add((Vector2Int)pos, TileType.None);
                        break;

                    default:
                        throw new ArgumentException("Invalid tile " + line[x]);
                }
            }
        }

        AIManager.S.LoadMap(tileMap);
    }
}
