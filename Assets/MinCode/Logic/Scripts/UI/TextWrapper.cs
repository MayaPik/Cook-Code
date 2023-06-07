using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[Serializable]
public class TextReplacement
{
    public StringReference textToReplace;
    public StringReference replaceToText;

    public override string ToString()
    {
        return $"replace '{textToReplace}'  to -> '{replaceToText}'";
    }
}

/// <summary>
/// Wraps a variable in a given prefix and suffix
/// Updates once the variable is updated if this variable has a change event
/// Works for both UI and world text mesh pro
/// </summary>
public class TextWrapper : BehaviorBase
{
    [Header("The item of the text's reference")]
    public ReferenceSelector Reference;
    [Header("The TMP element. Leave null to read from game object")]
    public TMP_Text Text;
    [Header("Texts that should be replaced with other texts")]
    public List<TextReplacement> TextsReplacements;

    [TextArea]
    public string Prefix;
    [TextArea]
    public string Suffix;

    public void UpdateText()
    {
        if (Text == null)
        {
            Text = (TMP_Text)GetComponent<TextMeshProUGUI>() ?? GetComponent<TextMeshPro>();

            if (Text == null)
            {
                PrintError($"FATAL can't find Text");
            }
        }

        var referenceValue = Reference.GetReferenceValue(printDebugInfo)?.ToString() ?? string.Empty;
        var text = $"{GetStringText(Prefix)}{referenceValue}{GetStringText(Suffix)}";
        var specialTextReplacement = TextsReplacements.FirstOrDefault(r => r.textToReplace.ToString().ToLower() == text.ToLower());

        if (specialTextReplacement != null)
        {
            text = specialTextReplacement.replaceToText;
        }

        Text.text = text;
    }

    private void OnEnable()
    {
        Reference.SetValueChangeListener(gameObject, UpdateText, printDebugInfo);
        UpdateText();
    }

    private string GetStringText(string str)
    {
        return string.IsNullOrEmpty(str) ? string.Empty : str.ToString();
    }
}
