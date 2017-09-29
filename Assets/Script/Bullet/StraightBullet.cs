using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : Bullet
{
    public float speed;//速度

    // Use this for initialization
    void Start()
    {
        vx = speed;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        BulletUpdate();
    }

    /// <summary>
    /// あたり判定
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Core")
        {
            Destroy(col.gameObject);
        }
    }
}
