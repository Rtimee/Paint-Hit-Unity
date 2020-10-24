using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBalls : MonoBehaviour
{
    #region Veriables

    public static UIBalls Instance;

    [SerializeField] GameObject ballPrefab;

    List<GameObject> ballList;

    int index;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
        ballList = new List<GameObject>();
    }

    #endregion

    #region Private Methods

    public void SpawnUIBall(int count)
    {
        Shooting.Instance.SetBallCount(count);
        index = count;
        ballList.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject ball = Instantiate(ballPrefab, transform);
            ballList.Add(ball);
        }
    }

    public void DestroyUIBall()
    {
        index--;
        Destroy(ballList[index]);
    }

    public void ClearList()
    {
        foreach (GameObject ball in ballList)
        {
            Destroy(ball);
        }
        ballList.Clear();
    }

    #endregion
}
