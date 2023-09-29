using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishZone : Zone
{

    // Confetti Animation Parameters
    [SerializeField] private float _cubeSize;
    [SerializeField] private int _cubesInRow;

    [SerializeField] private float _explosionForce;

    private CubeExploder _cubeExploder;


    public override void EnterZone(Player player)
    {
        _cubeExploder = new CubeExploder(_cubeSize, _cubesInRow, _explosionForce);

        _cubeExploder.CreateExplosion(player.transform);

        StartCoroutine(NextLevel(player));
    }

    private IEnumerator NextLevel(Player player)
    {
        FadeManager.instance.FadeIn(2f);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
