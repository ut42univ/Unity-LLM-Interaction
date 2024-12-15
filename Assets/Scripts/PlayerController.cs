using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed; //普段の移動速度
    public float dashSpeed; //ダッシュ時の移動速度
    public float jumpPower; //ジャンプ力
    private CharacterController controller;
    private Vector3 moveVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>(); //CharacterControllerを取得
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)) //Ctrlキーが押されている場合
        {
            moveVelocity.x = Input.GetAxis("Horizontal") * dashSpeed; //横軸の入力を取得
            moveVelocity.z = Input.GetAxis("Vertical") * dashSpeed; //縦軸の入力を取得
        }
        else
        {
            moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed; //横軸の入力を取得
            moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed; //縦軸の入力を取得
        }

        transform.LookAt(this.transform.position + new Vector3(moveVelocity.x, 0, moveVelocity.z));

        if (controller.isGrounded) //オブジェクトが地面にいる場合
        {
            if (Input.GetKeyDown(KeyCode.Space)) //スペースキー入力すると
            {
                moveVelocity.y = jumpPower; //Y軸正の方向に移動
            }
        }
        else
        {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime; //重力によるY軸負の方向への移動
        }

        controller.Move(moveVelocity * Time.deltaTime); //オブジェクトを動かす
    }
}
