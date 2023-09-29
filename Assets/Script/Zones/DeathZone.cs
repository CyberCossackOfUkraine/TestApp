using System.Collections;
using UnityEngine;

public class DeathZone : Zone
{
    // Death Animation Parameters
    [SerializeField] private float _cubeSize;
    [SerializeField] private int _cubesInRow;

    [SerializeField] private float _explosionForce;

    private CubeExploder _cubeExploder;

    ShieldManager _shieldManager;

    public override void EnterZone(Player player)
    {
        if (_shieldManager == null)
        {
            _shieldManager = player.gameObject.GetComponent<ShieldManager>();
        }

        if (_shieldManager.isShieldActive) return;
        
        player.gameObject.SetActive(false);

        _cubeExploder = new CubeExploder(player.transform, _cubeSize, _cubesInRow, _explosionForce);

        _cubeExploder.CreateExplosion();

        StartCoroutine(Restart(player));

    }

    private IEnumerator Restart(Player player)
    {
        yield return new WaitForSeconds(3f);

        player.gameObject.GetComponent<PlayerAI>().RestartPlayer();
        
    }
}
