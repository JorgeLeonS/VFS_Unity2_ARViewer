using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMaster : MonoBehaviour
{
	[HideInInspector]
	public GameObject objectToSpawn;

	[HideInInspector]
	public int currentPieceNumber;
	[HideInInspector]
	public GameObject currentObject;
	//private Vector2 touchPos;

    public List<GameObject> furniturePieces;
    public List<GameObject> spawnedFurniturePieces;	

	[HideInInspector]
	public bool isDeleteModeOn;
	[HideInInspector]
	public bool isSpawnModeOn;
	[HideInInspector]
	public bool hasAPieceBeenSpawned = false;

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
				if (!IsPointerOverUIObject())
				{
                    if (isDeleteModeOn && hit.collider.tag == "InteractablePiece")
                    {
						Destroy(hit.collider.gameObject);
                    }
                    else if(!isDeleteModeOn)
                    {
                        if (isSpawnModeOn && hit.collider.tag == "Plane" && !hasAPieceBeenSpawned)
                        {
							//spawnedFurniturePieces[currentPieceNumber] = Instantiate(furniturePieces[currentPieceNumber], hit.point, Quaternion.identity);
							Instantiate(furniturePieces[currentPieceNumber], hit.point, Quaternion.identity);
							hasAPieceBeenSpawned = true;
						}else if(hit.collider.tag == "InteractablePiece")
                        {
							currentObject = hit.collider.transform.gameObject;
							//Debug.Log(currentObject);
							//Debug.Log(currentObject.GetComponent(typeof (Outline)));
							//currentObject..GetComponent<Outline>().enabled = true;
							//hit.collider.transform.position = hit.point;
                        }else if(!isSpawnModeOn && hit.collider.tag == "Plane")
                        {
							currentObject.transform.position = hit.point;
                        }
					}
				}
			}
		}
	}

	public void AssignCurrentPiece(int pieceNumber)
    {
		currentPieceNumber = pieceNumber;
		objectToSpawn = furniturePieces[pieceNumber];
	}

	// Mehtod to avoid conflict when clicking on UI on mobile
	// From: https://answers.unity.com/questions/1115464/ispointerovergameobject-not-working-with-touch-inp.html
	private bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	// Start is called before the first frame update
	void Start()
	{
		isSpawnModeOn = true;
		Debug.Log(furniturePieces.Count);
		spawnedFurniturePieces = new List<GameObject>(furniturePieces.Count);
        for (int i = 0; i < spawnedFurniturePieces.Capacity; i++)
        {
			spawnedFurniturePieces.Add(null);
        }
    }
}
