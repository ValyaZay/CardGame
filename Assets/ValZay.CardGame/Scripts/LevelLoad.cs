using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ValZay.CardGame
{
    public class LevelLoad : MonoBehaviour
    {
        [SerializeField] private float waitTillStart = 3.0f;

        private int startSceneIndex = 1;
        
        IEnumerator Start()
        {
            yield return new WaitForSeconds(waitTillStart);
            LoadStartScene();
        }

        private void LoadStartScene()
        {
            SceneManager.LoadScene(startSceneIndex);
        }
    }
}