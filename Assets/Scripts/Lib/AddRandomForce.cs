using UnityEngine;
using System.Collections;

public class AddRandomForce : MonoBehaviour {

	Rigidbody m_Rigidbody;

	public float min,max;

	public GameObject explosionPrefab;
	public int numExplosions = 3;

	void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G))
		{
			for (int i = 0; i < numExplosions; i++)
			{
				GameObject.Instantiate (explosionPrefab, transform.position, transform.rotation);
			}
			m_Rigidbody.AddForce(new Vector3(UnityEngine.Random.Range(min, max),UnityEngine.Random.Range(1000, 100000),UnityEngine.Random.Range(1000, 100000)), ForceMode.Impulse);
			m_Rigidbody.AddTorque(new Vector3(UnityEngine.Random.Range(min, max),UnityEngine.Random.Range(1000, 100000),UnityEngine.Random.Range(1000, 100000)), ForceMode.Impulse);
		}
	}
}
