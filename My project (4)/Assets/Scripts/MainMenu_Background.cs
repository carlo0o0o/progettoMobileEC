using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Background : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Vector2 backGroundSpeed;

    private void Update()
    {
        mesh.material.mainTextureOffset += backGroundSpeed * Time.deltaTime;
    }
}
