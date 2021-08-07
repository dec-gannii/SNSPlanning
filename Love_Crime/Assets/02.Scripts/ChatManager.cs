using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // 현재 시간을 알기 위해서 사용
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject yellowArea, whiteArea, dateArea;
    public RectTransform contentRect;
    public Scrollbar scrollBar;
    AreaScript lastArea;

    public MyScript script_Object;
    public int now_Script_Index;
    public int now_Sheet_Index;
    private bool isClicked;

    private void Start()
    {
        isClicked = false;
        //Chat(false, "blah", "bad", Resources.Load<Texture2D>("ETC/Suspect"));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            isClicked = true;
            ScriptLoad(now_Script_Index);
        }
    }

    public void ScriptLoad(int index)
    {
        if (script_Object.sheets[now_Sheet_Index].list.Count <= now_Script_Index)
        {
            return;
        }

        Chat(script_Object.sheets[now_Sheet_Index].list[now_Script_Index].MyTurn,
            script_Object.sheets[now_Sheet_Index].list[now_Script_Index].Script,
            script_Object.sheets[now_Sheet_Index].list[now_Script_Index].Who,
            null);

        now_Sheet_Index++;

        if (now_Sheet_Index > 1)
        {
            now_Sheet_Index = 0;
            now_Script_Index++;
        }

        isClicked = false;

    }

    // 모든 채팅의 판단을 하는 함수
    public void Chat (bool isSend, string text, string user, Texture2D picture)
    {
        if (text.Trim() == "") // space나 Enter를 걸러줌, 즉, 공백일 때는 아래 부분이 실행되지 않게 함
            return;
        bool isBottom = scrollBar.value <= 0.00001f; // 스크롤 바의 현재 값, 0과 1 사이로 표현됨
                                                     //print(text);

        AreaScript area = Instantiate(isSend ? yellowArea : whiteArea).GetComponent<AreaScript>();
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
        } else
        {
            area.boxRect.sizeDelta = new Vector2(X, Y);
        }

        DateTime t = DateTime.Now;

        area.time = t.ToString("yyyy-MM-dd-HH-mm");
        area.user = user;

        int hour = t.Hour;

        if (t.Hour == 0)
        {
            hour = 12;
        } else if (t.Hour > 12)
        {
            hour -= 12;
        }
        area.timeText.text = (t.Hour > 12 ? "오후 " : "오전 ") + hour + ":" + t.Minute.ToString("D2");

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
}
