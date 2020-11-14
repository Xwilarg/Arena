using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObject/Tiles", fileName = "Tiles")]
public class Tiles : ScriptableObject
{
    public Tile Rock, RockGrass, Grass;
}