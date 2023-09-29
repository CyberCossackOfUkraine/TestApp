using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class CubeExploder
{
    private Transform explosionPosition;
    private float cubeSize;
    private int cubesInRow;
    private float explosionForce;

    private float cubesPivotDistance;
    private Vector3 cubesPivot;

    private List<GameObject> pieces = new List<GameObject>();

    public CubeExploder(Transform explosionPosition, float cubeSize, int cubesInRow, float explosionForce)
    {
        this.explosionPosition = explosionPosition;
        this.cubeSize = cubeSize;
        this.cubesInRow = cubesInRow;
        this.explosionForce = explosionForce;

        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    public void CreateExplosion()
    {
        CreateCubeInPieces();

        Explode();

        DestroyPieces();
    }

    private void CreateCubeInPieces()
    {
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    CreatePiece(x, y, z);
                }
            }
        }
    }

    private void CreatePiece(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.transform.position = new Vector3(explosionPosition.transform.position.x + cubeSize * x, explosionPosition.transform.position.y + cubeSize * y, explosionPosition.transform.position.z + cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;

        pieces.Add(piece);
    }

    private void Explode()
    {

        foreach (GameObject piece in pieces)
        {
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPosition.transform.position - cubesPivot, 4, 0.4f);
            }
        }


    }

    private async void DestroyPieces()
    {
        await Task.Delay(TimeSpan.FromSeconds(3));

        foreach (GameObject piece in pieces)
        {
            UnityEngine.Object.Destroy(piece);
        }
        pieces.Clear();
    }

}
