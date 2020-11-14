using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField]
    private ItemsInfo ItemsInfo;

    public void Open()
    {
        var item = ItemsInfo.Items[Random.Range(0, ItemsInfo.Items.Length)];
        var go = Instantiate(item.GameObject, transform.position, transform.rotation);
        go.GetComponent<Grappable>().Item = item;
        Destroy(gameObject);
    }
}
