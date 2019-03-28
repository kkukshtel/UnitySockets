using UnityEngine;
using Valve.Sockets;
using System.Collections;
using System.Threading;

public class ClientExample : MonoBehaviour
{
    NetworkingSockets client;
    StatusCallback status;
    Address address;
    static uint connection;
    const int maxMessages = 20;
    NetworkingMessage[] netMessages = new NetworkingMessage[maxMessages];
    DebugCallback debug = (type, message) => {
	    Debug.Log("Debug - Type: " + type + ", Message: " + message);
    };
    NetworkingUtils utils = new NetworkingUtils();
    ushort clientPort = 27200;
    void Awake()
    {
        utils.SetDebugCallback(DebugType.Everything, debug);
        client = new NetworkingSockets();
        address = new Address();
        address.SetAddress("::1", clientPort); //ipv6 localhost
        connection = client.Connect(address);

        InitCallbacks();
        StartCoroutine(HandleClient());
    }

    void InitCallbacks()
    {
        status = (info, context) => {
            switch (info.connectionInfo.state) {
                case ConnectionState.None:
                    break;

                case ConnectionState.Connected:
                    Debug.Log("Client connected to server - ID: " + connection);
                    break;

                case ConnectionState.ClosedByPeer:
                    client.CloseConnection(connection);
                    Debug.Log("Client disconnected from server");
                    break;

                case ConnectionState.ProblemDetectedLocally:
                    client.CloseConnection(connection);
                    Debug.Log("Client unable to connect");
                    break;
            }
        };
    }

    IEnumerator HandleClient()
    {
        while(true)
        {
            client.DispatchCallback(status);

            int netMessagesCount = client.ReceiveMessagesOnConnection(connection, netMessages, maxMessages);

            if (netMessagesCount > 0) {
                for (int i = 0; i < netMessagesCount; i++) {
                    // ref NetworkingMessage netMessage = ref netMessages[i];
                    NetworkingMessage netMessage = netMessages[i];

                    Debug.Log("Message received from server - Channel ID: " + netMessage.channel + ", Data length: " + netMessage.length);

                    netMessage.Destroy();
                }
            }

            Thread.Sleep(15);
            yield return null;
        }
    }

    public bool test = false;
    void Update()
    {
        if(test)
        {
            Test();
            test = false;
        }
    }

    void Test()
    {
        byte[] data = new byte[64];
        client.SendMessageToConnection(connection, data);
    }
}
