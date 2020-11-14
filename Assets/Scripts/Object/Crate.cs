using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField]
    private GameObject _sword;

    public void Open()
    {
        Instantiate(_sword, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
