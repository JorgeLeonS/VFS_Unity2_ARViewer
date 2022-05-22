using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public Transform target;
	public GameObject objectToSpawn;

	private GameObject spawnedObject;
	private Vector2 touchPos;


	// Update is called once per frame
	private void Update()
	{
		bool isPressing = false;

		Vector3 pressPosition = Vector3.zero;

#if UNITY_ANDROID || UNITY_IOS

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			pressPosition = touch.position;
			isPressing = true;

			//Instantiate(objectToSpawn, pressPosition, Quaternion.identity);

			Debug.Log("Press pos:"+pressPosition);
		}
#endif
		// This *will* work on mobile, but gives you the *average* touch, which
		// may not be desirable (and will not allow for multi-touch).

#if UNITY_STANDALONE || UNITY_EDITOR

		if (Input.GetMouseButton(0))
		{
			pressPosition = Input.mousePosition;
			isPressing = true;


			Debug.Log("Press pos:" + pressPosition);

		}
#endif

		if (isPressing)
		{
			Ray ray = Camera.main.ScreenPointToRay(pressPosition);
			Debug.Log("Ray:" + ray);

			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if (spawnedObject == null)
				{
					spawnedObject = Instantiate(objectToSpawn, ray.direction, Quaternion.identity);
				}
				else
				{
					spawnedObject.transform.position = ray.direction;
				}
				//target.position = hit.point;
			}
		}
	}

	// Start is called before the first frame update
	void Start()
	{

	}
}
