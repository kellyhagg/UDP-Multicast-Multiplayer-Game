using System;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;
using NetworkAPI;

public class PlayerMovement : MonoBehaviour
{
    //variables made public to allow editing in Unity editor
    NetworkComm networkComm;
    public float speed = 5;
    public Rigidbody rb1;
    public Rigidbody rb2;
    public float horizontalMultiplier = 2;
    float horizontalInput;
    
    public GameObject localPlayer, remotePlayer;
    public Vector3 localPlayerPos = new Vector3();
    public Vector3 remotePlayerPos;
    
    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb1.MovePosition(rb1.position + forwardMove + horizontalMove);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        networkComm = new NetworkComm();
        networkComm.MsgReceived += new NetworkComm.MsgHandler(processMsg);
        (new Thread(new ThreadStart(networkComm.ReceiveMessages))).Start();
        // (new Thread(new ThreadStart(threadfunc))).Start();
        remotePlayer = GameObject.Find("Player2"); localPlayer = GameObject.Find("Player1");
        remotePlayer.transform.position = remotePlayerPos;
        localPlayer.transform.position = localPlayerPos;  
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        // rb2.position = remotePlayerPos;
        remotePlayer.transform.position = remotePlayerPos;
        Console.WriteLine("Updated remotePlayerPos: " + remotePlayerPos);
    }
    
    public void processMsg(string message)
    {
        string[] msgParts = message.Split(";");
        if (msgParts[0].Contains("ID=2")) {
            string[] coordinates = msgParts[1].Split(",");
            Debug.Log("Inside processMsg");
            float x = float.Parse(coordinates[0], CultureInfo.InvariantCulture.NumberFormat);
            float y = float.Parse(coordinates[1], CultureInfo.InvariantCulture.NumberFormat);
            float z = float.Parse(coordinates[2], CultureInfo.InvariantCulture.NumberFormat);
            remotePlayerPos.x = x; remotePlayerPos.y = y; remotePlayerPos.z = z;
        }
    }
}
