using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenshotComponent : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}

	void Update ()
	{

		if(Input.GetKeyDown(KeyCode.S)) {
			string path = Application.dataPath + "/screens/";

			if(!Directory.Exists(path)) {
				Directory.CreateDirectory(path);
			}

			int count = Directory.GetFiles (path, "*", SearchOption.TopDirectoryOnly).Length;

			ScreenCapture.CaptureScreenshot(path + "/" + count + ".png");
		}
	}
}

