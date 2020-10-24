using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusPart : MonoBehaviour
{
    #region Veriables

    public bool painted;

    [SerializeField] Torus parent;

    MeshRenderer renderer;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    #endregion

    #region Private Methods

    public void Paint(Color color,bool truePart)
    {
        renderer.material.color = color;
        painted = true;
        if(truePart)
            parent.ControllTorusParts();
    }

    public void StopTorus()
    {
        parent.KillTween();
        Shooting.Instance.currentState = PlayerState.States.End;
    }

    public void Clear()
    {
        renderer.material.color = Color.white;
        painted = false;
    }

    #endregion
}
