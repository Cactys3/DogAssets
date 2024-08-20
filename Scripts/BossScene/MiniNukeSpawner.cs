using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniNukeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject MiniNukeInstance;
    public float Delay;

    public bool SpawningNukes;
    private bool testbool;

    void Start()
    {
        testbool = true;
        SpawningNukes = false;
        Delay = 1f;
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
        float x;
        float y;
        if (Random.Range(1, 3) == 1)
        {
            x = Random.Range(-8f, -6.5f);
            y = Random.Range(-9, -3.8f);
        }
        else
        {
            x = Random.Range(6.5f, 9);
            y = Random.Range(3.8f, 8);
        }
        Instantiate(MiniNukeInstance, new Vector3(x, y, 0), Quaternion.identity);
        Debug.Log("Spawned at: " + x + " " + y);
    }
}
