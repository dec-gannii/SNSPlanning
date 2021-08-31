using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ChatManager : MonoBehaviour
{
    public GameObject blueArea, redArea, blueAreaImage, redAreaImage, dateArea;
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

    public string[] Answers;

    public Button Btn2_1, Btn2_2, Btn3_1, Btn3_2, Btn3_3;
    public Text b2_1, b2_2, b3_1, b3_2, b3_3;
    public string text;

    Sprite[] message_Sprites;

    // Start is called before the first frame update
    void Start()
    {
        message_Sprites = Resources.LoadAll<Sprite>("GaEun/Message_Images");
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
            else if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 3)
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;

                b2_1.GetComponent<Text>().text = Answers[2];
                b2_2.GetComponent<Text>().text = Answers[3];

                Btn2_1.onClick.AddListener(Answer2_1);
                Btn2_2.onClick.AddListener(Answer2_2);

            }
            else if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 5)
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

    // 이미지 전송할 때 필요한 매개변수로 messagePicture 추가 (없는 경우도 있으니까 default 매개변수로 null 지정)
    public void Chat(bool isSend, string text, string user, Texture2D picture, Sprite messagePicture = null)
    {
        if (text.Trim() == "")
            return;

        bool isBottom = scrollBar.value <= 0.00001f;

        AreaScript area;

        // 이미지를 보내는 게 아니라면
        if (messagePicture != null)
        {
            // 텍스트는 그냥 없애고
            text = "";

            // 내가 보내는 거면 blueAreaImage, 상대방이 보내는 거면 redAreaImage
            area = Instantiate(isSend ? blueAreaImage : redAreaImage).GetComponent<AreaScript>();

            // area의 messageImage의 sprite를 매개변수로 전달받은 이미지로 설정
            area.messageImage.sprite = messagePicture;
            // content 아래로 생기도록 하기
            area.transform.SetParent(contentRect.transform, false);

            // 보내는 이미지 사이즈에 맞춰서 칸 사이즈 조절
            Fit(area.messageImage.GetComponent<RectTransform>());
        }
        else
        {
            area = Instantiate(isSend ? blueArea : redArea).GetComponent<AreaScript>();

            area.transform.SetParent(contentRect.transform, false);

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

        }

        DateTime t = DateTime.Now;

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
        area.timeText.text = (t.Hour > 12 ? "오후 " : "오전 ") + hour + ":" + t.Minute.ToString("D2");

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
            curDateArea.GetComponent<AreaScript>().dateText.text = t.Year + "년 " + t.Month + "월 " + t.Day + "일 " + week + "요일";
        }

        Fit(area.boxRect);
        Fit(area.areaRect);
        Fit(contentRect);
        lastArea = area;

        if (!isSend && !isBottom)
            return;

        Invoke("ScrollDelay", 0.03f);

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
        TotalGameManager.instance.Set_Popup_Clicked(true);
        SceneManager.LoadScene("Look_Around_Scene");
    }

    public void Popup_Close()
    {
        popup_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Answer1_1()
    {
        //Chat(true, text, "나", null);
        text = "넹 저야 땡큐죠 ㅋㅋㅋ";
        //내가 이미지 포함해서 보내면
        Chat(true, text, "나", null, message_Sprites[0]);

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

    public void Answer1_2()
    {
        TotalGameManager.instance.Set_Popup_Clicked(true);
        SceneManager.LoadScene("Look_Around_Scene");
    }

    public void Answer2_1()
    {
        // 상대방이 이미지 포함해서 보내면
        //Chat(true, text, "나", null, message_Sprites[1]);
        Chat(false, text, "상대방", null, message_Sprites[1]);
        text = "test";

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

    public void Answer2_2()
    {
        Chat(true, text, "나", null, null);
        text = "죄송해요… 사진은 좀…";

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

    public void Answer3_1()
    {
        Chat(true, text, "나", null, null);
        text = "ㅋㅋㅋㅋㅋㅋㅋㅋㅋ하나씩 보내요 하나씩 ㅋㅋㅋ";

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

    public void Answer3_2()
    {
        Chat(true, text, "나", null, null);
        text = "ㄴㄴㄴ 싫어요";

        answer2_Panel.SetActive(false);
        isGameStart = true;

    }

}

