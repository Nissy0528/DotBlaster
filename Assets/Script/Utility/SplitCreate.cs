using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitCreate : MonoBehaviour
{
    public GameObject maskObj;//画像にマスクをかけるオブジェクト
    public GameObject split;//分割した子オブジェクト
    public int splitValue;//削れる大きさ

    private List<GameObject> colliders = new List<GameObject>();//子オブジェクトのリスト
    private List<Vector2> childrenPos = new List<Vector2>();//子オブジェクトを配置する座標リスト
    private Vector3 splitSize;//分割した大きさ
    private bool isStart = false;//子オブジェクト生成完了判定

    // Use this for initialization
    void Start()
    {
        IniCreateChild();//子オブジェクト生成
        int childCnt = transform.childCount;//子オブジェクトの数
        //リストに現在の子オブジェクトを追加
        for (int i = 0; i < childCnt; i++)
        {
            colliders.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        IniSetCollider();

        if (colliders.Count == 0 && transform.Find("Core") == null)
        {
            Destroy(gameObject);
        }

        colliders.RemoveAll(c => c.GetComponent<BoxCollider2D>().enabled == false);
    }

    /// <summary>
    /// あたり判定
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!isStart) return;

        if (col.gameObject.tag == "Attack" && colliders.Count > 0)
        {
            GameObject nearObj = NearCollision(col.gameObject, colliders);//弾と最も近い子オブジェクト
            Vector3 pos = nearObj.transform.position;
            GameObject mask = Instantiate(maskObj, pos, Quaternion.identity, transform);//マスクオブジェクトを生成
            mask.transform.localScale = splitSize;
            SetChild(int.Parse(nearObj.name));
            nearObj.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (!isStart) return;

        if (col.gameObject.tag == "Attack" && colliders.Count > 0)
        {
            GameObject nearObj = NearCollision(col.gameObject, colliders);//弾と最も近い子オブジェクト
            Vector3 pos = nearObj.transform.position;
            GameObject mask = Instantiate(maskObj, pos, Quaternion.identity, transform);//マスクオブジェクトを生成
            mask.transform.localScale = splitSize;
            SetChild(int.Parse(nearObj.name));
            nearObj.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    /// <summary>
    /// 弾と最も近い子オブジェクトを返す
    /// </summary>
    /// <param name="bullet">弾</param>
    /// <param name="c_objs">子オブジェクト</param>
    /// <returns></returns>
    private GameObject NearCollision(GameObject bullet, List<GameObject> c_objs)
    {
        float tmpDis = 0;//距離用一時変数
        float nearDis = 0;//最も近いオブジェクトの距離

        GameObject targetObj = null;//最も近いオブジェクト

        //子オブジェクトを一つずつ取得
        foreach (GameObject obj in c_objs)
        {
            //弾と子オブジェクトとの距離を取得
            tmpDis = Vector3.Distance(obj.transform.position, bullet.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクトを取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                targetObj = obj;
            }
        }

        //最も近かったオブジェクトを返す
        return targetObj;
    }

    /// <summary>
    /// 子オブジェクトを生成（初期）
    /// </summary>
    private void IniCreateChild()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();//スプライト取得
        Vector2 spriteSize = sprite.bounds.size;//スプライトの画像サイズ取得
        splitSize = new Vector3(spriteSize.x / splitValue, spriteSize.y / splitValue, 1);//画像サイズを分割
        //初期座標設定
        float inix = -spriteSize.x / 2 + (splitSize.x / 2);
        float iniy = spriteSize.y / 2 - (splitSize.y / 2);
        float x = 0.0f;
        float y = 0.0f;
        GameObject child = null;
        for (int i = 0; i < splitValue * splitValue; i++)
        {
            //並ぶように座標を更新
            x = inix + (splitSize.x * (i % splitValue));
            y = iniy - (splitSize.y * (i / splitValue));

            childrenPos.Add(new Vector2(x, y));

            if (i <= splitValue || i % splitValue == 0 || i % splitValue == splitValue - 1 || i >= splitValue * (splitValue - 1))
            {
                //子オブジェクトを生成
                child = Instantiate(split, gameObject.transform);
                child.name = i.ToString();
            }
        }
    }

    /// <summary>
    /// 子オブジェクトをパーツ用に設定（初期）
    /// </summary>
    private void IniSetCollider()
    {
        if (isStart) return;

        GameObject child = null;
        //各子オブジェクトに処理
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            child.GetComponent<BoxCollider2D>().size = splitSize;//あたり判定のサイズを分割サイズに
            child.transform.localPosition = childrenPos[int.Parse(child.name)];//並ぶように座標を設定
        }

        isStart = true;
    }

    /// <summary>
    /// 子オブジェクト生成
    /// </summary>
    /// <param name="childNum">破壊された子オブジェクト</param>
    private void SetChild(int childNum)
    {
        int top = childNum - splitValue;//一つ上
        int under = childNum + splitValue;//一つ下
        int left = childNum - 1;//一つ左
        int right = childNum + 1;//一つ右

        //一つ上に生成
        CreateChild(top);
        //一つ下に生成
        CreateChild(under);
        //一つ左に生成
        CreateChild(left);
        //一つ右に生成
        CreateChild(right);
    }
    private void CreateChild(int num)
    {
        if (transform.Find(num.ToString()) != null) return;

        GameObject child = null;
        if (num > 0 && num < splitValue * splitValue)
        {
            //子オブジェクトを生成
            child = Instantiate(split, gameObject.transform);
            child.name = num.ToString();
            child.GetComponent<BoxCollider2D>().size = splitSize;//あたり判定のサイズを分割サイズに
            child.transform.localPosition = childrenPos[num];//並ぶように座標を設定
            colliders.Add(child);
        }
    }

    /// <summary>
    /// 子オブジェクト生成完了判定のゲッター
    /// </summary>
    public bool IsStart
    {
        get { return isStart; }
    }
}
