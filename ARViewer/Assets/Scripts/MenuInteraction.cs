using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    public GameObject SelectionPanel;

    public Button ShowHideButton;
    bool isMenuEnabled = true;

    public Button DeleteButton;
    [HideInInspector]
    public bool isDeleteModeOn;

    public Button NewObjectMenuButton;
    public Button InSceneMenuButton;

    public GameObject NewObjectScrollSection;
    public GameObject InSceneScrollSection;

    public Button RotateRightButton;
    public Button RotateLeftButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHideMenu()
    {
        //isMenuEnabled ? ScrollView.gameObject.SetActive(isMenkuEnabled);
        if (isMenuEnabled)
        {
            SelectionPanel.gameObject.SetActive(false);
            ShowHideButton.GetComponentInChildren<Text>().text = "Show menu";
            FindObjectOfType<GameMaster>().isSpawnModeOn = false;
            isMenuEnabled = false;

            DeleteButton.interactable = true;

            RotateRightButton.interactable = true;
            RotateLeftButton.interactable = true;
        }
        else
        {
            SelectionPanel.gameObject.SetActive(true);
            ShowHideButton.GetComponentInChildren<Text>().text = "Hide menu";
            FindObjectOfType<GameMaster>().isSpawnModeOn = true;
            isMenuEnabled = true;

            DeleteButton.GetComponentInChildren<Text>().text = "Delete Off";
            DeleteButton.interactable = false;
            FindObjectOfType<GameMaster>().isDeleteModeOn = false;
            isDeleteModeOn = false;

            RotateRightButton.interactable = false;
            RotateLeftButton.interactable = false;
        }
    }

    public void TriggerDeleteMode()
    {
        if (isDeleteModeOn)
        {
            DeleteButton.GetComponentInChildren<Text>().text = "Delete Off";
            FindObjectOfType<GameMaster>().isDeleteModeOn = false;
            isDeleteModeOn = false;
        }
        else
        {
            DeleteButton.GetComponentInChildren<Text>().text = "Delete On";
            FindObjectOfType<GameMaster>().isDeleteModeOn = true;
            isDeleteModeOn = true;
        }
    }

    public void NewObjectMenuSelection()
    {
        NewObjectScrollSection.SetActive(true);
        InSceneScrollSection.SetActive(false);
        NewObjectMenuButton.interactable = false;
        InSceneMenuButton.interactable = true;
    }

    public void InSceneMenuSelection()
    {
        NewObjectScrollSection.SetActive(false);
        InSceneScrollSection.SetActive(true);
        NewObjectMenuButton.interactable = true;
        InSceneMenuButton.interactable = false;
    }

    #region Buttons functions for furnitture pieces
    public void GetCube()
    {
        Debug.Log("Cube");
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        FindObjectOfType<GameMaster>().AssignCurrentPiece(1);
    }
    public void GetCone()
    {
        Debug.Log("Cone");
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        FindObjectOfType<GameMaster>().AssignCurrentPiece(0);
    }
    #endregion
}
