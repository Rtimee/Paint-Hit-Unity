using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Veriables

    public static LevelManager Instance;

    [SerializeField] List<Level> levels;

    public Level currentLevel;

    int index;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Private Methods

    public void NextLevel()
    {
        if (index < levels.Count - 1)
        {
            currentLevel.gameObject.SetActive(false);
            index++;
            currentLevel = levels[index];
            currentLevel.gameObject.SetActive(true);
            currentLevel.LoadLevel();
            UIManager.Instance.NextLevel();
            GameManager.Instance.ResetCounter();
        }
        else
            Debug.Log("Game Over! ");
    }
    
    public void StartTheFirstLevel()
    {
        index = 0;
        currentLevel = levels[index];
        currentLevel.gameObject.SetActive(true);
        currentLevel.LoadLevel();
    }

    public void RestartLevel()
    {
        currentLevel.LoadLevel();
        UIManager.Instance.RestartLevel();
        GameManager.Instance.ResetCounter();
    }

    #endregion
}
