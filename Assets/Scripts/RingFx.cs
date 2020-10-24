using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFx : MonoBehaviour
{
    #region Veriables

    Animator anim;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    #endregion

    #region Private Methods

    public void CloseAnimator()
    {
        anim.enabled = false;
    }

    #endregion
}
