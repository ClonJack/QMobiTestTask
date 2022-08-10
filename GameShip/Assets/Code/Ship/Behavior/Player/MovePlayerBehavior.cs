using Code.Ship.Interfaces;
using TMPro;
using UnityEngine;

namespace Code.Ship.Behavior.Player
{
    [System.Serializable]
    public class MovePlayerBehavior : IMove
    {
        private readonly float _speedShip;
        private readonly float _rotationSpeedShip;
        private readonly Transform _owner;

        private Vector2 _currentVelocity;

        public MovePlayerBehavior(float speedShip, float rotationSpeedShip, Transform owner)
        {
            _speedShip = speedShip;
            _rotationSpeedShip = rotationSpeedShip;
            _owner = owner;
        }

        private Vector3 min => Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        private Vector3 max => Camera.main.ViewportToWorldPoint(new Vector2(1, 1));


        public void Move()
        {
            /*     if (_owner.position.x > max.x || _owner.position.x < min.x)
                 {
                     return;
                 }
     
                 // улетела пуля за екран по оси у
                 if (_owner.position.y > max.y || _owner.position.y < min.y)
                 {
                     return;
                 }*/


            //  Vector3 move = (Input.GetAxis("Vertical") * _speedShip);
            var translation = Input.GetAxis("Vertical") * _speedShip /** Time.deltaTime*/;
            //var translation = _owner.up * (10 * Input.GetAxis("Vertical"));

            var target = (_owner.up * (100 * Input.GetAxis("Vertical")));
            var rotation = -(Input.GetAxis("Horizontal") * _rotationSpeedShip) * Time.deltaTime;

            _owner.transform.position =
                Vector2.SmoothDamp(_owner.transform.position, target, ref _currentVelocity, 1f, 10f);

                //  _owner.Translate(target * Time.deltaTime,Space.World);

          
           
            _owner.Rotate(0, 0, rotation);


            Debug.DrawRay(_owner.transform.position, _owner.up * 5, Color.green);
        }
    }
}