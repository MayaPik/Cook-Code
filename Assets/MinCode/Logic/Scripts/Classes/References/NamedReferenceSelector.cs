using System;

/// <summary>
/// A class used to work with ReferenceSelector with a name identifier. In MinCode we use it to pass event parameters 
/// </summary>
[Serializable]
public class NamedReferenceSelector
{
    public string name;
    public ReferenceSelector value;
}
