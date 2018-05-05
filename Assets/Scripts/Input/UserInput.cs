using System.Collections;
using UnityEngine;




public class KeyboardInput : IUserInputProxy
{
	
	public InputModel GetInput ()
	{
		InputModel i = new InputModel ((Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) ? 1 : 0, (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) ? 1 : 0);
		return i;
	}

	

}

public class TouchInput : IUserInputProxy
{
	
	public InputModel GetInput ()
	{
		InputModel input = new InputModel ();
		Touch[] myTouches = Input.touches;
		for (int i = 0; i < Input.touchCount; i++)
		{
			if (myTouches [i].position.x < Screen.width / 2)
				input.leftInput = 1;
			else if (myTouches[i].position.x >= Screen.width / 2)
			{
				input.rightInput = 1;
			}
		}
		return input;

	}

	
}
