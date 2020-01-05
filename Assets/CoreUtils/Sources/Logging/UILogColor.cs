using System;
using UnityEngine;

namespace CoreUtils.Logging
{
    [Serializable]
    public class UILogColor
    {
        [SerializeField] private string constant;
        [SerializeField] private Color color;

        public UILogColor()
        {
        }

        public UILogColor(string constant, Color color)
        {
            this.constant = constant;
            this.color = color;
        }

        public string Replace(string source)
        {
            var origin = $"<color={constant}>";
            var replace = $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>";
            return source.Replace(origin, replace);
        }
    }
}