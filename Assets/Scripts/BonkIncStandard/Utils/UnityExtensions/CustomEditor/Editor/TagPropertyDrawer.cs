using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

 [CustomPropertyDrawer(typeof(Tag))]
public class TagPropertyDrawer : MultipleChoicePropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var tags = new List<string>(UnityEditorInternal.InternalEditorUtility.tags);
        var tagAttribute = this.attribute as Tag;
        
        if(property.propertyType != SerializedPropertyType.String){
            Debug.LogWarning("Tag attribute only allowed on string fields");
            EditorGUI.PropertyField(position, property, label);
            return;
        }

        if(tagAttribute.useUnityDefaultTagDropdown) {
            CreateUnityTagField(position, property, label, tags);
        } else {
            base.OnGUI(position, property, label);
        }
    }

    private void CreateUnityTagField(Rect position, SerializedProperty property, GUIContent label, List<string> tags){
            EditorGUI.BeginProperty(position, label, property);

            string selectedTag = EditorGUI.TagField(
                position,
                label,
                property.stringValue
            );
            int newTagIndex = tags.IndexOf(selectedTag);
            property.stringValue = selectedTag;

            if( newTagIndex < 0){
                Debug.LogWarning($"The selected tag {selectedTag} does not exist.");
            }

            EditorGUI.EndProperty();
    }

}