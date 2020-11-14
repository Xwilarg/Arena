using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SingleItem", fileName = "SingleItem")]
public class SingleItem : ScriptableObject
{
    public Vector2 Position;
    public float Rotation;
    public GameObject GameObject;
}
