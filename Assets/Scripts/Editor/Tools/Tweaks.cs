using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEditor;

namespace Assets.Scripts.Editor.Tools
{
    public abstract class BaseTweakerButton
    {
        public const int EDITOR_GUI_WIDTH = 150;

        public UnityAction pressAction;
        public string name;
        public GUIContent content;

        public virtual float GetSize()
        {
            return GUI.skin.button.CalcSize(content).x + GUI.skin.button.margin.horizontal;
        }

        public virtual bool OnGUI()
        {
            bool pressed = GUILayout.Button(name);
            if(pressed)
            {
                pressAction();
                return true;
            }

            return false;
        }
        
    }
    public class TweakerButton : BaseTweakerButton
    {
        public TweakerButton(string name, UnityAction pressAction)
        {
            this.name = name;
            this.pressAction = pressAction;
            content = new GUIContent(name);
        }

        public override float GetSize()
        {
            return GUI.skin.button.CalcSize(content).x + GUI.skin.button.margin.horizontal;
        }

        public override bool OnGUI()
        {
            bool pressed = GUILayout.Button(name);
            if(pressed)
            {
                pressAction();
                return true;
            }

            return false;
        }
    }
    public class TweakerButtonWithIntInput : BaseTweakerButton
    {
        public const int BUTTON_WIDTH = 200;

        public UnityAction<int> pressAction;
        public string name;
        public string inputName;
        public int inputValue;

        public TweakerButtonWithIntInput(string name, string inputName, UnityAction<int> pressAction)
        {
            this.name = name;
            this.inputName = inputName;
            this.pressAction = pressAction;
            content = new GUIContent(name);
        }

        public override float GetSize()
        {
            return BUTTON_WIDTH;
        }

        public override bool OnGUI()
        {
            GUILayout.BeginVertical();
            var value = EditorGUILayout.IntField($"{inputName}:{inputValue}", inputValue, GUILayout.Width(BUTTON_WIDTH));
            bool pressed = GUILayout.Button(name, GUILayout.Width(BUTTON_WIDTH));
            GUILayout.EndVertical();
            inputValue = Mathf.Max(0, value);
            if(pressed)
            {
                pressAction(inputValue);
                return true;
            }

            return false;
        }
    }
    public class TweakerButtonWithStringInput : BaseTweakerButton
    {
        public const int BUTTON_WIDTH = 260;

        public UnityAction<string> pressAction;
        public string name;
        public string inputName;
        public string inputValue;

        public TweakerButtonWithStringInput(string name, string inputName, UnityAction<string> pressAction)
        {
            this.name = name;
            this.inputName = inputName;
            this.pressAction = pressAction;
            content = new GUIContent(name);
        }

        public override float GetSize()
        {
            return BUTTON_WIDTH;
        }

        public override bool OnGUI()
        {
            GUILayout.BeginVertical();
            inputValue = EditorGUILayout.TextField($"{inputName}:{inputValue}", inputValue, GUILayout.Width(BUTTON_WIDTH));
            bool pressed = GUILayout.Button(name, GUILayout.Width(BUTTON_WIDTH));
            GUILayout.EndVertical();
            if(pressed)
            {
                pressAction(inputValue);
                return true;
            }

            return false;
        }
    }

    public static class Tweaks
    {
        public static List<BaseTweakerButton> GetAllButtons()
        {
            var buttons = new List<BaseTweakerButton>();
       
            buttons.Add(new TweakerButton("RESET GAME", () =>
            {
                PlayerPrefs.DeleteAll();
            }));
            
            buttons.Add(new TweakerButton("Show device ID", () =>
            {
                Debug.LogWarning(SystemInfo.deviceUniqueIdentifier);
            }));
                    
            
            return buttons;
            
        }     
    }
}