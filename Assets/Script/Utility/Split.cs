using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    private GameObject core;//敵のコア
    private Transform parent;//親オブジェクト
    private Vector3 coreSize;//コアのサイズ
    private Vector3 size;//自分のサイズ

    void Start()
    {
        parent = transform.parent;//親オブジェクト取得
    }

    void Update()
    {
        if (parent.tag != "Enemy") return;//親が敵じゃなければ何もしない

        core = parent.Find("Core").gameObject;//敵のコア取得
        coreSize = core.GetComponent<SpriteRenderer>().bounds.size;//コアのサイズ取得
        size = GetComponent<BoxCollider2D>().size;//自分のサイズ取得

        //コアと当たったら
        if (CollisionUtility.CircleBoxCollision(transform.position, core.transform.position, size.x / 2, coreSize.x / 2))
        {
            GetComponent<BoxCollider2D>().enabled = false;//あたり判定を非アクティブ化
        }
    }
}
