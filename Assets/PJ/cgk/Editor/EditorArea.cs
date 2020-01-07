﻿using UnityEditor;

[CustomEditor(typeof(Area))]
public class EditorArea : Editor {

    protected SerializedProperty areaType;
    protected SerializedProperty offset;
    protected SerializedProperty xSize;
    protected SerializedProperty ySize;
    protected SerializedProperty radius;
    protected SerializedProperty length;

    protected virtual void OnEnable() {
        this.areaType = this.serializedObject.FindProperty("areaShape");
        this.offset = this.serializedObject.FindProperty("offset");
        this.xSize = this.serializedObject.FindProperty("xSize");
        this.ySize = this.serializedObject.FindProperty("zSize");
        this.radius = this.serializedObject.FindProperty("radius");
        this.length = this.serializedObject.FindProperty("length");
    }

    public override void OnInspectorGUI() {
        this.serializedObject.UpdateIfRequiredOrScript();

        Area spawnArea = (Area)this.target;

        EditorGUILayout.PropertyField(this.areaType);
        EditorGUILayout.PropertyField(this.offset);

        switch(spawnArea.areaShape) {
            case EnumRegionShape.POINT:
                break;
            case EnumRegionShape.SQUARE:
                EditorGUILayout.PropertyField(this.xSize);
                EditorGUILayout.PropertyField(this.ySize);
                break;
            case EnumRegionShape.CIRCLE:
                EditorGUILayout.PropertyField(this.radius);
                break;
            case EnumRegionShape.LINE:
                EditorGUILayout.PropertyField(this.length);
                break;
        }

        this.serializedObject.ApplyModifiedProperties();

        this.DrawDefaultInspector();
    }
}