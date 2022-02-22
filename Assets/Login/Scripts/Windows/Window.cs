using UnityEngine;

namespace Windows.Login
{
    public class Window : MonoBehaviour
    {
        private Animator _loginAnim;
        private const string IsTurnOn = "TurnOn";
        private static readonly int On = Animator.StringToHash(IsTurnOn);

        private void Awake()
        {
            _loginAnim = GetComponent<Animator>();
        }

        public void TurnOff()
        {
            _loginAnim.SetBool(On,false);
        }

        public void TurnOn()
        {
            _loginAnim.SetBool(On, true);
        }
    }
}
