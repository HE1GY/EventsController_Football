using System;
using System.Collections.Generic;
using UnityEngine;

namespace DATA
{
    public class DataManager : MonoBehaviour
    {
        public Action<string> ShowErrorMess;
        public Action<string> OpenAccountPage;
        
        [SerializeField] private InputSys _inputSys;
        
        private Dictionary<string, User> _users;


        private void OnEnable()
        {
            _inputSys.Logining += CheckInput;
        }

        private void OnDisable()
        {
            _inputSys.Logining -= CheckInput;
        }

        private void Start()
        {
            _users = new Dictionary<string, User>()
            {
                {"admin", new User("Admin", "admin", "admin")},
                {"oleh", new User("Oleh", "oleh", "oleh")}
            };
        }

        private void CheckInput(string login,string password)
        {
            if (login!=null&&_users.ContainsKey(login))
            {
                if (_users[login].Password == password)
                {
                    OpenAccountPage?.Invoke(_users[login].Name);
                }
                else
                {
                    ShowErrorMess?.Invoke($"wrong password  ");
                }
            }
            else
            {
                ShowErrorMess?.Invoke($"there is not account with  login");
            }
        
        }
    }
}