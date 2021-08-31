using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary Storage", menuName ="Data Objects/Dictionary Storage Object")]
public class Functionality : ScriptableObject
{
    [SerializeField]
    private List<string> keys = new List<string>();
    [SerializeField]
    private List<object> values = new List<object>();

    public List<string> Keys { get => keys; set => keys = value; }
    public List<object> Values { get => values; set => values = value; }
}
