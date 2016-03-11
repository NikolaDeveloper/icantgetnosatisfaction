using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.Editor
{
	public enum LineNameType{
		prefix,
		suffix
	}

	class LineNameEditor : EditorWindow, IContentEditorWindow
    {
		private string fName = "LineNames";

        private bool createNew = false;
        private bool load = false;
        private bool stored = false;
        private bool editing = false;

        private bool gotName = false;
        private int eventsCount = 0;
        private JSONObject loadedEvents;


        private string lineName;
		private LineNameType lineType;


        [MenuItem("Tools/Managers/Line Editor")]
        public static void ShowWindow()
        {
			EditorWindow.GetWindow(typeof(LineNameEditor), false, "Line Editor");
        }


        public void getDataFrom()
        {
			
			lineType = (LineNameType)loadedEvents [eventsCount].GetField ("type").n;
			lineName = loadedEvents[eventsCount].GetField("name").PrintFormattedString();


        }

        public void OnGUI()
        {
			
            EditorGUILayout.HelpBox("This is a tool for managing the library of names available to generate a random train line name.\n\nCreate a new line name by choosing NEW and defining a Prefix or Suffix to be generated.\n\nYou can also load or edit any of the current line names in the library by choosing the LOAD option.", MessageType.Info);

            GUILayout.Label("Manage", EditorStyles.boldLabel);
            if (GUILayout.Button("New"))
            {
                createNew = true;
                load = false;
                stored = false;
                gotName = false;
                reset();
            }
            if (GUILayout.Button("Load"))
            {
                createNew = false;
                load = true;
                stored = false;
                reset();
                readAll();

            }
            if (GUILayout.Button("Reset"))
            {
                createNew = false;
                load = false;
                stored = false;
                gotName = false;
                reset();
            }


            EditorGUILayout.Separator();
            // The actual window code goes here
            if (createNew)
            {

                GUILayout.Label("Create New", EditorStyles.boldLabel);
                lineName = EditorGUILayout.TextField("Name", lineName);

				GUILayout.Label ("Type");
				lineType = (LineNameType)EditorGUILayout.EnumPopup (lineType);

                EditorGUILayout.Space();


                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();


                if (GUILayout.Button("Clear"))
                {
                    reset();
                    this.Repaint();
                }

                if (GUILayout.Button("Store"))
                {
                    createNew = false;
                    stored = true;
                    if (!editing)
                    {
                        writeNew();
                    }
                    else {
                        replace();
                    }
                    this.Repaint();
                }
                EditorGUILayout.EndHorizontal();
            }

            if (stored)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Line Name Stored!");
            }

            if (gotName && loadedEvents)
            {
				
                EditorGUILayout.LabelField("All Names");
                EditorGUILayout.LabelField((eventsCount + 1) + "/" + loadedEvents.Count);

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Info", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Line Name: " + loadedEvents[eventsCount].GetField("name"));
				EditorGUILayout.LabelField("Type: " + ((LineNameType)loadedEvents[eventsCount].GetField("type").n).ToString());

                if (GUILayout.Button("Edit"))
                {
                    editing = true;
                    createNew = true;
                    getDataFrom();
                    gotName = false;
                    
                }

                //GUILayout.Button ("Delete");
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Previous"))
                {
                    eventsCount--;
                    if (eventsCount <= 0)
                    {
                        eventsCount = 0;
                    }
                    this.Repaint();
                }
                if (GUILayout.Button("Next"))
                {
                    eventsCount++;

                    if (eventsCount > loadedEvents.Count - 1)
                    {
                        eventsCount = loadedEvents.Count - 1;
                    }

                    this.Repaint();
                }
                EditorGUILayout.EndHorizontal();

            }
        }
			
			

        public void readAll()
        {
            loadedEvents = DataLoader.read(fName);

            gotName = true;
        }

        public void replace()
        {
			DataLoader.removeFromData(eventsCount, fName);
            writeNew();

        }

        public void reset()
        {
            lineName = "";
			lineType = LineNameType.prefix;
            editing = false;

        }

        public void writeNew()
        {
			
            JSONObject eventObj = new JSONObject(JSONObject.Type.OBJECT);
            eventObj.SetField("name", lineName);
			eventObj.SetField("type", (int)lineType);

            DataLoader.writeData(fName, eventObj);
            Debug.Log(eventObj.Print());
            reset();
           

        }
    }
}
