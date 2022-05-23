using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCtrl : NetworkBehaviour
{
    [SyncVarAttribute] public float runSpeed=5;
    [SyncVarAttribute] public float walkSpeed=10;
    public float turnSmoothTime = 0.13f;//设定角色转向平滑时间
    float turnSmoothVelocity;//平滑函数需要这么一个平滑加速度, 不需要为他赋值, 但是需要把这个变量当参数传入
    public float speedSmoothTime = 0.13f;//用于平滑速度
    float speedSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            // 如果不是本地玩家，则退出更新
            Debug.Log("不是本地玩家");
            return;
        }

        // 处理玩家的移动输入
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized; bool running = Input.GetKey(KeyCode.LeftShift);//根据是否按下左shift设定一个布尔变量在下一句设定是奔跑速度还是走路速度

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        //根据布尔变量获取速度后乘以输入向量的长度,当没有任何键盘输入时,这个长度是0,有任何输入时长度都是1,所以其实是用来在键盘没有输入时将目标速度置0用的
        transform.Translate(transform.forward * targetSpeed * Time.deltaTime, Space.World);
        //***转向部分代码***//
        if (inputDir != Vector2.zero)
        {
            //***加入鼠标干预***//
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            //这个cameraT是引用的主摄像机(也不知道具体是不是引用,等回学校看看headfirstC#),传入了主摄像机的y值,而在后面摄像机的控制代码中,这个y值是受鼠标控制的
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            //上边这个函数是角度渐变, 也可以叫平滑吧, 这个ref是什么意思, 以后再说, 还有这个turnSmoothVelocity也以后再说
        }
    }
}
