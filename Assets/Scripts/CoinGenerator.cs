using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour 
{

    public ObjectPooler coinPool;

    public float distanceBetweenCoins;

    public void SpawnCoins(Vector3 startPosition)
    {
        var coin1 = coinPool.GetPooledObject();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);

        var coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
        coin2.SetActive(true);

        var coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
        coin3.SetActive(true);
    }
}
