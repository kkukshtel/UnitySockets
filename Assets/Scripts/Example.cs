using UnityEngine;
using Valve.Sockets;
using System.Collections;
using System.Threading;

public class Example : MonoBehaviour
{
    NetworkingSockets server;
    Address address;
    const int maxMessages = 20;
    NetworkingMessage[] netMessages = new NetworkingMessage[maxMessages];
    StatusCallback status;
    uint listenSocket;
    void Awake()
    {
        Valve.Sockets.Library.Initialize();
        server = new NetworkingSockets();
        address = new Address();
        address.SetAddress("71.183.225.200", (ushort)10);
        listenSocket = server.CreateListenSocket(address);
        InitCallbacks();
        StartCoroutine(HandleServer());
    }

    void InitCallbacks()
    {
        status = (info, context) => {
            switch (info.connectionInfo.state) {
                case ConnectionState.None:
                    break;

                case ConnectionState.Connecting:
                    server.AcceptConnection(info.connection);
                    break;

                case ConnectionState.Connected:
                    Debug.Log("Client connected - ID: " + info.connection + ", IP: " + info.connectionInfo.address.GetIP());
                    break;

                case ConnectionState.ClosedByPeer:
                    server.CloseConnection(info.connection);
                    Debug.Log("Client disconnected - ID: " + info.connection + ", IP: " + info.connectionInfo.address.GetIP());
                    break;
            }
        };
    }

    IEnumerator HandleServer()
    {
        while(true)
        {
            server.DispatchCallback(status);

            int netMessagesCount = server.ReceiveMessagesOnListenSocket(listenSocket, netMessages, maxMessages);

            if (netMessagesCount > 0) 
            {
                for (int i = 0; i < netMessagesCount; i++) 
                {
                    // ref NetworkingMessage netMessage = ref netMessages[i];
                    NetworkingMessage netMessage = netMessages[i];

                    Debug.Log("Message received from - ID: " + netMessage.connection + ", Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);

                    netMessage.Destroy();
                }
            }

            Thread.Sleep(15);
            yield return null;
        }
    }
}
