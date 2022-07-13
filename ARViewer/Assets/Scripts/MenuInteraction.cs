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

    public GameObject OpenEyeIcon;
    public GameObject ClosedEyeIcon;

    public GameObject OpenDeleteIcon;
    public GameObject CloseDeleteIcon;
    public GameObject HiddenDeleteIcon;

    public GameObject ChairButtonOutine;
    public GameObject SingleBedButtonOutine;
    public GameObject QueenBedButtonOutine;
    public GameObject ShelfAButtonOutine;
    public GameObject ShelfBButtonOutine;
    public GameObject ShelfCButtonOutine;
    public GameObject TableAButtonOutine;
    public GameObject TableBButtonOutine;
    public GameObject TableCButtonOutine;

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
            //Apear the open eye and the unselectable trash can
            OpenEyeIcon.gameObject.SetActive(true);
            ClosedEyeIcon.gameObject.SetActive(false);

            OpenDeleteIcon.gameObject.SetActive(false);
            HiddenDeleteIcon.gameObject.SetActive(false);
            CloseDeleteIcon.gameObject.SetActive(true);


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
            OpenEyeIcon.gameObject.SetActive(false);
            ClosedEyeIcon.gameObject.SetActive(true);

            HiddenDeleteIcon.gameObject.SetActive(true);
            CloseDeleteIcon.gameObject.SetActive(false);
            OpenDeleteIcon.gameObject.SetActive(false);


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
            OpenDeleteIcon.SetActive(false);
            CloseDeleteIcon.SetActive(true);

            DeleteButton.GetComponentInChildren<Text>().text = "Delete Off";
            FindObjectOfType<GameMaster>().isDeleteModeOn = false;
            isDeleteModeOn = false;
        }
        else
        {
            OpenDeleteIcon.SetActive(true);
            CloseDeleteIcon.SetActive(false);

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

    public void GetChair()
    {
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        HideAllButtonOutline();
        ChairButtonOutine.SetActive(true);
        FindObjectOfType<GameMaster>().AssignCurrentPiece(0);
    }

    public void GetBedS()
    {
        Debug.Log("Cube");
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        HideAllButtonOutline();
        SingleBedButtonOutine.SetActive(true);
        FindObjectOfType<GameMaster>().AssignCurrentPiece(1);
    }
    public void GetBedQ()
    {
        Debug.Log("Cone");
        HideAllButtonOutline();
        QueenBedButtonOutine.SetActive(true);
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        FindObjectOfType<GameMaster>().AssignCurrentPiece(2);
    }

    public void GetShelfA()
    {
        HideAllButtonOutline();
        ShelfAButtonOutine.SetActive(true);
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        FindObjectOfType<GameMaster>().AssignCurrentPiece(3);
    }

    public void GetShelfB()
    {
        HideAllButtonOutline();
        ShelfBButtonOutine.SetActive(true);
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        FindObjectOfType<GameMaster>().AssignCurrentPiece(4);
    }

    public void GetShelfC()
    {
        HideAllButtonOutline();
        ShelfCButtonOutine.SetActive(true);
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        FindObjectOfType<GameMaster>().AssignCurrentPiece(5);
    }

    public void GetTableA()
    {
        HideAllButtonOutline();
        TableAButtonOutine.SetActive(true);
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        FindObjectOfType<GameMaster>().AssignCurrentPiece(6);
    }

    public void GetTableB()
    {
        HideAllButtonOutline();
        TableBButtonOutine.SetActive(true);
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        FindObjectOfType<GameMaster>().AssignCurrentPiece(7);
    }

    public void GetTableC()
    {
        HideAllButtonOutline();
        TableCButtonOutine.SetActive(true);
        FindObjectOfType<GameMaster>().hasAPieceBeenSpawned = false;
        FindObjectOfType<GameMaster>().AssignCurrentPiece(8);
    }

    public void HideAllButtonOutline()
    {
        ChairButtonOutine.SetActive(false);
        SingleBedButtonOutine.SetActive(false);
        QueenBedButtonOutine.SetActive(false);
        ShelfAButtonOutine.SetActive(false);
        ShelfBButtonOutine.SetActive(false);
        ShelfCButtonOutine.SetActive(false);
        TableAButtonOutine.SetActive(false);
        TableBButtonOutine.SetActive(false);
        TableCButtonOutine.SetActive(false);
    }
}

    #endregion
