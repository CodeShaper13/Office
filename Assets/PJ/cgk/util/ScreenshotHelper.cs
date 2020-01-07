using System;
using System.IO;
using UnityEngine;

public static class ScreenshotHelper {

    private static string screenshotDirectoryName = "screenshots/";

    /// <summary>
    /// Takes a screenshot.  When no path is passes, the default screenshot directory is used with the current time as the name.
    /// </summary>
    public static void captureScreenshot(string screenshotPath = null) {
        if(screenshotPath == null) {
            string time = DateTime.Now.ToString();
            time = time.Replace('/', '-').Replace(' ', '_').Replace(':', '.').Substring(0, time.Length - 3);

            screenshotPath = ScreenshotHelper.screenshotDirectoryName + time + ".png";
        }

        if(!Directory.Exists(ScreenshotHelper.screenshotDirectoryName)) {
            Directory.CreateDirectory(ScreenshotHelper.screenshotDirectoryName);
        }

        ScreenCapture.CaptureScreenshot(screenshotPath);
    }
}