using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_GameManager : MonoBehaviour
{
    public float myTime = 0f;
    [SerializeField] Text myTimeUI ;
    [SerializeField] Text myAttempts;
    [SerializeField] Text myJumpCount;
    [SerializeField] GameObject myPlayer;

    [SerializeField] GameObject myFinishUI;
    [SerializeField] GameObject myFinishUI2;
    [SerializeField] Text[] myNameList = new Text[3];
    [SerializeField] Text[] myScore = new Text[3];
    [SerializeField] Text myFinalScore;

    public static string[] myName = new string[100];
    public static int[,] finalChanceCount = new int[100, 2];
    public static int[,] finalJumpCount = new int[100, 2];
    public static float[,] finalTime = new float[100, 2];
    public static int n = 0;
    public InputField inputName;

    // Start is called before the first frame update
    void Start()
    {
        myFinishUI.SetActive(false);
        myFinishUI2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;
        myTimeUI.text = "Time:" + myTime.ToString();
        myAttempts.text = "尝试次数:" + myPlayer.GetComponent<PlayerController>().myChanceCount.ToString();
        myJumpCount.text = "跳跃次数:" + myPlayer.GetComponent<PlayerController>().myJumpCount.ToString();
        if (Input.GetKeyDown(KeyCode.R))
        {
            myPlayer.GetComponent<PlayerController>().Restart();
        }
    }

    public void InputOK()
    {
        myName[n] = inputName.text;
        finalChanceCount[n, 0] = myPlayer.GetComponent<PlayerController>().myChanceCount;
        finalChanceCount[n, 1] = n;
        finalTime[n, 0] = myTime;
        finalTime[n, 1] = n;
        finalJumpCount[n, 0] = myPlayer.GetComponent<PlayerController>().myJumpCount;
        //Debug.Log(finalJumpCount[n, 0]);
        finalJumpCount[n, 1] = n;
        n++;
        SortList();
        ShowList();
    }

    private void SortList2(int[,] a)
    {
        for (int i = 1; i <= n; i++)
        {
            int tmp = a[i, 0], j;
            int tmpName = a[i, 1];
            for (j = i; j > 0 && a[j - 1, 0] >= tmp; j--)
            {
                a[j, 0] = a[j - 1, 0];
                a[j, 1] = a[j - 1, 1];
            }
            a[j, 0] = tmp;
            a[j, 1] = tmpName;
        }
    }
    private void SortList()
    {
        //尝试次数升序
        for (int i = 1; i < n; i++)
        {
            int tmp = finalChanceCount[i, 0], j;
            int tmpName = finalChanceCount[i, 1];
            for (j = i; j > 0 && finalChanceCount[j - 1, 0] >= tmp; j--)
            {
                finalChanceCount[j, 0] = finalChanceCount[j - 1, 0];
                finalChanceCount[j, 1] = finalChanceCount[j - 1, 1];
            }
            finalChanceCount[j, 0] = tmp;
            finalChanceCount[j, 1] = tmpName;
        }
        //时间升序
        for (int i = 1; i < n; i++)
        {
            float tmp = finalTime[i, 0];
            int j;
            float tmpName = finalTime[i, 1];
            for (j = i; j > 0 && finalTime[j - 1, 0] >= tmp; j--)
            {
                finalTime[j, 0] = finalTime[j - 1, 0];
                finalTime[j, 1] = finalTime[j - 1, 1];
            }
            finalTime[j, 0] = tmp;
            finalTime[j, 1] = tmpName;
        }
        //跳跃次数升序
        for (int i = 1; i < n; i++)
        {
            int tmp = finalJumpCount[i, 0], j;
            int tmpName = finalJumpCount[i, 1];
            for (j = i; j > 0 && finalJumpCount[j - 1, 0] >= tmp; j--)
            {
                finalJumpCount[j, 0] = finalJumpCount[j - 1, 0];
                finalJumpCount[j, 1] = finalJumpCount[j - 1, 1];
            }
            finalJumpCount[j, 0] = tmp;
            finalJumpCount[j, 1] = tmpName;
        }
    }
    private void ShowList()
    {
        //改进方法？
        myFinishUI.SetActive(false);
        myFinishUI2.SetActive(true);
        myNameList[0].text = myName[finalChanceCount[0, 1]].ToString();
        myScore[0].text = finalChanceCount[0, 0].ToString();
        myNameList[1].text = myName[(int)finalTime[0, 1]].ToString();
        myScore[1].text = finalTime[0, 0].ToString();
        myNameList[2].text = myName[finalJumpCount[0, 1]].ToString();
        myScore[2].text = finalJumpCount[0, 0].ToString();
        myFinalScore.text = inputName.text +"   尝试次数：" + myPlayer.GetComponent<PlayerController>().myChanceCount.ToString() +
                "   时间：" + myTime.ToString() +
                "   跳跃次数：" + myPlayer.GetComponent<PlayerController>().myJumpCount.ToString();
    }

}
