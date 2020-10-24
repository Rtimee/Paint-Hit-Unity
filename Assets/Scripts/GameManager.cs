using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Veriables

    public static GameManager Instance;
    public PlayerState.States currentState;

    int counter;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
        counter = -1;
    }

    #endregion

    #region Private Methods

    public void StartGame()
    {
        currentState = PlayerState.States.gameStarted;
        UIManager.Instance.StartGame();
        LevelManager.Instance.StartTheFirstLevel();
        UpdateTheRemainText();
    }

    public void UpdateTheRemainText()
    {
        counter++;
        UIManager.Instance.remainText.text = counter + " / " + LevelManager.Instance.currentLevel.torusCount;
    }

    public void ResetCounter()
    {
        counter = -1;
        UpdateTheRemainText();
    }

    #endregion
}
