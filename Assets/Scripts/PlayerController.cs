using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    //基础数据
    [SerializeField] GameObject myGameManager;
    [SerializeField] GameObject myStartPoint;
    public int myChanceCount = 1;
    public int myJumpCount = 0;
    //视角控制
    [SerializeField] GameObject myViewPoint;
    [SerializeField] Vector2 myRotateSpeed;
    //跳跃
    [SerializeField] GameObject myCamera;
    private Vector3 myDirection;
    public float myCharge;
    [SerializeField] Rigidbody myRigidbody;
    private bool isJumping = false;
    //UI
    [SerializeField] GameObject myUI;
    [SerializeField] GameObject myFinishUI;
    [SerializeField] GameObject myFinishUI2;
    [SerializeField] GameObject myBar;
    [SerializeField] Text myFinalScore;

    private void Start()
    {
        //myChanceCount = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame

    void Update()
    {
        UpdateRotation();
        //Debug.Log(isJumping);
        if (isJumping == false)
            Jump();
        Bar();
    }
    private void UpdateRotation()//视角控制
    {
        float t_deltaX = Input.GetAxis("Mouse X");
        float t_deltaY = Input.GetAxis("Mouse Y") * -1;

        myViewPoint.transform.Rotate(Vector3.up, t_deltaX * Time.deltaTime * myRotateSpeed.x);
        myViewPoint.transform.Rotate(Vector3.right, t_deltaY * Time.deltaTime * myRotateSpeed.y);
        Vector3 temp=myViewPoint.transform.rotation.eulerAngles;
        temp.z = 0;
        //Debug.Log(temp.x);
        if (temp.x >= 80 && temp.x <= 90) temp.x = 80;
        if (temp.x <= 360 && temp.x >= 340) temp.x = 0;
        //if (temp.x <= 280 && temp.x >= 270) temp.x = 280;
        myViewPoint.transform.rotation = Quaternion.Euler(temp);
    }

    private void Bar()
    {
        myBar.transform.LookAt(myCamera.transform.position);
    }

    private void Jump()
    {
        
        if (Input.GetKey(KeyCode.Space))//蓄力条
        {
            myCharge += Time.deltaTime;
            if (myCharge >= 1f)
                myCharge = 1f;
            if (myCharge <= 0.1f)
                myCharge = 0.1f;
            Debug.Log(myCharge);

            //设置蓄力条UI
            myBar.GetComponent<CS_Bar>().SetValue(myCharge / 1f);

        }
        if (Input.GetKeyUp(KeyCode.Space))//跳跃
        {
            myDirection = myCamera.transform.forward;
            myDirection.y = 0;
            myDirection = myDirection.normalized;
            //myDirection.y = 1;
            //myDirection = myDirection.normalized;
            
            //发力y轴方向随蓄力改变
            myDirection.y = 5 * myCharge;
            myDirection = myDirection.normalized;
            
            myRigidbody.AddForce(myDirection * myCharge * 1000);
            
            myCharge = 0f;
            isJumping = true;
            myJumpCount++;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<CS_Plane>() == true)
        {

            myBar.GetComponent<CS_Bar>().SetValue(myCharge / 1f);
            isJumping = false;
        }
        if (collision.transform.GetComponent<CS_RestartPlane>() == true)
        {
            myBar.GetComponent<CS_Bar>().SetValue(myCharge / 1f);
            this.transform.position = myStartPoint.transform.position;
            myChanceCount++;
            myJumpCount = 0;
            myGameManager.GetComponent<CS_GameManager>().myTime = 0f;
        }
        if (collision.transform.GetComponent<CS_Finsh>() == true)
        {
            myUI.SetActive(false);
            myFinishUI.SetActive(true);
            myFinalScore.text = "尝试次数：" + myChanceCount.ToString() +
                "   时间：" + myGameManager.GetComponent<CS_GameManager>().myTime.ToString() +
                "   跳跃次数：" + myJumpCount.ToString();
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void Restart() 
    {
        this.transform.position = myStartPoint.transform.position;
        myChanceCount = 1;
        myJumpCount = 0;
        myGameManager.GetComponent<CS_GameManager>().myTime = 0f;
        myUI.SetActive(true);
        myFinishUI.SetActive(false);
        myFinishUI2.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
