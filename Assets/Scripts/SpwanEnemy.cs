using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyComponent/SpwanEnemy")]
public class SpwanEnemy : MonoBehaviour
{
    public Transform _enemyPrefable;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawanEnemys());
    }

    IEnumerator SpawanEnemys()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            Instantiate(_enemyPrefable, this.transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "item.png");
    }
}
