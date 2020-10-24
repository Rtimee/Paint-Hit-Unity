using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Veriables

    public static UIManager Instance;

    public GameObject infoPanel;
    public GameObject gamePanel;
    public GameObject nextLevelPanel;
    public GameObject youLostPanel;
    public Text remainText;

    [SerializeField] Canvas canvas;
    [SerializeField] Text levelText;
    [SerializeField] Image progressBar;
    [SerializeField] GameObject nextLevelButton;
    [SerializeField] Sprite[] backgrounds;
    [SerializeField] Image background;
    [SerializeField] GameObject winFx;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Private Methods

    public void StartGame()
    {
        infoPanel.SetActive(false);
        gamePanel.SetActive(true);
        background.sprite = GetBackground();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
    }

    public IEnumerator ShowTheNextLevelScreen()
    {
        winFx.SetActive(true);
        yield return new WaitForSeconds(2f);
        winFx.SetActive(false);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        gamePanel.SetActive(false);
        nextLevelPanel.SetActive(true);
        FillTheBar();
    }

    public void NextLevel()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        progressBar.fillAmount = 0;
        nextLevelPanel.SetActive(false);
        gamePanel.SetActive(true);
        background.sprite = GetBackground();
        nextLevelButton.SetActive(false);
    }

    public IEnumerator LostLevel()
    {
        yield return new WaitForSeconds(2f);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        gamePanel.SetActive(false);
        youLostPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        youLostPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void FillTheBar()
    {
        levelText.text = LevelManager.Instance.currentLevel.gameObject.name;
        progressBar.DOFillAmount(1, 1f).OnComplete(delegate {
            nextLevelButton.SetActive(true);
        });
    }

    Sprite GetBackground()
    {
        int random = Random.Range(0, backgrounds.Length);

        return backgrounds[random];
    }

    #endregion
}
