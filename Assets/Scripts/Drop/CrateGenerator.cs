using System.Collections;
using UnityEngine;

public class CrateGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform _leftBound, _rightBound;

    [SerializeField]
    private GameObject _crate;

    private int _count = 1;

    private void Start()
    {
        StartCoroutine(SpawnCrate());
    }

    private IEnumerator SpawnCrate()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            var crate = Instantiate(_crate, new Vector2(Random.Range(_leftBound.position.x, _rightBound.position.x), Random.Range(_leftBound.position.y, _rightBound.position.y)), Quaternion.identity);
            crate.name = "Crate " + _count;
            _count++;
        }
    }
}
