using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float vx;//速度
    protected Vector3 position;//座標

    private Camera mainCamera;//カメラ
    private Vector3 screenPosMin;//画面の左下
    private Vector3 screemPosMax;//画面右上

    //初期化
    public virtual void Initialize()
    {
        position = transform.position;
        //画面端取得
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        screenPosMin = mainCamera.ScreenToWorldPoint(Vector3.zero);
        screemPosMax = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    //更新
    public virtual void BulletUpdate()
    {
        transform.Translate(transform.right * vx * Time.deltaTime,Space.World);//移動
        position = transform.position;
        Remove();
    }

    /// <summary>
    /// 画面外に出たら消失
    /// </summary>
    private void Remove()
    {
        if (position.x < screenPosMin.x || position.x > screemPosMax.x
            || position.y < screenPosMin.y || position.y > screemPosMax.y)
        {
            Destroy(gameObject);
        }
    }
}
