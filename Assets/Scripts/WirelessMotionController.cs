using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class WirelessMotionController : MonoBehaviour
{

    const string hostIP = "192.168.128.5";
    const int port = 80;

    private SocketClient socketClient;
    private Quaternion _quaternion;
    public bool isTriggered;
    public float pitch;
    public float yaw;
    public float roll;
    public float potentiometer;
    [SerializeField]
    private float pitchOffset;
    [SerializeField]
    
    private float yawOffset;

    private bool calibrated = false;

    private void Awake() {
        socketClient = new SocketClient(hostIP, port);
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // Calibrate
        if(calibrated == false){
            if(socketClient.isTriggered){
                pitchOffset = socketClient.roll;
                yawOffset = socketClient.yaw;
                calibrated = true;
            }
            return;
        }
        isTriggered = socketClient.isTriggered;
        pitch = (socketClient.pitch - pitchOffset);
        yaw = socketClient.yaw - yawOffset;
        roll = socketClient.roll;
        potentiometer = socketClient.potentiometer;
    }

    void OnDestroy () {
        socketClient.Close();
    }
}
