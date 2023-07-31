using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawn : MonoBehaviour
{
    public GameObject columnPrefab;
    public int columnPoolSize = 5;
    public float spawnRate = 3f;
    public float columnMin = -1f;
    public float columnMax = 3.5f;
    public float columnXoffset = 5f;

    GameObject[] columns;
    int currentColumn = 0;

    // Start is called before the first frame update
    void Start()
    {
        columns = new GameObject[columnPoolSize];
        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, new Vector3(-15.0f, -25.0f, 0), Quaternion.identity);
        }
        StartCoroutine("SpawnLoop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Vector3 pos = this.transform.position;
            //Debug.Log("transform.position:" + pos);
            pos.y = Random.Range(columnMin, columnMax);
            Vector3 birdPos = GameObject.Find("Bird").transform.position;
            pos.x = birdPos.x + columnXoffset;
            columns[currentColumn].transform.position = pos;
            if (++currentColumn >= columnPoolSize)
                currentColumn = 0;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void StopSpawn()
    {
        StopCoroutine("SpawnLoop");
    }
}
