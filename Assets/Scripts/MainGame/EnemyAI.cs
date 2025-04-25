using UnityEngine;
using System.Collections;
namespace MainGame
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float m_speed = 2f;
        [SerializeField] private float m_moveTime = 2f;
        [SerializeField] private float m_waitTime = 1f;

        private Vector3 moveDirection;

        private void Start()
        {
            StartCoroutine(MoveRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            while (true)
            {
                moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

                float elapsed = 0f;
                while (elapsed < m_moveTime)
                {
                    transform.Translate(moveDirection * m_speed * Time.deltaTime, Space.World);
                    elapsed += Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(m_waitTime);
            }
        }
    }
}