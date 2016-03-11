using UnityEngine;
using UnityEditor;
using System.Collections;

interface IContentEditorWindow 
{

    void OnGUI();

    void reset();

    void getDataFrom();

    void writeNew();

    void readAll();

    void replace();

}
