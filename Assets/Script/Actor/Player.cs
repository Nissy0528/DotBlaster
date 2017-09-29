using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;//移動速度
    public float jumpSpeed;//ジャンプ速度
    public float shotIntaval;//発射間隔
    //public GameObject[] bullets;//弾

    private Camera mainCamera;//カメラ
    private Vector3 screenPosMin;//画面の左下
    private Vector3 screemPosMax;//画面右上
    private Vector3 size;//プレイヤーのサイズ
    private Rigidbody2D rigid;//リジッドボディ
    private Animator anim;//アニメーション
    private bool isJump = false;//ジャンプ判定
    private bool isAttack = false;//攻撃判定
    //private float shotCnt = 0;//発射カウント
    //private int bulletNum = 0;//弾の番号

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        screenPosMin = mainCamera.ScreenToWorldPoint(Vector3.zero);
        screemPosMax = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        size = transform.localScale;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FloorHit();//床とのあたり判定
        if (!isAttack)
        {
            Move();//移動
            Rotate();//向き変更
            Jump();//ジャンプ
        }
        Attack();//攻撃
        //Shot();//発砲
        //BulletChange();//弾切り替え
        Clamp();//画面内に制限
    }

    /// <summary>
    /// プレイヤーを画面内に収める
    /// </summary>
    private void Clamp()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, screenPosMin.x + size.x / 10, screemPosMax.x - position.x / 10);//横移動の制限
        transform.position = position;
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        //左右移動
        float h_axis = Input.GetAxisRaw("Horizontal");
        float vx = h_axis * speed * Time.deltaTime;
        transform.Translate(transform.right * vx);

        if (h_axis != 0.0f)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    /// <summary>
    /// 向き変更
    /// </summary>
    private void Rotate()
    {
        float h_axis = Input.GetAxisRaw("Horizontal");//左右入力
        //右向きに
        if (h_axis > 0.0f)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 1);
        }
        //左向きに
        if (h_axis < 0.0f)
        {
            transform.localRotation = new Quaternion(0, -180, 0, 1);
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        //ジャンプボタンが押されたら上にジャンプ（ジャンプ中は不可）
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            isJump = true;
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        var animState = anim.GetCurrentAnimatorStateInfo(0);
        GameObject attackCol = transform.GetChild(0).gameObject;

        if (Input.GetButtonDown("Shot") && !isAttack)
        {
            anim.SetTrigger("Shot");
            attackCol.SetActive(true);
            isAttack = true;
        }
        if (animState.IsName("Player_Shot") && animState.normalizedTime >= 1 && isAttack)
        {
            attackCol.SetActive(false);
            isAttack = false;
        }
    }

    /// <summary>
    /// 発砲
    /// </summary>
    /*
    private void Shot()
    {
        //装備している球を発射
        if (Input.GetButton("Shot") && shotCnt <= 0)
        {
            Vector3 m_pos = GameObject.Find("Muzzle").transform.position;
            Vector3 pos = new Vector3(m_pos.x, transform.position.y, transform.position.z);
            GameObject bullet = Instantiate(bullets[bulletNum], pos, transform.localRotation);
            anim.SetTrigger("Shot");
            //貫通弾ならインターバルを増やす
            if (bulletNum == 1)
            {
                shotCnt = shotIntaval * 10;
            }
            else
            {
                shotCnt = shotIntaval;
            }
        }
        if (shotCnt > 0)
        {
            shotCnt = Mathf.Max(shotCnt -= Time.deltaTime, 0.0f);
        }
    }
    */

    /// <summary>
    /// 床とのあたり判定（Rayで判定）
    /// </summary>
    private void FloorHit()
    {
        Ray2D ray = new Ray2D(transform.position + new Vector3(0, -0.5f, 0), -transform.up);//Ray生成
        float distance = 0.1f;//Rayの距離
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance);//Rayと当たったか

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);//Rayを可視化

        if (!hit.collider)
        {
            if (isJump)
            {
                isJump = false;
            }
        }
        //ジャンプ中にRayが床に当たったら再びジャンプ可能に
        else
        {
            if (hit.transform.tag == "Floor" && isJump)
            {
                isJump = false;
            }
        }
    }

    /// <summary>
    /// 弾切り替え
    /// </summary>
    /*
    private void BulletChange()
    {
        if (Input.GetButtonDown("Change"))
        {
            bulletNum++;
            if (bulletNum >= bullets.Length)
            {
                bulletNum = 0;
            }
        }
    }

    /// <summary>
    /// 弾番号取得
    /// </summary>
    /// <returns></returns>
    public int GetBulletNum()
    {
        return bulletNum;
    }
    /// <summary>
    /// 球発射間隔取得
    /// </summary>
    /// <returns></returns>
    public float GetShotInterval()
    {
        return shotCnt;
    }
    */
}
