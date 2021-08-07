using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaScript : MonoBehaviour
{
    public RectTransform areaRect, boxRect, textRect;
    public GameObject tail;
    public Text timeText, userText, dateText;
    public Image userImage;
    public string time, user; // Time은 보낸 시간, user은 누가 보냈는지 (타인 or 나)

}
