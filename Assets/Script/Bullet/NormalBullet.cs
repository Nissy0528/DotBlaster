using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    public float speed;//速度

    // Use this for initialization
    void Start()
    {
        vx = speed;//速度設定
        Initialize();//初期化
    }

    // Update is called once per frame
    void Update()
    {
        BulletUpdate();//更新
    }

    /// <summary>
    /// あたり判定
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        //プレイヤー以外に当たったら消滅
        if (col.gameObject.tag != "Player")
        {
            if (col.gameObject.tag == "Core")
            {
                //Destroy(col.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
