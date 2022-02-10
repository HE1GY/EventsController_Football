using UnityEngine;

namespace GameLogic.Creature.PlayerScripts
{
    public class PlayerInput:MonoBehaviour
    {
        public float InputZ { private set; get;}
        private void Update()
        {
            InputZ = Input.GetAxis("Vertical");
        }
    }
}
