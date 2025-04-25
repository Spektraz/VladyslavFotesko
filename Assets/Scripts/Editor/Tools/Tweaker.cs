using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Editor.Tools
{
    public class TweakerWindow : EditorWindow
    {
        [MenuItem("Game/Tools/Tweaker")]
        public static void ShowWindow()
        {
            TweakerWindow wnd = (TweakerWindow)EditorWindow.GetWindow(typeof(TweakerWindow));
            wnd.Show();
        }

        List<BaseTweakerButton> buttons = new List<BaseTweakerButton>();
        List<BaseTweakerButton> buttonsWithIntInput = new List<BaseTweakerButton>();
        List<BaseTweakerButton> buttonsWithStringInput = new List<BaseTweakerButton>();

        void OnGUI()
        {
            if(buttons.Count == 0)
                InitButtons();
            
            ShowButtons();
        }

        void InitButtons()
        {
            buttons = new List<BaseTweakerButton>(Tweaks.GetAllButtons());
          
        }

        void ShowButtons()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("===== Regular Action buttons =====");
            GUILayout.Space(20);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            
            
            float width = EditorGUIUtility.currentViewWidth;

            foreach (var button in buttons)
            {
                width -= button.GetSize();
                if (width < 0.0f)
                {
                    width = EditorGUIUtility.currentViewWidth - button.GetSize();
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }

                button.OnGUI();
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Space(20);
            GUILayout.Label("===== Int Action buttons =====");
            GUILayout.Space(20);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            
            foreach (var button in buttonsWithIntInput)
            {
                width -= button.GetSize();
                if (width < 0.0f)
                {
                    width = EditorGUIUtility.currentViewWidth - button.GetSize();
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }

                button.OnGUI();
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Space(20);
            GUILayout.Label("===== String Action buttons =====");
            GUILayout.Space(20);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            
            foreach (var button in buttonsWithStringInput)
            {
                width -= button.GetSize();
                if (width < 0.0f)
                {
                    width = EditorGUIUtility.currentViewWidth - button.GetSize();
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }

                button.OnGUI();
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}