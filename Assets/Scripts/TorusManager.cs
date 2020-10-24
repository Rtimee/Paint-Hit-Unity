using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusManager : MonoBehaviour
{
    #region Veriables

    public static TorusManager Instance;
    public PoolManager[] torusPools;

    [SerializeField] float torusOffset;
    [SerializeField] Animator ringFX;

    List<GameObject> torusGroup;

    int torusCount;
    int counter;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;

        torusGroup = new List<GameObject>();
    }

    #endregion

    #region Private Mathods

    void AddTorusToList(GameObject torus)
    {
        torusGroup.Add(torus);
    }

    void SlideTorusGroup()
    {
        foreach (GameObject torus in torusGroup)
        {
            torus.transform.DOMoveY(torus.transform.position.y - torusOffset, .5f, false).SetEase(Ease.Linear);
        }
    }

    public IEnumerator SpawnTorus(int paintCount)
    {
        yield return new WaitForSeconds(.1f);
        if (counter >= torusCount)
            StartCoroutine(UIManager.Instance.ShowTheNextLevelScreen());
        else
        {
            UIBalls.Instance.SpawnUIBall(LevelManager.Instance.currentLevel.paintCount);
            if (torusGroup.Count > 0)
                SlideTorusGroup();
            ColorManager.Instance.GenerateColor();
            int random = Random.Range(0, torusPools.Length);
            GameObject torus = torusPools[random].GetObjFromPool(transform.position, transform.rotation);
            torus.GetComponent<Torus>().LoadTorus(paintCount);
            torus.transform.DOMoveY(0, .5f, false).SetEase(Ease.Linear).OnComplete(delegate
            {
                AddTorusToList(torus);
                Shooting.Instance.currentState = PlayerState.States.canShoot;
                if (torusGroup.Count > 1)
                    ringFX.enabled = true;

            });
            counter++;
        }
    }

    public void ResetValues(int count)
    {
        counter = 0;
        torusCount = count;
        ResetScene();
        torusGroup.Clear();
    }

    void ResetScene()
    {
        foreach (GameObject torus in torusGroup)
        {
            int index = torus.GetComponent<Torus>().GetIndex();
            torus.GetComponent<Torus>().ResetTorus();
            torusPools[index].ReturnObjToPool(torus);
        }
    }

    #endregion
}
