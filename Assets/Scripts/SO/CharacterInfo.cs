using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterInfo", fileName = "CharacterInfo")]
public class CharacterInfo : ScriptableObject
{
    public float Speed;
    public float GrassSpeedReductor;
    public float JumpHeight;
    public float SecondJumpHeight;
}