using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torus : MonoBehaviour
{
    #region Veriables

    [SerializeField] List<TorusPart> torusParts;
    [SerializeField] TorusDataManager torusData;


    List<TorusPart> nonPaintedParts;
    int paintCount;
    int counter;
    Color torusColor;
    Tweener tween;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        nonPaintedParts = new List<TorusPart>();
    }

    #endregion

    #region Private Methods

    void RotatingRight()
    {
        tween = transform.DORotate(Vector3.zero + new Vector3(0, torusData.rightRotateValue, 0), 360 / torusData.leftRotateValue * torusData.rotateSpeed, RotateMode.WorldAxisAdd).OnComplete(delegate
        {
            RotatingLeft();
        });
    }
    void RotatingLeft()
    {
        tween = transform.DORotate(Vector3.zero + new Vector3(0, -torusData.leftRotateValue, 0), 360 / torusData.rightRotateValue * torusData.rotateSpeed, RotateMode.WorldAxisAdd).OnComplete(delegate
        {
            RotatingRight();
        });
    }

    public void ControllTorusParts()
    {
        if (counter < paintCount)
        {
            counter++;
            if (counter == paintCount)
                PaintAllParts();
        }
    }

    void PaintAllParts()
    {
        Shooting.Instance.currentState = PlayerState.States.Wait;
        GameManager.Instance.UpdateTheRemainText();
        foreach(TorusPart torusPart in torusParts)
        {
            bool isPainted = torusPart.GetComponent<TorusPart>().painted;
            if (!isPainted)
                torusPart.GetComponent<TorusPart>().Paint(torusColor,true);
        }
        StartCoroutine(TorusManager.Instance.SpawnTorus(LevelManager.Instance.currentLevel.paintCount));
        KillTween();
    }

    void PaintRandomParts(int _count)
    {
        FillTheTempArray();
        for (int i = 0; i < _count; i++)
        {
            int random = Random.Range(0, nonPaintedParts.Count);
            if (!nonPaintedParts[random].painted)
            {
                nonPaintedParts[random].Paint(torusColor, false);
                nonPaintedParts.Remove(nonPaintedParts[random]);
            }
        }
    }

    void FillTheTempArray()
    {
        nonPaintedParts.Clear();
        foreach (TorusPart part in torusParts)
        {
            nonPaintedParts.Add(part);
        }
    }

    public void SetColor(Color color)
    {
        torusColor = color;
    }

    public void KillTween()
    {
        tween.Kill();
    }

    public void SetPaintCount(int count)
    {
        paintCount = count;

    }

    public int GetIndex()
    {
        return torusData.arrayIndex;
    }

    public void ResetTorus()
    {
        foreach (TorusPart torusPart in torusParts)
        {
            torusPart.Clear();
        }
    }

    public void LoadTorus(int count)
    {
        RotatingRight();
        counter = 0;
        SetColor(ColorManager.Instance.GetColor());
        PaintRandomParts((torusParts.Count - count) / 2);
        SetPaintCount(count);
    }

    #endregion
}
