using UnityEngine;
using System.Collections;

public class ChangeTimeSpeed : MonoBehaviour {

	float min = 0.35f;
	float max = 99;
	public float t;

	void Update()
	{
		t = Time.timeScale;
		if (Input.GetKey(KeyCode.Minus))
			t -= 0.3f;
		else if (Input.GetKey(KeyCode.Equals))
			t += 0.3f;

		Time.timeScale = Mathf.Clamp(t, min, max);

		if (Input.GetKeyDown(KeyCode.P))
			Time.timeScale = 1;


	}

	public void SpeedUp()
	{
		t = Time.timeScale;
		t += 0.3f;
		Time.timeScale = Mathf.Clamp(t, min, max);
	}

	public void SpeedDown()
	{
		t = Time.timeScale;
		t -= 0.3f;
		Time.timeScale = Mathf.Clamp(t, min, max);
	}

	public void Reset()
	{
		Time.timeScale = 1;
	}
}
