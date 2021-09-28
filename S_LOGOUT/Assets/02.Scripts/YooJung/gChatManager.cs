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
        message_Sprites = Resources.LoadAll<Sprite>("YooJung/ChatImgs");
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
            Sound_Manager.instance.Message_Sound();

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


            //Debug.Log("script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index : " + script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index);
            if (now_Script_Index.Equals(4))
            {
                ImageChat(false, "YooJung/ChatImgs/g_myprofile", "타인", null);
            }

            else if (now_Script_Index.Equals(11))
            {
                ImageChat(false, "YooJung/ChatImgs/g_eyes", "타인", null);
                Sound_Manager.instance.Message_Sound();
            }

            else if (now_Script_Index.Equals(29))
            {
                ImageChat(false, "YooJung/ChatImgs/g_body", "타인", null);
                Sound_Manager.instance.Message_Sound();
            }

            else if (now_Script_Index.Equals(40))
            {
                ImageChat(false, "YooJung/ChatImgs/g_face", "타인", null);
                Sound_Manager.instance.Message_Sound();
            }

            else if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 56 && isLoadSubScript1.Equals(false))
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;

                b2_1.GetComponent<Text>().text = Answers[0];
                b2_2.GetComponent<Text>().text = Answers[1];

                //개인정보 보낸다 안보낸다
                Btn2_1.onClick.AddListener(Answer1_1);
                Btn2_2.onClick.AddListener(Answer1_2);

            }

            else if (now_Script_Index.Equals(62))
            {
                ImageChat(false, "YooJung/ChatImgs/g_insta", "타인", null);
                Sound_Manager.instance.Message_Sound();
            }

            else if (script_Object.sheets[now_Sheet_Index].list[now_Script_Index].index == 97)
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;


                b2_1.GetComponent<Text>().text = Answers[2];
                b2_2.GetComponent<Text>().text = Answers[3];

                Btn2_1.onClick.RemoveListener(Answer1_1);
                Btn2_2.onClick.RemoveListener(Answer1_2);

                //사진 보낸다 안보낸다
                Btn2_1.onClick.AddListener(Answer2_1);
                Btn2_2.onClick.AddListener(Answer2_2);

            }

            else if (now_Script_Index.Equals(97))
            {
                ImageChat(false, "YooJung/ChatImgs/g_mybody", "타인", null);
                Sound_Manager.instance.Message_Sound();
            }

            else if (now_Script_Index.Equals(107))
            {
                isGameStart = false;
                Invoke("Game_Final_End", 2f);
            }

            if (now_Script_Index2.Equals(22) && isSubScript1.Equals(true))
            {
                answer2_Panel.SetActive(true);
                isGameStart = false;

                b2_1.GetComponent<Text>().text = Answers[4];
                b2_2.GetComponent<Text>().text = Answers[5];


                Btn2_1.onClick.RemoveListener(Answer1_1);
                Btn2_2.onClick.RemoveListener(Answer1_2);

                //설치한다 안한
                Btn2_1.onClick.AddListener(Answer1_1_1);
                Btn2_2.onClick.AddListener(Answer1_1_2);
            }

            else if (now_Script_Index2.Equals(23) && isSubScript1.Equals(true))
            {
                ImageChat(false, "YooJung/ChatImgs/g_mybody", "타인", null);
                Sound_Manager.instance.Message_Sound();
            }

            else if (now_Script_Index2.Equals(29) && isSubScript1.Equals(true))
            {
                isGameStart = false;
                Invoke("Game_Final_End", 2f);
            }
        }
    }

    public void ImageChat(bool isSend, string text, string user, Texture2D picture)
    {
        bool isBottom = scrollBar.value <= 0.00001f;     //  스크롤바의 현재 값. 0과 1 사이로 표현됩니다.

        // 다시 추가 시킨다 isSend에 따라서 노란색인지 하얀색인지 알기 위해서
        AreaScript area = Instantiate(isSend ? blueAreaImage : redAreaImage).GetComponent<AreaScript>();
        area.transform.SetParent(contentRect.transform, false);  // 뭉개지지 않게 잘 되게 하기 위해서 false
        area.boxRect.sizeDelta = new Vector2(600, area.boxRect.sizeDelta.y);        // width and height 가로 600 최대, y는 기본 크기로
        area.messageImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(text);

        Fit(area.boxRect);  // Text에 사이즈를 맞춰주기 위해서

        // 글자 크기만큼 노란 박스 크기를 설정해주기 위해서
        // 두줄 이상이면 크기를 줄여가면서, 한 줄이 아래로 내려가면 바로 전 크기를 대입
        float X = area.messageImage.sprite.rect.size.x;   // 기본보다 약간 크게 해준다 42정도를 더해서
        float Y = area.messageImage.sprite.rect.size.y;         // 높이는 기본 크기 그대로

        Debug.Log("Sprite size X : " + X);
        Debug.Log("Sprite size X : " + Y);
        /*
         if (Y > 49)        // 2줄 이상인 경우에는
         {
             for (int i = 0; i < 200; i++)  // 200번 정도 돌면서
             {
                 // 2씩 줄여가면서 Text 크기 까지 맞춰준다
                 area.boxRect.sizeDelta = new Vector2(X - i * 2, area.boxRect.sizeDelta.y);
                 Fit(area.boxRect);

                 // 딱 맞게 줄어들면~
                 if (Y != area.messageImage.sprite.rect.size.y)
                 {
                     area.boxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y);   // 기존에 뺏던 거에서 2만큼 더해서 사이즈 조절을 해주고~ 빠져나간다
                     break;
                 }

             }
         }
         else // 그렇지 않으면~ (1줄이면)
         {
             area.boxRect.sizeDelta = new Vector2(X, Y);     // 기존에 사이즈에 넣어준다
         }
         */
        area.boxRect.sizeDelta = new Vector2(X, Y);     // 기존에 사이즈에 넣어준다

        // 현재 것에 분까지 나오는 날짜와 유저이름 대입
        DateTime t = DateTime.Now;      // 현재 시간 갖고오기

        area.time = t.ToString("yyyy-MM-dd-HH-mm");     //
        area.user = user;   // 나, 타인

        // 현재 것은 항상 새로운 시간 대입
        int hour = t.Hour;      // 24시간으로 처리되므로..

        // 오전 오후 구분을 위해
        if (t.Hour == 0)
        {
            hour = 12;
        }
        else if (t.Hour > 12)
        {
            hour -= 12;
        }

        area.timeText.text = (t.Hour > 12 ? "오후" : "오전") + hour + ":" + t.Minute.ToString("D2");




        // 이전 것과 같으면 이전 시간, 꼬리 없에기  lastArea는 가장 나중에 보낸 것인데.. 그것이 비어있지 않고, 그것의 시간이 현재와 같고, 유저의 이름과 같으면
        bool isSame = lastArea != null && lastArea.time == area.time && lastArea.user == area.user;

        if (isSame)
        {
            lastArea.timeText.text = "";    // 이전것을 지운다
        }

        area.tail.SetActive(!isSame);


        // 타인이 이전 것과 같으면, 이미지, 이름 없에기
        /*if (!isSend)
        {
            area.userImage.gameObject.SetActive(!isSame);
            area.userText.gameObject.SetActive(!isSame);
            area.userText.text = area.user;     // 위에서 지웠기 때문에.. 아래에서는 지원해준다
            // 이미지를 추가하기 위해서
            if (picture != null)
            {
                area.userImage.sprite = Sprite.Create(picture, new Rect(0, 0, picture.width, picture.height), new Vector2(0.5f, 0.5f));
            }
        }*/


        // 이전 것과 날짜가 다르면 날짜영역 보이기
        // sub 0~10은  yyyy-mm-dd 까지이다 이전것과 새로운 것이 다르다면~!(날짜가 바뀌었다면)
        if (lastArea != null)
            Debug.Log(lastArea.time.Substring(0, 10));

        Debug.Log(area.time.Substring(0, 10));

        if (lastArea != null && lastArea.time.Substring(0, 10) != area.time.Substring(0, 10))
        {

            Transform curDateArea = Instantiate(dateArea).transform;
            curDateArea.SetParent(contentRect.transform, false);

            curDateArea.SetSiblingIndex(curDateArea.GetSiblingIndex() - 1);     // Index에서 1을 빼줘야지만, 날짜가 먼저 나오고 나서 Yellow인지 White인지가 나오게 된다.

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


        // Fie 맞추는 순서가 중요하다.
        // 바로 상위 상위 상위로 업데이트 해 주어야 한다.
        Fit(area.boxRect);
        Fit(area.areaRect);
        Fit(contentRect);
        lastArea = area;


        // 스크롤바가 위로 올라간 상태에서 메시지를 받으면 맨 아래로 내리지 않음
        if (!isSend && !isBottom)
            return;

        Invoke("ScrollDelay", 0.03f);   // 0.03초 이후에 해 준다
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

    public void Game_End()
    {
        Sound_Manager.instance.PlaySFX(1);
        TotalGameManager.instance.Set_Popup_Clicked(true);
        SceneManager.LoadScene("Look_Around_Scene");
    }

    public void Game_Final_End()
    {
        Sound_Manager.instance.PlaySFX(0);
        TotalGameManager.instance.Set_Is_Ended(true);
        SceneManager.LoadScene("Look_Around_Scene");
    }

    public void Answer1_1()
    {
        text = "2xxxxx-xxxxxxx 여기!";
        Chat(true, text, "나", null);
        Sound_Manager.instance.Message_Sound();

        answer2_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Answer1_2()
    {
        isSubScript1 = true;
        ScriptLoad2(now_Script_Index2);
        Sound_Manager.instance.Message_Sound();
        isLoadSubScript1 = true;

        answer2_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Answer2_1()
    {
        //최종엔딩
        ImageChat(true, "YooJung/ChatImgs/rc_mybody", "나", null);
        Sound_Manager.instance.Message_Sound();
        Invoke("Game_Final_End", 2f);

        answer2_Panel.SetActive(false);
        //isGameStart = true;
    }

    public void Answer2_2()
    {
        text = "안보낼거임 자라 그냥";
        Chat(true, text, "나", null);
        Sound_Manager.instance.Message_Sound();
        answer2_Panel.SetActive(false);        
        isGameStart = true;
    }

    public void Answer1_1_1()
    {
        text = "뭐야 아무것도 안뜨는데?";
        Chat(true, text, "나", null);
        Sound_Manager.instance.Message_Sound();

        answer2_Panel.SetActive(false);
        isGameStart = true;
    }

    public void Answer1_1_2()
    {
        //초기엔딩
        Invoke("Game_End", 2f);
    }
}
