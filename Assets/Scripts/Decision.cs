using System;
using UnityEngine;

[Serializable]
public class Decision
{
    public string Name;

    [TextArea]
    public string Description;
    public string[] Options;
}
