using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereColorText : MonoBehaviour
{
    public EventListener clickedEventListener;
    public TMP_Text Text;
    private string originalText;

    public void Start()
    {
        originalText = Text.text.ToString();
        clickedEventListener.EventRaisedEvent += OnEventRaised;
    }

    public void OnEventRaised(IDictionary<string, object> parametersByName)
    {
        const string SphereColorParameterName = "sphereColor";
        var sphereColorValue = clickedEventListener.GetEventParameter<string>(SphereColorParameterName, parametersByName);

        Text.text = $"{sphereColorValue.ToString().ToUpper()} colored sphere was clicked! I know it by '{SphereColorParameterName}' parameter, of '{clickedEventListener.Event.name}' event";
    }

    public void Reset()
    {
        Text.text = originalText ?? Text.text;
    }
}
