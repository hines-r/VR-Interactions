using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemToSpawn;
    public float cooldownTime = 1f;
    private float nextTimeToSpawn;

    private GameObject currentItem;

    void Update()
    {
        if (currentItem == null)
        {
            if (nextTimeToSpawn <= 0f)
            {
                SpawnItem();
                nextTimeToSpawn = cooldownTime;
            }
            else
            {
                nextTimeToSpawn -= Time.deltaTime;
            }
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
