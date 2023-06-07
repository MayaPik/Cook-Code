using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class TakeScreenshots : MonoBehaviour
{
    public class TakeScreenshotInEditor : ScriptableObject
    {
        public static string filePath = "Screenshots";
        public static int startNumber = 1;

        private static string sessionGuid;

        //#if DEBUG
        //        [MenuItem("Custom/Take Screenshot of Game View %^s")]
        //#endif
        static void TakeScreenshot()
        {
            if (string.IsNullOrWhiteSpace(sessionGuid))
            {
                sessionGuid = Guid.NewGuid().ToString();
            }

            int number = startNumber;
            string name = "" + number;
            var fullPath = $"{Environment.CurrentDirectory}/{filePath}/{Screen.width} x {Screen.height}/";

            while (true)
            {
                var fileName = $"{fullPath}{number}.png";

                if (System.IO.File.Exists(fileName))
                {
                    number++;
                    continue;
                }
                else
                {
                    System.IO.Directory.CreateDirectory(fullPath);
                    startNumber = number + 1;
                    ScreenCapture.CaptureScreenshot(fileName);
                    break;
                }
            }


        }
    }
}
