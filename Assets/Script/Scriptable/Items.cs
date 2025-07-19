using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Items : ScriptableObject
{
    public string Name;
    public Sprite icon;
    public string description;
}
