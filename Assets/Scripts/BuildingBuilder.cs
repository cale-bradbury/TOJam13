using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilder : MonoBehaviour
{

	public Transform player;
	public List<GameObject> prefabs;
	public float spawnAheadDistance = 300;
	public float spawnRange = 100;
	public float spawnEverySeconds = .5f;

	public float chunkSize = 50;
	public int numChunks = 6;

	float t = 0;
	List<GameObject> buildings;

	void Awake ()
	{
		GameManager.OnStateChange += HandleOnGameStateChange;
		buildings = new List<GameObject> ();
	}

	void OnDestroy ()
	{
		GameManager.OnStateChange -= HandleOnGameStateChange;
	}

	void HandleOnGameStateChange (GameState state)
	{
		switch (state) {
		case GameState.Intro:
			enabled = false;
			break;
		case GameState.StartGame:
			enabled = true;
			break;
		case GameState.Collision:
			enabled = false;
			break;
		case GameState.End:
			break;
		}
	
	}


	// Use this for initialization
	void OnEnable ()
	{
		if (buildings.Count == 0) {
			
			buildings = new List<GameObject> ();
			for (int i = 0; i < numChunks; i++) {
				GameObject g = Instantiate<GameObject> (prefabs [Random.Range (0, prefabs.Count)], new Vector3 (0, 0, chunkSize * buildings.Count), Quaternion.identity);
				buildings.Add (g);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (buildings.Count > 0) {
			
			if (buildings [0].transform.position.z < (player.position.z - 400)) {
				Destroy (buildings [0]);
				buildings.RemoveAt (0);
				GameObject g = Instantiate<GameObject> (prefabs [Random.Range (0, prefabs.Count)], new Vector3 (0, 0, chunkSize * buildings.Count), Quaternion.identity);
				buildings.Add (g);
			}
		}
	}

	//	void Spawn ()
	//	{
	//		GameObject g = Instantiate<GameObject> (prefabs [Random.Range (0, prefabs.Count)]);
	//		buildings.Add (g);
	//		g.transform.position = player.position;
	//		g.transform.position.Scale (new Vector3 (1, 0, 1));
	//		g.transform.position += new Vector3 (Random.Range (-spawnRange, spawnRange), 0, spawnAheadDistance);
	//	}

	void EndGame ()
	{
		for (int i = 0; i < buildings.Count; i++) {
			GameObject.Destroy (buildings [i]);
			buildings.RemoveAt (i);
		}
		buildings.Clear ();
		buildings = new List<GameObject> ();
	}
}
