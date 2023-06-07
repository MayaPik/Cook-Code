using System;

[Serializable]
public enum ParameterType
{
    Integer,
    String,
    GameObject,
    Boolean,
    Float,
    Vector3
}

[Serializable]
public class EventParameter
{
    public ParameterType parameterType;

}
