using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Authorization
{
    static private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/UserData.json";
    static public void LogIn()
    {
        string login = Login.Instance.login.text;
        string pass = Login.Instance.password.text;
        string[] usersString = File.ReadAllText(path).Split(';');
        if (usersString.Length == 0) return;
        List<Account> users = new List<Account>();
        foreach (string user in usersString)
            if (user != "") users.Add(JsonUtility.FromJson<Account>(user));
        foreach (Account user in users)
            if (login == user.Login && pass == user.Password)
            {
                Login.Instance.menu.SetActive(true);
                Login.Instance.gameObject.SetActive(false);
            }
    }
    static public void Register()
    {
        Account user = new Account(Registration.Instance.firstname.text, Registration.Instance.lastname.text,
            Registration.Instance.email.text, Registration.Instance.password.text);
        string json = JsonUtility.ToJson(user, true);
        File.AppendAllText(path, json + ";");
    }
    static public void Play() => SceneManager.LoadScene(1);
    [System.Serializable]
    struct Account
    {
        public string Login;
        public string Password;
        public string FirstName;
        public string LastName;
        public string Email;
        public Account(string firstname, string lastname, string email, string password)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Login = firstname;
            Password = password;
        }
    }
}
