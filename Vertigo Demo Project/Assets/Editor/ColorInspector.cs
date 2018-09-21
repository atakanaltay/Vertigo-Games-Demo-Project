using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
[CustomEditor(typeof(ColorSystem))]
public class ColorInspector : Editor
{
    int count;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ColorSystem targett = (ColorSystem)target;
        if (targett.colorsSc!=null)
        {
            count = targett.colorsSc.Colorz.Count;

            EditorGUILayout.BeginHorizontal();

            count = EditorGUILayout.IntField("Color Count : ", count);
            EditorGUILayout.EndHorizontal();
        if (count != targett.colorsSc.Colorz.Count)
        {
            int ccc;
            if (count>targett.colorsSc.Colorz.Count)
            {
                if (targett.colorsSc.Colorz.Count == 0)
                {
                    targett.colorsSc.Colorz.Add(Color.black);
                }
                 ccc=(count-targett.colorsSc.Colorz.Count);
                for (int i = 0; i < ccc; i++)
                {
                   targett.colorsSc.Colorz.Add(targett.colorsSc.Colorz[targett.colorsSc.Colorz.Count - 1]);
                }
            }
            else
            {
                  ccc=(targett.colorsSc.Colorz.Count-count);
                for (int i = 0; i < ccc; i++)
                {
                    targett.colorsSc.Colorz.RemoveAt(targett.colorsSc.Colorz.Count - 1);
                }

            }
        }
            for (int i = 0; i < targett.colorsSc.Colorz.Count; i++)
            {

                EditorGUILayout.BeginHorizontal();
                targett.colorsSc.Colorz[i] = EditorGUILayout.ColorField("Color " + i.ToString() + ": ", targett.colorsSc.Colorz[i]);
                EditorGUILayout.EndHorizontal();
            } 
        }
        EditorUtility.SetDirty(targett.colorsSc);
    }

}
