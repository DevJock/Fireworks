using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public string heroName;
    public string heroMessage;
    public string currentTime;
    public string nextYearTime;
    public string timeDiff;
    public GameObject button;
    public GameObject celebrate;
    public GameObject countDown;
    public bool showCelebrate;
    TextMesh message;

    private void Start()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
         WebGLInput.captureAllKeyboardInput = false;
        #endif
        showCelebrate = false;
        message = countDown.transform.Find("message").GetComponent<TextMesh>();
        randomizeBackground();
        if (!countDown.activeSelf)
        {
            countDown.SetActive(true);
        }
        if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer)
        {
            button.SetActive(true);
            button.GetComponentInChildren<Text>().text = "Show Message";
        }
        else
        {
            button.SetActive(false);
        }
        Application.ExternalEval("getName()");
    }


    void randomizeBackground()
    {
        Camera.main.backgroundColor = new Color(UnityEngine.Random.Range(0f, 200.0f) / 255.0f, UnityEngine.Random.Range(0f, 200.0f) / 255.0f, UnityEngine.Random.Range(0f, 200.0f) / 255.0f);
    }


    public void Customize(string name)
    {
        if (name.Length < 2)
        {
            heroName = "";
        }
        else
        {
            heroName = name.Trim();
        }
        checkHero();
    }


    void Update()
    {
        TimeSpan duration = (new DateTime(DateTime.Now.Year + 1, 01, 01, 00, 00, 00) - DateTime.Now);
        timeDiff = string.Format("{0:}Day(s) {1:}Hour(s) {2:} Minute(s) {3:}Second(s)", (int)duration.TotalDays, (int)duration.Hours, (int)duration.Minutes, (int)duration.Seconds);

        if (!Application.isEditor)
        {
            if (DateTime.Now.Month == 1 && DateTime.Now.Day == 1)
            {
                showCelebrate = true;
            }
            else
            {
                showCelebrate = false;
            }
            if (showCelebrate && !celebrate.activeSelf)
            {
                ACTION_SHOW_CELEBRATION();

            }
            else if (!showCelebrate && celebrate.activeSelf)
            {
                ACTION_SHOW_COUNTDOWN();
            }

        }

        currentTime = string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);
        message.text = string.Format("{0:} Day(s) {1:} Hour(s) {2:} Minute(s) {3:} Second(s)", (int)duration.TotalDays, (int)duration.Hours, (int)duration.Minutes, (int)duration.Seconds);
        if (nextYearTime == "" || (DateTime.Now.Month == 1 && DateTime.Now.Day == 1 && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0 && DateTime.Now.Second == 0))
        {
            int year = DateTime.Today.Year + 1;
            nextYearTime = string.Format("{0:}/01/01 00:00:00", year);
            countDown.transform.Find("year").GetComponent<TextMesh>().text = year.ToString();
            countDown.transform.Find("year").GetComponent<TextMesh>().color = new Color(UnityEngine.Random.Range(100.0f, 255.0f) / 255.0f, UnityEngine.Random.Range(100.0f, 255.0f) / 255.0f, UnityEngine.Random.Range(100.0f, 255.0f) / 255.0f);
        }

    }


    public void ACTION_TOGGLE()
    {
        if (celebrate.activeSelf)
        {
            ACTION_SHOW_COUNTDOWN();
        }
        else
        {
            ACTION_SHOW_CELEBRATION();
        }
    }

    void checkHero()
    {
        if (heroName.Equals(" ") || heroName.Equals("") || heroName.Length < 2)
        {
            countDown.transform.Find("name").GetComponent<TextMesh>().text = "";
            countDown.transform.Find("msg1").GetComponent<TextMesh>().text = "";
            countDown.transform.Find("msg2").GetComponent<TextMesh>().text = "";
            celebrate.transform.Find("name").GetComponent<TextMesh>().text = "Chiraag Bangera";
        }
        else
        {
            celebrate.transform.Find("name").GetComponent<TextMesh>().text = heroName;
            countDown.transform.Find("name").GetComponent<TextMesh>().text = heroName;
            countDown.transform.Find("msg1").GetComponent<TextMesh>().text = "Invited You To";
            countDown.transform.Find("msg2").GetComponent<TextMesh>().text = "With Them";
        }
    }


    public void ACTION_SHOW_COUNTDOWN()
    {
        checkHero();
        countDown.SetActive(true);
        celebrate.SetActive(false);
        randomizeBackground();
        button.GetComponentInChildren<Text>().text = "Show Message";
    }

    public void ACTION_SHOW_CELEBRATION()
    {
        checkHero();
        celebrate.transform.GetChild(0).Find("year").GetComponent<TextMesh>().text = DateTime.Now.Year.ToString();
        countDown.SetActive(false);
        celebrate.SetActive(true);
        Camera.main.backgroundColor = Color.black;
        button.GetComponentInChildren<Text>().text = "Show CountDown";
    }

}
