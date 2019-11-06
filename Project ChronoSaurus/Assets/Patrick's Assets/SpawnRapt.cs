using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRapt : MonoBehaviour
{
    public GameObject spawnLeft;
    public GameObject spawnRight;
    public GameObject spawnAbove;
    public GameObject spawnBelow;
    public GameObject velociraptor;
    public GameObject clone = new GameObject();
    public int wait;

    public int mobCount;
    // Start is called before the first frame update
    void Start()
    {
        mobCount = 20;
        wait = 1;
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    IEnumerator waiter()
    {
        while (mobCount > 0)
        {
            clone = velociraptor;
            GameObject rapt0 = Instantiate(clone, spawnLeft.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(wait);
            mobCount--;
            clone = velociraptor;
            GameObject rapt1 = Instantiate(clone, spawnRight.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(wait);
            mobCount--;
            clone = velociraptor;
            GameObject rapt2 = Instantiate(clone, spawnAbove.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(wait);
            mobCount--;
            clone = velociraptor;
            GameObject rapt3 = Instantiate(clone, spawnBelow.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(wait);
            mobCount--;
        }
    }

}
