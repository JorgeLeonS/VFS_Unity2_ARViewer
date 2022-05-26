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

    public List<GameObject> furniturePieces;
    public List<GameObject> spawnedFurniturePieces;	
	public int currentPiece;

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
					if (spawnedFurniturePieces[currentPiece] == null)
					{
						spawnedFurniturePieces[currentPiece] = Instantiate(furniturePieces[currentPiece], hit.point, Quaternion.identity);
						//spawnedObject = Instantiate(objectToSpawn, ray.direction, Quaternion.identity);
					}
					else if (hit.collider.tag == "Plane")
					{
						spawnedFurniturePieces[currentPiece].transform.position = hit.point;
					}
				}
			}
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log(furniturePieces.Count);
		spawnedFurniturePieces = new List<GameObject>(furniturePieces.Count);
        for (int i = 0; i < spawnedFurniturePieces.Capacity; i++)
        {
			spawnedFurniturePieces.Add(null);
        }
    }
}
