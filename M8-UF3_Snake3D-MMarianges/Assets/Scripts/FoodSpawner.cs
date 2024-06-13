using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private float timer;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameManager _gm;
    [SerializeField] private GameObject foodParent;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime && !_gm.gameOver)
        {
            SpawnFood();
        }
    }

    void SpawnFood()
    {
        Vector3 max = cube.GetComponent<MeshRenderer>().bounds.max;
        Vector3 min = cube.GetComponent<MeshRenderer>().bounds.min;
        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(min.x, max.x), 1, UnityEngine.Random.Range(min.z, max.z));

        Instantiate(foodPrefab, spawnPos, Quaternion.identity, foodParent.transform);
        timer = 0;
    }
}