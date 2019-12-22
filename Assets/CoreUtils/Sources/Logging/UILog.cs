using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CoreUtils.Logging
{
    public class UILog : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;
        [SerializeField]
        private int linesCount = 5;

        private const string NextLine = "\n\r";

        private readonly List<string> _log = new List<string>();

        private void Awake()
        {
            Application.logMessageReceived += ApplicationOnLogMessageReceived;
        }

        private void ApplicationOnLogMessageReceived(string condition, string stacktrace, LogType type)
        {
            _log.Add(condition);
            var min = Mathf.Max(_log.Count - linesCount, 0);
            var logText = string.Empty;
            for (var i = min; i < _log.Count; i++)
            {
                logText += _log[i] + NextLine;
            }

            text.text = logText;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= ApplicationOnLogMessageReceived;
        }
    }
}