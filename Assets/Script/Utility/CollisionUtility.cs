using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionUtility : MonoBehaviour
{
    /// <summary>
    /// 円と長方形のあたり判定
    /// </summary>
    /// <param name="boxPos">長方形の座標</param>
    /// <param name="circlePos">円の座標</param>
    /// <param name="boxSize">長方形の大きさ</param>
    /// <param name="radius">円の大きさ</param>
    /// <returns></returns>
    public static bool CircleBoxCollision(Vector2 boxPos, Vector2 circlePos, float boxSize, float radius)
    {
        Vector2 boxLT_Pos = new Vector2(boxPos.x - boxSize, boxPos.y + boxSize);//長方形の左上の座標
        Vector2 boxRB_Pos = new Vector2(boxPos.x + boxSize, boxPos.y - boxSize);//長方形の右下の座標

        //条件１
        //長方形の横中央に入っているか
        bool A = (circlePos.x > boxLT_Pos.x)
            && (circlePos.x < boxRB_Pos.x)
            && (circlePos.y > boxLT_Pos.y - radius)
            && (circlePos.y < boxRB_Pos.y + radius);
        //長方形の縦中央に入っているか
        bool B = (circlePos.x > boxLT_Pos.x - radius)
            && (circlePos.x < boxRB_Pos.x + radius)
            && (circlePos.y > boxLT_Pos.y)
            && (circlePos.y < boxRB_Pos.y);

        //条件２
        //長方形の左上が円に入っているか
        bool C = ((boxLT_Pos.x - circlePos.x) * (boxLT_Pos.x - circlePos.x)
            + (boxLT_Pos.y - circlePos.y) * (boxLT_Pos.y - circlePos.y)) < radius * radius;
        //長方形の右上が円に入っているか
        bool D = ((boxRB_Pos.x - circlePos.x) * (boxRB_Pos.x - circlePos.x)
            + (boxLT_Pos.y - circlePos.y) * (boxLT_Pos.y - circlePos.y)) < radius * radius;
        //長方形の左下が円に入っているか
        bool E = ((boxRB_Pos.x - circlePos.x) * (boxRB_Pos.x - circlePos.x)
            + (boxRB_Pos.y - circlePos.y) * (boxRB_Pos.y - circlePos.y)) < radius * radius;
        //長方形の右下が円に入っているか
        bool F = ((boxLT_Pos.x - circlePos.x) * (boxLT_Pos.x - circlePos.x)
            + (boxRB_Pos.y - circlePos.y) * (boxRB_Pos.y - circlePos.y)) < radius * radius;

        //どれかの判定が当てはまっていたら当たっている
        if (A || B || C || D || E || F)
        {
            return true;
        }

        return false;
    }
}
