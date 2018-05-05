using UnityEngine;
using System.Collections;


public interface IUserInputProxy
{
	InputModel GetInput ();
}

public interface ITakesInput
{
	void SendInput (float leftInput, float rightInput);
}

public interface IRequireUserInput
{
	IUserInputProxy InputProxy { get; set; }
}

public class InputModel
{
	public float leftInput = 0;
	public float rightInput = 0;

	public InputModel()
	{
		leftInput = 0;
		rightInput = 0;
	}

	public InputModel(float left, float right)
	{
		this.leftInput = left;
		this.rightInput = right;
	}

	public void Reset()
	{
		leftInput = 0;
		rightInput = 0;
	}
}