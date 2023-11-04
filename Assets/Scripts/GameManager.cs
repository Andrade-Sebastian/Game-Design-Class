using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //Different game states
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    //stores the current state of the game
    public GameState currentState;
    public GameState previousState;


    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;

    [Header("Current Stat Displays")]
    //current stat displays
    public Text currentHealthDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;

    [Header("Results Screen Display")]
    public Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReachedDisplay;
    public List<Image> chosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveItemsUI = new List<Image>(6);

    public bool isGameOver = false;


    void Awake()
    {
        //Warning to check if there is a duplicate singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA " + this + "DELETED");
        }
        DisableScreens();
    }


    void Update()
    {
        //Behavior for each gamestate
        switch (currentState)
        {
            case GameState.Gameplay:
                //Gameplay state
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                //Paused state code
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                //GameOver state code
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f; //Stop the game
                    Debug.Log("Game over!");
                    DisplayResults();
                }
                break;

            default:
                Debug.Log("Current state does not exist");
                break;
        }
    }


    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        { 
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; //Stop the gameplay
            pauseScreen.SetActive(true);
            Debug.Log("Game is paused..");
        }

    }


    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f; // Resume game
            pauseScreen.SetActive(false);
            Debug.Log("Game resumed..");
        }
    }


    public void ChangeState(GameState newState)
    {
        currentState = newState;

    }


    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
    }


    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.name;
    }


    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenWeaponsAndPassiveUI (List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData)
    {
        if (chosenWeaponsData.Count != chosenWeaponsUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.Log("Chosen Weapons & Passive items data lists have different lengths");
            return;
        }

        //assign chosen weapons data
        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            //check the sprite of the corresponding element
            if (chosenWeaponsData[i].sprite)
            {
                //enable the corresponding element
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {
                //disable the corresponding element
                chosenWeaponsUI[i].enabled = false;
            }
        }

        //assign chosen passive items data
        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            //check the sprite of the corresponding element
            if (chosenPassiveItemsData[i].sprite)
            {
                //enable the corresponding element
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {
                //disable the corresponding element
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
    }
}