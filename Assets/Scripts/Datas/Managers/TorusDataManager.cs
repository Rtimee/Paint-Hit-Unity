using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Torus Data", menuName = "Torus")]
public class TorusDataManager : ScriptableObject
{
    public float rotateSpeed;
    public float rightRotateValue;
    public float leftRotateValue;
    public int arrayIndex;
}
