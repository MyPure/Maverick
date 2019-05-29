using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //玩家控制器
    public CharacterController characterController;

    //玩家速度
    public float playerSpeed;

    //跳跃速度
    public float jumpSpeed;

    //主摄像机
    private Camera mainCamera;

    //刚体
    //public Rigidbody rigidBody;

    //重力
    private float gravity = 9.8f;

    //移动方向
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();

        //基础上升为1.5个单位，初速度应为sqrt(30),但不知为什么不对
        //jumpSpeed = Mathf.Sqrt(30);
        jumpSpeed = 8;
        playerSpeed = 4;
    }

    //玩家在x轴上移动的间隔距离
    private Vector3 deltaPosition;

    // Update is called once per frame
    void Update()
    {
        //记录下移动前的位置
        Vector3 oldPosition = gameObject.transform.position;

        //玩家移动
        PlayerMove();

        //求出移动的距离
        deltaPosition = gameObject.transform.position - oldPosition;

    }

    private void LateUpdate()
    {
        //摄像机(x方向上)跟随
        mainCamera.transform.position += new Vector3(deltaPosition.x,0,0);
    }

    /// <summary>
    /// 读取键盘输入，控制玩家移动
    /// </summary>
    void PlayerMove()
    {
        #region 左右移动

        characterController.SimpleMove(Input.GetAxis("Horizontal") * playerSpeed * Vector3.right);

        #endregion

        #region 上下移动（跳跃）

        //在地面上时才可以跳跃
        if (characterController.isGrounded)
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpSpeed;

        //模拟重力
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        #endregion
    }
}
