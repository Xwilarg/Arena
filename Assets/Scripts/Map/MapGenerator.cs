using System;
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

        var map = File.ReadAllLines(path).Reverse().ToArray();
        int startX = -map[0].Length / 2;
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
                        break;

                    case 'I':
                        _tilemapIce.SetTile(pos, _tiles.Ice);
                        break;

                    case 'G':
                        _tilemapGrass.SetTile(pos, _tiles.RockGrass);
                        _tilemapDecoration.SetTile(pos + new Vector3Int(0, 1, 0), _tiles.Grass);
                        break;

                    case '.': break;

                    default:
                        throw new ArgumentException("Invalid tile " + line[x]);
                }
            }
        }
    }
}
