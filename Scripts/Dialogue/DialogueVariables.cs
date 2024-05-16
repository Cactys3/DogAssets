using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEditor;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }
    private Story globalVariablesStory;
    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        // create the story
        globalVariablesStory = new Story(loadGlobalsJSON.text);
        // if we have saved data, load it on startup
        if (true)//if saveload system has saved data to load on startup
        {
            string jsonState = null; //TODO: set jsonState to the data
            //TODO: save the INK variables data to data system
            // globalVariablesStory.state.LoadJson(jsonState); //TODO: remove the comment after jsonState is implemented
        }

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }
    public void StartListening(Story story)
    {
        //important that VariablesToStory is called before the listener is assigned!
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
            if (name.ToLower().Contains("has"))
            {
                Debug.Log("Changed variable: " + name + " to " + ToSystem(value));
                GameObject.FindObjectOfType<ManageInventory>().SetState(name, (bool) ToSystem(value));
            }
            SaveVariables();
        }
        else
        {
            Debug.LogWarning("idk how but DialogueVariables tried to load a variable we don't have defined already: " + name + " set to: " + value);
        }

    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
            Debug.Log("changed (maybe) global dialogue variable: " + variable.Key + " = " + variable.Value);
        }
    }

    public void SetVariable(string VariableName, System.Object Value)
    {
        Ink.Runtime.Object VariableValue = ToInk(Value);
        if (variables.ContainsKey(VariableName) && VariableValue != null)
        {
            variables.Remove(VariableName);
            variables.Add(VariableName, VariableValue);
            if (VariableName.Contains("has_"))
            {
                GameObject.FindObjectOfType<ManageInventory>().SetState(VariableName, (bool) Value);
            }
            SaveVariables();
        }
        else
        {
            Debug.LogWarning("idk how but DialogueVariables tried to SET a variable we don't have defined already: " + VariableName + " set to: " + VariableValue);
        }
    }
    public System.Object GetVariable(string VariableName)
    {
        Ink.Runtime.Object TempObject = null;
        if (variables.ContainsKey(VariableName))
        {//gets the value from the dictionary
            TempObject = variables[VariableName];
            //or this code gets the value from the ink story: globalVariablesStory.variablesState.GetVariableWithName(VariableName);
        }
        else
        {
            Debug.LogWarning("DialogueVariables tried to GET a variable we don't have defined already: " + VariableName);
        }
        return ToSystem(TempObject);
    }

    public void SaveVariables()
    {
        if (globalVariablesStory != null)
        {
            //load the current state ofa ll our variables to the globals stroy
            VariablesToStory(globalVariablesStory);
            //TODO: here we save the globalVariablesStory.state.ToJson() to our save system
        }
    }

    private Ink.Runtime.Object ToInk(System.Object obj)
    {
        if (obj.GetType() == typeof (bool))
        {
            return new Ink.Runtime.BoolValue((bool)obj);
        }
        if (obj.GetType() == typeof(string))
        {
            return new Ink.Runtime.StringValue((string) obj);
        }
        if (obj.GetType() == typeof(float))
        {
            return new Ink.Runtime.FloatValue((float)obj);
        }
        if (obj.GetType() == typeof(int))
        {
            return new Ink.Runtime.IntValue((int)obj);
        }
        
        Debug.LogWarning("tried to convert an object to ink but it wasn't a string/bool/int/float, this is bad");
        return null;
    }
    private System.Object ToSystem(Ink.Runtime.Object inkObj)
    {
        if (inkObj is Ink.Runtime.BoolValue)
        {
            return ((Ink.Runtime.BoolValue)inkObj).value;
        }
        if (inkObj is Ink.Runtime.StringValue)
        {
            return ((Ink.Runtime.StringValue)inkObj).value;
        }
        if (inkObj is Ink.Runtime.FloatValue)
        {
            return ((Ink.Runtime.FloatValue)inkObj).value;
        }
        if (inkObj is Ink.Runtime.IntValue)
        {
            return ((Ink.Runtime.IntValue)inkObj).value;
        }

        Debug.LogWarning("Tried to convert an Ink object to System.Object but it wasn't bool/string/int/float");
        return null;
    }
}
