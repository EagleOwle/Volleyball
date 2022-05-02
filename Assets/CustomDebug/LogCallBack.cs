using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogCallBack : MonoBehaviour
{
    public static LogCallBack Instance;

    [SerializeField] private RectTransform contentRectTransform;
    [SerializeField] private Text logText;

    private int stringNumber;
    private List<LogMessage> queue = new List<LogMessage>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Application.logMessageReceived += Application_logMessageReceived;
        stringNumber = 0;
    }

    private void Application_logMessageReceived(string logString, string stackTrace, LogType type)
    {
        if (type != LogType.Warning)
        {
            LogMessage logMessage = new LogMessage(logString, type);
            queue.Add(logMessage);
        }
    }

    void LateUpdate()
    {
        if (queue.Count > 0)
        {
            Color color = Color.white;

            foreach (LogMessage log in queue)
            {
                switch (log.logType)
                {
                    case LogType.Error:
                        color = Color.red;
                        break;
                    case LogType.Assert:
                        color = Color.yellow;
                        break;
                    case LogType.Warning:
                        color = Color.yellow;
                        break;
                    case LogType.Log:
                        color = Color.white;
                        break;
                    case LogType.Exception:
                        color = Color.red;
                        break;
                }

                Log(log.message, color);
            }

            queue.Clear();
        }
    }

    private void Log(string value, Color color)
    {
        string s = ColorString(value, color);

        //logText.text = logText.text + "\n";
        stringNumber++;
        logText.text = logText.text + "\n" + stringNumber + " " + s;
    }

    public static string ColorString(string text, Color color)
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
    }

    private class LogMessage
    {
        public LogMessage(string message, LogType logType)
        {
            this.message = message;
            this.logType = logType;
        }

        public string message;
        public LogType logType;
    }

}
