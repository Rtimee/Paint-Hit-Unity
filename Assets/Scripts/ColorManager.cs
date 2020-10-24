using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    #region Veriables

    public static ColorManager Instance;

    [SerializeField] MeshRenderer tempBall;
    [SerializeField] Color[] colors;

    Color currentColor;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Private Methods

    public void GenerateColor()
    {
        int random = Random.Range(0, colors.Length);
        Color newColor = colors[random];
        currentColor = newColor;
        tempBall.material.color = currentColor;
    }

    public Color GetColor()
    {
        return currentColor;
    }

    #endregion
}
