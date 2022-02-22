using System;
using UnityEngine;

public class InputSys:MonoBehaviour
{
    public Action<string, string> Logining;

    private string _login;
    private string _password;

    public void LogingFieldFilled(string login)
    {
        _login = login;
    }
    public void PasswordFieldFilled(string password)
    {
        _password = password;
    }
    
    
    public void LoginButtonPress()
    {
        Logining?.Invoke(_login,_password);
    }

}