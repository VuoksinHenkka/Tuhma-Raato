using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class material_seetrough : MonoBehaviour
{
    private MaterialPropertyBlock ourPropertyBlock;
    private Renderer ourRenderer;

    private void Start()
    {
        ourRenderer = GetComponent<Renderer>();
        ourPropertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        ourPropertyBlock.SetVector("_Position", GameManager.Instance.ref_Camera.playerPositionInScreenSpace);
        ourRenderer.SetPropertyBlock(ourPropertyBlock);
    }
}
