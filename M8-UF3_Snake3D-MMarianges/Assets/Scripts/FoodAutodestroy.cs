using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAutodestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private float _destroyTime;
    private float _timer;
    void Start()
    {
        _destroyTime = 8f;
        _timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _destroyTime)
        {
            Destroy(gameObject);
        }

    }
}
