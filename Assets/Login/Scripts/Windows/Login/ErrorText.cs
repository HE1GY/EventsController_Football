
using DATA;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorText : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private TextMeshProUGUI _text;
    

    private void OnEnable()
    {
        _dataManager.ShowErrorMess += ShowError;
    }
    
    private void OnDisable()
    {
        _dataManager.ShowErrorMess -= ShowError;
    }


    private void ShowError(string errorText)
    {
        _text.text = errorText;
    }
}
