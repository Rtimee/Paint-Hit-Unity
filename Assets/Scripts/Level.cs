using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region Veriables

    public int paintCount;
    public int torusCount;

    #endregion

    #region Private Methods

    public void LoadLevel()
    {
        Shooting.Instance.Move();
        TorusManager.Instance.ResetValues(torusCount);
        StartCoroutine(TorusManager.Instance.SpawnTorus(paintCount));
    }

    #endregion
}
