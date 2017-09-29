using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLog : MonoBehaviour
{
    public GameObject bulletNumber;
    public GameObject shotInterval;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ShowBulletNum();
        //ShowShotInterval();
    }

    /// <summary>
    /// 弾番号を表示
    /// </summary>
    /*
    private void ShowBulletNum()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        Text text = bulletNumber.GetComponent<Text>();

        text.text = "BulletNum : " + player.GetBulletNum().ToString();
    }

    /// <summary>
    /// 球発射間隔を表示
    /// </summary>
    private void ShowShotInterval()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        Text text = shotInterval.GetComponent<Text>();

        text.text = "ShotInterval : " + player.GetShotInterval().ToString();
    }
    */
}
