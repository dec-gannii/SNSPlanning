using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class gChatManager : MonoBehaviour
{

    public GameObject blueArea, redArea, blueAreaImage, redAreaImage, dateArea;
    public RectTransform contentRect;
    public Scrollbar scrollBar;
    AreaScript lastArea;

    public main_g script_Object;
    public int now_Script_Index;
    public int now_Sheet_Index;
    private bool isClicked;
    private bool isGameStart;

    public g_1_2 script_Object2;
    public int now_Script_Index2;
    public int now_Sheet_Index2;

    public bool isSubScript1;
    public bool isLoadSubScript1;
    public bool isSubScript2;
    public bool isLoadSubScript2;
    public bool isPhotoSend;

    public GameObject explain_Panel;
    public GameObject popup_Panel;
    public GameObject answer2_Panel;

    public string[] Answers;

    public Button Btn2_1, Btn2_2;
    public Text b2_1, b2_2;
    public string text;

    Sprite[] message_Sprites;

    // Start is called before the first frame update
    void Start()
    {
        message_Sprites = Resources.LoadAll<Sprite>("GaEun/Message_Images");
        isClicked = false;
        isGameStart = true;
        isSubScript1 = false;
        isLoadSubScript1 = false;
        isPhotoSend = false;
    }

    public void ScriptLoad(int index)
    {
        Chat(script_Object.sheets[now_Sheet_Index].list[now_Script_Index].MyTurn,
            script_Object.sheets[now_Sheet_Index].list[now_Script_Index].Script,
            script_Object.sheets[now_Sheet_Index].list[now_Script_Index].Who, null);

        now_Script_Index++;
        isClicked = false;
    }

    public void ScriptLoad2(int index)
    {
        Chat(script_Object2.sheets[now_Sheet_Index2].list[now_Script_Index2].MyTurn,
            script_Object2.sheets[now_Sheet_Index2].list[now_Script_Index2].Script,
            script_Object2.sheets[now_Sheet_Index2].list[now_Script_Index2].Who, null);

        now_Script_Index2++;
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !isClicked && isGameStart.Equals(true))
        {
            isClicked = true;

            if (isPhotoSend.Equals(false))
            {
                if (isSubScript1.Equals(true))
                {
                    ScriptLoad2(now_Script_Index2);
                }

                else
                {
                    ScriptLoad(now_Script_Index);
                }
            }

            if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 24)
            {
                Chat(false, text, "상대방", null, message_Sprites[1]);
            }

            else if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 46)
            {
                Chat(false, text, "상대방", null, message_Sprites[1]);
            }

            else if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 79 && isLoadSubScript1.Equals(false))
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;

                b2_1.GetComponent<Text>().text = Answers[0];
                b2_2.GetComponent<Text>().text = Answers[1];

                Btn2_1.onClick.AddListener(Answer1_1);
                Btn2_2.onClick.AddListener(Answer1_2);

            }

            else if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 125)
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;


                b2_1.GetComponent<Text>().text = Answers[2];
                b2_2.GetComponent<Text>().text = Answers[3];

                Btn2_1.onClick.RemoveListener(Answer1_1);
                Btn2_2.onClick.RemoveListener(Answer1_2);

                Btn2_1.onClick.AddListener(Answer2_1);
                Btn2_2.onClick.AddListener(Answer2_2);

            }

            else if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 127)
            {
                Chat(false, text, "상대방", null, message_Sprites[1]);
            }
            
            else if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 136)
            {
                isGameStart = false;
                Invoke("Rc_Final_End", 2f);
            }

            if (now_Script_Index2.Equals(32) && isSubScript1.Equals(true))
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;

                b2_1.GetComponent<Text>().text = Answers[4];
                b2_2.GetComponent<Text>().text = Answers[5];


                Btn2_1.onClick.RemoveListener(Answer1_1);
                Btn2_2.onClick.RemoveListener(Answer1_2);

                Btn2_1.onClick.AddListener(Answer1_1_1);
                Btn2_2.onClick.AddListener(Answer1_1_2);
            }

            else if (now_Script_Index2.Equals(34) && isSubScript1.Equals(true))
            {
                Chat(false, text, "상대방", null, message_Sprites[1]);
            }

            else if (now_Script_Index2.Equals(40) && isSubScript1.Equals(true))
            {
                isGameStart = false;
                Invoke("Rc_Final_End", 2f);
            }
        }
    }

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

    public void Rc_End()
    {
        Sound_Manager.instance.PlaySFX(1);
        TotalGameManager.instance.Set_Popup_Clicked(true);
        SceneManager.LoadScene("Look_Around_Scene");
    }

    public void Rc_Final_End()
    {
        Sound_Manager.instance.PlaySFX(0);
        TotalGameManager.instance.Set_Is_Ended(true);
        SceneManager.LoadScene("Look_Around_Scene");
    }

    public void Answer1_1()
    {
        text = "2xxxxx-xxxxxxx 여기!";
        Chat(true, text, "나", null);

        answer2_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Answer1_2()
    {
        isSubScript1 = true;
        ScriptLoad2(now_Script_Index2);
        isLoadSubScript1 = true;

        answer2_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Answer2_1()
    {
        //최종엔딩
        Chat(true, text, "나", null, message_Sprites[1]);
        Invoke("Rc_Final_End", 2f);
    }

    public void Answer2_2()
    {
        answer2_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Answer1_1_1()
    {
        answer2_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Answer1_1_2()
    {
        //초기엔딩
        Invoke("Rc_Final_End", 2f);
    }
}
