using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRender : MonoBehaviour
{
    [SerializeField] GameObject RenderPlayer = null;
    [SerializeField] Player player = null;
    // Start is called before the first frame update
    void Awake()
    {
        player.TurnPlaying += ShowPlayer;
        player.PlayerInit += SetMat;
    }

    public void ShowPlayer(bool _value)
    {
        gameObject.SetActive(_value);
        RenderPlayer.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SetMat(Material _mat, Color _col)
    {
        RenderPlayer.GetComponent<MeshRenderer>().material.color = _col;
    }
    private void OnMouseOver()
    {
        RenderPlayer.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnMouseExit()
    {
        RenderPlayer.GetComponent<MeshRenderer>().enabled = true;
    }
    private void OnMouseDown()
    {
        player.GetCell().OnMouseDown();
    }
    private void OnMouseUp()
    {
        player.GetCell().OnMouseUp();
    }
}
