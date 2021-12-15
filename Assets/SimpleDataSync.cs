
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
public class SimpleDataSync : NetworkBehaviour
{

    [Header("Child Text Objects")]
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _dataText;


    [Header("SyncVars")]
    [SyncVar]
    public int playerNumber;
    [SyncVar]
    public Color playerColor;


    [SyncVar(hook = nameof(OnPlayerDataChanged))]
    public int playerData;
    private void OnPlayerDataChanged(int oldData, int newData)
    {
        _dataText.text = string.Format("Data:{0:000}", newData);
        Debug.Log("OnPlayerDataChanged:" + newData);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("OnStartServer");
        InvokeRepeating(nameof(UpdateData), 1, 1);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("OnStartClient");
        transform.SetParent(GameObject.Find("PlayersPanel").transform);
        float x = 100 + playerNumber % 4 * 150;
        float y = -150 - playerNumber / 4 * 80;
        ((RectTransform)transform).anchoredPosition = new Vector2(x, y);
        _nameText.text = string.Format("Data:{0:000}", playerNumber);
        _nameText.color = playerColor;
    }

    [ServerCallback]
    private void UpdateData()
    {
        Debug.Log("UpdateData:" + playerData);
        playerData = Random.Range(0, 256);

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // apply a shaded background to our player
        transform.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
        Debug.Log("OnStartLocalPlayer");
    }
}
