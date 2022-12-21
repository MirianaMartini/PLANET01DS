using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;

public class SessionManager : MonoBehaviour
{
    public AddPatchPlayer _form;

    private FileStream fs;
    private string path = "Session.json";
    private SessionsClass _sessions;

    void Awake(){
        ////////////////// Create the file if it doesn't exist.
        if (!File.Exists(path)){ 
            CreateJSON();
        }
        else {
            string jsonString = File.ReadAllText(path);
            _sessions = JsonUtility.FromJson<SessionsClass>(jsonString);
        }
        LoadSessions();
    }

    public void LoadSessions(){
        if (_sessions == null || _sessions.Sessions.Count == 0) return;
        else {
            foreach(Session _session in _sessions.Sessions){
                _form.CreateItemGUI(_session.nameID, false);
            }
        }
    }

    public void ReloadSessions(){
        _form.DeleteItemsGUI();
        LoadSessions();
    }

    public bool ExistSession(string id){
        string jsonString = File.ReadAllText(path);
        _sessions = JsonUtility.FromJson<SessionsClass>(jsonString);
        if (_sessions == null || _sessions.Sessions.Count == 0) {
            CreateJSON();
            return false;
        }
        foreach(Session _session in _sessions.Sessions){
            if(_session.nameID == id) return true;
        }
        return false;
    }

    public void AddSession(string id){
        Session user = new Session(id, getDate(), getDateTime());
        _sessions.Sessions.Add(user);
        File.WriteAllText(path, JsonUtility.ToJson(_sessions));
    }

    public void DeleteSession(string id){
        SessionsClass _tmp = new SessionsClass();

        foreach(Session _session in _sessions.Sessions){
            if(_session.nameID != id){
                _tmp.Sessions.Add(_session);
            }
        }
        _sessions = _tmp;
        File.WriteAllText(path, JsonUtility.ToJson(_sessions));
    }

    public int CountSessions(){
        return _sessions.Sessions.Count;
    }

    public Session GetSession(string id){
        foreach(Session _session in _sessions.Sessions){
            if(_session.nameID == id){
                return _session;
            }
        }
        return null;
    }

    public void EditSession(string id){
        
    }
    private void CreateJSON(){
        File.Delete(path);
        fs = File.Create(path); 
        fs.Close(); 
        _sessions = new SessionsClass();
        File.WriteAllText(path, JsonUtility.ToJson("{}"));
    }

    private string getDate(){
        return (DateTime.Now.ToString("dd/MM/yyyy"));
    }

     private string getDateTime(){
        return (DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss"));
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void ReadJSON(){
        //Read in JSON file
        string jsonString = File.ReadAllText(path);
        _sessions = JsonUtility.FromJson<SessionsClass>(jsonString);

        Debug.Log(_sessions.Sessions[0].nameID); //Outputs Patch1.cs
    }

    private void WriteJSON(){
        //Write to JSON file
        _sessions.Sessions[0].nameID = "Patch3.cs"; //Changes the name of the entry named Patch1.cs
        File.WriteAllText(path, JsonUtility.ToJson(_sessions)); //Writes updata back to file
        //////////////////////////////////////////////////////////
    }
}

[Serializable]
public class SessionsClass {
    public List<Session> Sessions;

    public SessionsClass (){
        Sessions = new List<Session>();
    }
}

[Serializable]
public class Session {
  public string nameID;
  public string creationDate;
  public string lastEdit;

  public Session (string id, string dat, string edit){
      nameID = id;
      creationDate = dat;
      lastEdit = edit;
  }
}