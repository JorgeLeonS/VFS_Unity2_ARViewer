using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    public GameObject ScrollView;
    bool isMenuEnabled;
    public Button ShowHideButton;
    // Start is called before the first frame update
    void Start()
    {
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

    public void ShowHideMenu()
    {
        //isMenuEnabled ? ScrollView.gameObject.SetActive(isMenkuEnabled);

        if (isMenuEnabled)
        {
            ScrollView.gameObject.SetActive(false);
            ShowHideButton.GetComponentInChildren<Text>().text = "Show menu";
            isMenuEnabled = false;
        }
        else
        {
            ScrollView.gameObject.SetActive(true);
            ShowHideButton.GetComponentInChildren<Text>().text = "Hide menu";
            isMenuEnabled = true;
        }
    }
}
