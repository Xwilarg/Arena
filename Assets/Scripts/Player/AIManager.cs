using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager S;

    private void Awake()
    {
        S = this;
    }

    private Dictionary<Vector2Int, TileType> _map;
    private List<Character> _characters = new List<Character>();

    public void LoadMap(Dictionary<Vector2Int, TileType> map)
    {
        _map = map;
    }

    public void RegisterCharacter(Character c)
    {
        _characters.Add(c);
    }

    /// <summary>
    /// Get all character, except myself
    /// </summary>
    public Character[] GetCharacters(Character me)
    {
        return _characters.Where(c => c != me).ToArray();
    }
}
