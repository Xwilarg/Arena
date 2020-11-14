using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemsInfo", fileName = "ItemsInfo")]
public class ItemsInfo : ScriptableObject
{
    public SingleItem[] Items;
}