using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject core;

    // Use this for initialization
    void Start()
    {
        core = transform.Find("Core").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (core == null)
        {
            Destroy(gameObject);
        }
    }
}
