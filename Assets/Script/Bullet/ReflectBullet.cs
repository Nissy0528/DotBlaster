using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBullet : Bullet
{
    public float speed;//速度
    public float destroyTime;//消滅時間

    private Rigidbody2D rigid;
    private GameObject parent;

    // Use this for initialization
    void Start()
    {
        vx = 0;
        Initialize();//初期化
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.right * speed, ForceMode2D.Impulse);//前方に加速

        //親オブジェクト登録
        if (transform.parent != null)
        {
            parent = transform.parent.gameObject;
            transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        BulletUpdate();
        ParentDestroy();
        Dead();
    }

    /// <summary>
    /// 親オブジェクト削除
    /// </summary>
    private void ParentDestroy()
    {
        if (parent == null) return;

        //親オブジェクトに子オブジェクトがいなければ削除
        if (parent.transform.childCount == 0)
        {
            Destroy(parent);
        }
    }

    /// <summary>
    /// 消滅
    /// </summary>
    private void Dead()
    {
        destroyTime -= Time.deltaTime;//消滅時間カウント

        //消滅時間になったら消滅
        if (destroyTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// あたり判定
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        //プレイヤー以外に当たったら消滅
        if (col.transform.tag == "Enemy")
        {
            if (col.gameObject.tag == "Core")
            {
                Destroy(col.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
