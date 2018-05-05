using UnityEngine;
using System.Collections;

public class Persist : MonoBehaviour {
	

	void Awake()
	{

		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
			return;//Avoid doing anything else
		}
		_instance = this;
		DontDestroyOnLoad(this);

	}


	private static Persist _instance = null;
	public static Persist instance
	{
		get
		{
			if (!_instance) {
				_instance = FindObjectOfType(typeof(Persist)) as Persist;
			}
			return _instance;

		}
	}


	void OnApplicationQuit ()
	{
		// release reference on exit
		_instance = null;
	}

}
