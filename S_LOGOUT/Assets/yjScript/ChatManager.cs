using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChatManager : MonoBehaviour
{
    public GameObject blueArea, redArea, dateArea;
    public RectTransform contentRect;
    public Scrollbar scrollBar;
    AreaScript lastArea;

    //스크립트 변경
    public MyScript script_Object;
    public int now_Script_Index;
    public int now_Sheet_Index;
    private bool isClicked;
    private bool isGameStart;

    public GameObject explain_Panel;
    public GameObject popup_Panel;
    public GameObject answer2_Panel;
    public GameObject answer3_Panel;

    public GameObject middle_Ending_Panel;

    public string[] Answers;

    public Button Btn2_1, Btn2_2, Btn3_1, Btn3_2, Btn3_3;
    public Text b2_1, b2_2, b3_1, b3_2, b3_3;
    public string text;


    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
        isGameStart = false;
    }

    public void ScriptLoad(int index)
    {
        if (script_Object.sheets[now_Sheet_Index].list.Count <= now_Script_Index)
        {
            return;
        }

        Chat(script_Object.sheets[now_Sheet_Index].list[now_Script_Index].MyTurn,
            script_Object.sheets[now_Sheet_Index].list[now_Script_Index].Script,
            script_Object.sheets[now_Sheet_Index].list[now_Script_Index].Who, null);

        now_Sheet_Index++;

        if (now_Sheet_Index > 1)
        {
            now_Sheet_Index = 0;
            now_Script_Index++;
        }
        isClicked = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isClicked && isGameStart.Equals(true))
        {
            isClicked = true;
            ScriptLoad(now_Script_Index);

            if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 1)
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;

                b2_1.GetComponent<Text>().text = Answers[0];
                b2_2.GetComponent<Text>().text = Answers[1];

                Btn2_1.onClick.AddListener(Answer1_1);
                Btn2_2.onClick.AddListener(Answer1_2);
            }

            if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 3)
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;

                b2_1.GetComponent<Text>().text = Answers[2];
                b2_2.GetComponent<Text>().text = Answers[3];

                Btn2_1.onClick.AddListener(Answer2_1);
                Btn2_2.onClick.AddListener(Answer2_2);

            }

            if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 5)
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;

                b2_1.GetComponent<Text>().text = Answers[4];
                b2_2.GetComponent<Text>().text = Answers[5];

                Btn2_1.onClick.AddListener(Answer3_1);
                Btn2_2.onClick.AddListener(Answer3_2);
            }
        }
    }



    public void Chat(bool isSend, string text, string user, Texture2D picture)
    {
        if (text.Trim() == "")
            return;
        bool isBottom = scrollBar.value <= 0.00001f;
        //print(text);
        AreaScript area = Instantiate(isSend ? blueArea : redArea).GetComponent<AreaScript>();
        area.transform.SetParent(contentRect.transform, false);
        area.boxRect.sizeDelta = new Vector2(600, area.boxRect.sizeDelta.y);
        area.textRect.GetComponent<Text>().text = text;

        Fit(area.boxRect);

        float X = area.textRect.sizeDelta.x + 42;
        float Y = area.textRect.sizeDelta.y;

        if (Y > 49)
        {
            for (int i = 0; i < 200; i++)
            {
                area.boxRect.sizeDelta = new Vector2(X - i * 2, area.boxRect.sizeDelta.y);
                Fit(area.boxRect);

                if (Y != area.textRect.sizeDelta.y)
                {
                    area.boxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y);
                    break;
                }
            }
        }
        else
        {
            area.boxRect.sizeDelta = new Vector2(X, Y);
        }
        

        //데이트박스 오류

        /*DateTime t = DateTime.Now;

        area.time = t.ToString("yyyy-MM-dd-HH-mm");
        area.user = user;

        int hour = t.Hour;

        if (t.Hour == 0)
        {
            hour = 12;
        }
        else if (t.Hour > 12)
        {
            hour -= 12;
        }

        area.timeText.text = (t.Hour > 12 ? "오후" : "오전") + hour + ":" + t.Minute.ToString("D2");

        bool isSame = lastArea != null && lastArea.time == area.time && lastArea.user == area.user;

        if (isSame)
        {
            lastArea.timeText.text = "";

        }

        area.tail.SetActive(!isSame);

        if (!isSend)
        {
            area.userImage.gameObject.SetActive(!isSame);
            area.userText.gameObject.SetActive(!isSame);
            area.userText.text = area.user;

            if (picture != null)
            {
                area.userImage.sprite = Sprite.Create(picture, new Rect(0, 0, picture.width, picture.height), new Vector2(0.5f, 0.5f));
            }
        }


        if (lastArea != null)
            Debug.Log(lastArea.time.Substring(0, 10));
        //Debug.Log(area.time.Substring(0, 10);

        if (lastArea != null && lastArea.time.Substring(0, 10) != area.time.Substring(0, 10))
        {
            Transform curDateArea = Instantiate(dateArea).transform;

            curDateArea.SetParent(contentRect.transform, false);
            curDateArea.SetSiblingIndex(curDateArea.GetSiblingIndex() - 1);

            string week = "";
            switch (t.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    week = "일";
                    break;
                case DayOfWeek.Monday:
                    week = "월";
                    break;
                case DayOfWeek.Tuesday:
                    week = "화";
                    break;
                case DayOfWeek.Wednesday:
                    week = "수";
                    break;
                case DayOfWeek.Thursday:
                    week = "목";
                    break;
                case DayOfWeek.Friday:
                    week = "금";
                    break;
                case DayOfWeek.Saturday:
                    week = "토";
                    break;
            }

            curDateArea.GetComponent<AreaScript>().dateText.text = t.Year + "년" + t.Month + "월" + t.Day + "일" + week + "요일";

        }

        Fit(area.boxRect);
        Fit(area.areaRect);
        Fit(contentRect);
        lastArea = area;


        if (!isSend && !isBottom)
            return;

        Invoke("ScrollDelay", 0.03f);*/

    }

    void Fit(RectTransform rect)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
    }

    void ScrollDelay()
    {
        scrollBar.value = 0;
    }

    public void Game_Start()
    {
        explain_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Popup_Open()
    {
        popup_Panel.SetActive(true);
        isGameStart = false;
    }

    public void Popup_Ok()
    {
        //초기엔딩과 연결
        popup_Panel.SetActive(false);
        middle_Ending_Panel.SetActive(true);
    }

    public void Popup_Close()
    {
        popup_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Answer1_1()
    {
        Chat(true, text, "나", null);
        text = ("넹 저야 땡큐죠 ㅋㅋㅋ");

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

    public void Answer1_2()
    {
        //초기엔딩과 연결

    }

    public void Answer2_1()
    {
        Chat(true, text, "나", null);
        text = ("저 비싼데 ㅋㅋㅋ");

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

    public void Answer2_2()
    {
        Chat(true, text, "나", null);
        text = ("죄송해요… 사진은 좀…");

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

    public void Answer3_1()
    {
        Chat(true, text, "나", null);
        text = ("ㅋㅋㅋㅋㅋㅋㅋㅋㅋ하나씩 보내요 하나씩 ㅋㅋㅋ");

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

    public void Answer3_2()
    {
        Chat(true, text, "나", null);
        text = ("ㄴㄴㄴ 싫어요");

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

}

