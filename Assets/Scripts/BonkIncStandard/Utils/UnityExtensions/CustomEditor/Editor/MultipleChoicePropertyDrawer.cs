using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

 [CustomPropertyDrawer(typeof(MultipleChoice))]
public class MultipleChoicePropertyDrawer : PropertyDrawer
{

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        MultipleChoice multipleChoiceAttribute = this.attribute as MultipleChoice;
        if (property.propertyType == SerializedPropertyType.String)
        {            
            int current = multipleChoiceAttribute.choices.IndexOf(property.stringValue);
            List<string> choices = new List<string>(multipleChoiceAttribute.choices);
            if(current < 0){
                choices.Add(property.stringValue);
                current = choices.Count - 1;
            }
            current = EditorGUI.Popup (position, label.text, current, choices.ToArray());
            property.stringValue = choices[current];                
        }
        else
        {
            Debug.LogWarning("MultipleChoice attribute only allowed on string fields");
            EditorGUI.PropertyField(position, property, label);
        }
        EditorGUI.EndProperty();
    }
}