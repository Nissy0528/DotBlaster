using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitManager : MonoBehaviour
{
    private SplitCreate[] splits;
    private GameObject splitsObj;
    private int splitsNum = 0;

    // Use this for initialization
    void Awake()
    {
        splits = FindObjectsOfType(typeof(SplitCreate)) as SplitCreate[];
        for (int i = 1; i < splits.Length; i++)
        {
            splitsObj = GameObject.Find(splits[i].name);
            splitsObj.GetComponent<SplitCreate>().enabled = false;
        }
        splitsObj = GameObject.Find(splits[splitsNum].name);
    }

    // Update is called once per frame
    void Update()
    {
        splits = FindObjectsOfType(typeof(SplitCreate)) as SplitCreate[];

        StartCoroutine("SplitStart");
    }

    /// <summary>
    /// 順番に分割開始
    /// </summary>
    private IEnumerator SplitStart()
    {
        if (splitsNum < splits.Length - 1 && splitsObj.GetComponent<SplitCreate>().IsStart)
        {
            ++splitsNum;
            splitsObj = GameObject.Find(splits[splitsNum].name);
            splitsObj.GetComponent<SplitCreate>().enabled = true;
        }
        yield return new WaitForSeconds(1f);
    }
}
