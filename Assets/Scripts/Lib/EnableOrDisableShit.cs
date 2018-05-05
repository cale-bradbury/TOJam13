using UnityEngine;

/// <summary>
/// This Class is to put shit in that you regularly disable while working in editor
/// and then forget to enable before running game because you're a silly silly turkey
/// if you want to spooper idiot proof yerself, add this to script execution order
/// </summary>
public class EnableOrDisableShit : MonoBehaviour {

	public GameObject[] shitToEnable;
	public GameObject[] shitToDisable;

	void Awake()
	{
		if (shitToEnable.Length > 0)
		{
			foreach (GameObject shit in shitToEnable)
			{
				if (shit != null)
				{

					if (!shit.activeInHierarchy)
						shit.SetActive(true);
				}
			}
		}
		if (shitToDisable.Length > 0)
		{
			foreach (GameObject shit in shitToDisable)
			{
				if (shit != null)
				{
					if (shit.activeInHierarchy)
						shit.SetActive(false);
				}
			}
		}
	}

}
