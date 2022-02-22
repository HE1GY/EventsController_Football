using System;
using DATA;
using TMPro;
using UnityEngine;

namespace Windows.Login.Account
{
    public class AccountPage:MonoBehaviour
    {
        [SerializeField] private DataManager _dataManager;
        [SerializeField]private TextMeshProUGUI _nameText;
        
        private Window _window;

        private void Awake()
        {
            _window = GetComponent<Window>();
        }

        private void OnEnable()
        {
            _dataManager.OpenAccountPage += OpenAccountPage;
        }

        private void OnDisable()
        {
            _dataManager.OpenAccountPage -= OpenAccountPage;
        }

        private void OpenAccountPage(string name)
        {
            _window.TurnOn();
            _nameText.text = name;
        }
    }
}