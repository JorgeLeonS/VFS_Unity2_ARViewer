using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
		}
#endif
		// This *will* work on mobile, but gives you the *average* touch, which
		// may not be desirable (and will not allow for multi-touch).

#if UNITY_STANDALONE || UNITY_EDITOR

		if (Input.GetMouseButton(0))
		{
			pressPosition = Input.mousePosition;
			isPressing = true;
		}
#endif

		if (isPressing)
		{
			Ray ray = Camera.main.ScreenPointToRay(pressPosition);


			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				// Check if the mouse was clicked over a UI element

				if (!EventSystem.current.IsPointerOverGameObject())
				{
					if (spawnedObject == null)
					{
						spawnedObject = Instantiate(objectToSpawn, hit.point, Quaternion.identity);
						//spawnedObject = Instantiate(objectToSpawn, ray.direction, Quaternion.identity);
					}
					else if (hit.collider.tag == "Plane")
					{
						spawnedObject.transform.position = hit.point;
					}
				}
			}
		}
	}

	// Start is called before the first frame update
	void Start()
	{

	}
}
