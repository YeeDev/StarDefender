using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Yee.Dialogue
{
    public class DialoguePrinter : MonoBehaviour
    {
        [SerializeField] [Range(0f, 10f)] float printSpeed = 0.1f;

        bool skip;
        bool inEndCode;
        string endCode = "";

        public void ClearDialogue(Text text) { text.text = ""; }

        public IEnumerator PrintDialogue(string textToPrint, Text text)
        {
            string s = "";
            endCode = "";
            inEndCode = false;

            for (int i = 0; i < textToPrint.Length; i++)
            {
                char c = textToPrint[i];
                s += c;

                if (skip)
                {
                    if (c == '>')
                    {
                        if (!inEndCode && endCode == "")
                        {
                            SearchForEndCode(textToPrint.Substring(i));
                        }
                        skip = false;
                    }

                    continue;
                }

                if (c == '<')
                {
                    skip = true;

                    if (textToPrint[i + 1] == '/')
                    {
                        endCode = "";
                        inEndCode = true;
                        continue;
                    }

                    inEndCode = false;
                    continue;
                }

                text.text = s + endCode;

                yield return new WaitForSecondsRealtime(printSpeed);
            }
        }

        private void SearchForEndCode(string text)
        {
            int startIndex = text.IndexOf('/') - 1;

            if (startIndex < 0) { return; }

            string checkSubstring = text.Substring(startIndex);
            int endIndex = checkSubstring.IndexOf('>');

            endCode += checkSubstring.Substring(0, endIndex + 1);

            if (checkSubstring.Length >= (endIndex + 2))
            {
                if (checkSubstring[endIndex + 2] != '/') { return; }

                SearchForEndCode(checkSubstring.Substring(endIndex + 1));
            }
        }
    }
}