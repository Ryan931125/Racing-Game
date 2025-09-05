using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class SocketClient
{
    private Socket socketClient;
    private Thread thread;
    private byte[] data = new byte[1024];

    public bool isTriggered = false;
    public float roll = 0f, pitch = 0f, yaw = 0f;
    public float potentiometer = 0f;

    public SocketClient(string hostIP, int port) {
        thread = new Thread(() => {
            // while the status is "Disconnect", this loop will keep trying to connect.
            while (true) {
                try {
                    socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Debug.Log("Connecting\n");
                    socketClient.Connect(new IPEndPoint(IPAddress.Parse(hostIP), port));
                    Debug.Log("Connected\n");
                    // while the connection
                    while (true) {
                        /*********************************************************
                         * TODO: you need to modify receive function by yourself *
                         *********************************************************/
                        if (socketClient.Available < 100) {
                            Thread.Sleep(1);
                            continue;
                        }
                        int length = socketClient.Receive(data);
                        string message = Encoding.UTF8.GetString(data, 0, length);
                        // Debug.Log("Recieve message: <" + message + ">");

                        string [] updates = message.Split("\n");
                        foreach(string update in updates){
                            string []vals = update.Split(" ");
                            if(vals.Length < 5)
                                break;
                            // Debug.Log("Parsing update: <" + update + ">");
                            try {
                                isTriggered = Convert.ToBoolean(vals[0][0] - '0');
                                potentiometer = (float) Convert.ToDouble(vals[1]) / 1024f;
                                roll = (float) Convert.ToDouble(vals[2]);
                                pitch = (float) Convert.ToDouble(vals[3]);
                                yaw = (float) Convert.ToDouble(vals[4]);
                                // Debug.Log($"Updated to: <{isTriggered} {potentiometer} {roll} {pitch} {yaw}>");
                            } catch (Exception readEx) {
                                Debug.Log(readEx.Message);
                            }
                        }
                        // */
                    }
                } catch (Exception ex) {
                    if (socketClient != null) {
                        socketClient.Close();
                    }
                    Debug.Log(ex.Message);
                }
            }
        });
        thread.IsBackground = true;
        thread.Start();
    }

    public void Close() {
        thread.Abort();
        if (socketClient != null) {
            socketClient.Close();
        }
    }
}
