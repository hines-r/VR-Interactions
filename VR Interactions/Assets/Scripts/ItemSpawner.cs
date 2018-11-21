using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemToSpawn;

    private GameObject currentItem;

    void Update()
    {
        if (currentItem == null)
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        currentItem = Instantiate(itemToSpawn, transform.position, Quaternion.identity);
        currentItem.transform.parent = gameObject.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentItem)
        {
            currentItem = null;
        }
    }
}
