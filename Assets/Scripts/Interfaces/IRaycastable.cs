using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public interface IRaycastable
{
	RaycastHit2D hit { get; set; } 
	float hitDistance { get; set; }
}






public interface IAddableList
{
	void AddToList<T> (T obj);
	void RemoveFromList<T> (T obj);
}

public interface IDestroyable
{
	void DestroyObject (GameObject explosionPrefab);
}


