using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 diff; //カメラとプレイヤーの距離
    public GameObject target; //追従するターゲットオブジェクト
    public float followSpeed; //追従するスピード

    void Start()
    {
        diff = target.transform.position - this.transform.position; //カメラとプレイヤーの初期の距離を指定
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(this.transform.position, target.transform.position - diff, Time.deltaTime * followSpeed); //線形補間関数によるカメラの移動
        transform.LookAt(target.transform); //ターゲットを常に注視する
    }
}
