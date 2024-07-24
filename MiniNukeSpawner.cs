using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniNukeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject MiniNukeInstance;
    [SerializeField] private float Delay;

    public bool SpawningNukes;
    private bool testbool;

    void Start()
    {
        testbool = true;
        SpawningNukes = false;
        Delay = 2f;
    }
    public void StartSpawning()
    {
        StartCoroutine("SpawnEnum");
    }
    public void StopSpawning()
    {
        SpawningNukes = false;
    }
    private IEnumerator SpawnEnum()
    {
        SpawningNukes = true;
        while (SpawningNukes)
        {
            yield return new WaitForSeconds(Delay);
            SpawnNuke();
        }
    }
    private void SpawnNuke()
    {
        int x = Random.Range(-10, 10);//TODO: implement randomness
        int y = Random.Range(-10, 10);
        Instantiate(MiniNukeInstance, new Vector3(x, y, 0), Quaternion.identity);
        Debug.Log("Spawned at: " + x + " " + y);
    }
}
