using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteraction : MonoBehaviour
{
    public List<GameObject> furniture;
    // Start is called before the first frame update
    void Start()
    {
        furniture = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCube()
    {
        Debug.Log("Cube");
        FindObjectOfType<GameMaster>().currentPiece = 1;
    }
    public void GetCone()
    {
        Debug.Log("Cone");
        FindObjectOfType<GameMaster>().currentPiece = 0;
    }
}