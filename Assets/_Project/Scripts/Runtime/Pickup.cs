using UnityEngine;

namespace RollABall
{
    /// <summary>
    /// 可收集物：持續旋轉，玩家碰到後加分並隱藏。Collider 需勾選 Is Trigger。
    /// </summary>
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationSpeed = new Vector3(15f, 30f, 45f);

        private bool collected;

        private void Update()
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (collected) return;

            var player = other.GetComponent<BallController>();
            if (player == null) return;

            collected = true;
            player.CollectPickup();
            gameObject.SetActive(false);
        }
    }
}
