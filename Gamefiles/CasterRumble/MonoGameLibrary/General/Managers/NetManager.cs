using LiteNetLib;
using MonoGameLibrary.General.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Managers
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        public Authority Authority { get; private set; }

        private EventBasedNetListener Listener { get; set; }
        private NetManager Netmanager { get; set; }


        public NetworkManager()
        {
            Authority = Authority.None;
            Listener = new EventBasedNetListener();
            Netmanager = new NetManager(Listener);
        }

        public void CreateServer(int _Port, int _MaxPlayers, string _key)
        {
            Authority = Authority.Server;
            Netmanager.Start(_Port);

            Listener.ConnectionRequestEvent += request =>
            {
                if (Netmanager.ConnectedPeersCount < _MaxPlayers /* max connections */)
                    request.AcceptIfKey(_key);
                else
                    request.Reject();

                while (!Console.KeyAvailable)
                {
                    Netmanager.PollEvents();   // process incoming/outgoing packets
                    Thread.Sleep(15);      // ~66 polls/sec, avoids CPU spinning
                }
            };
        }

        public void CreateClient(string _IP, int _Port,string _Key)
        {
            Authority = Authority.Client;
            Netmanager.Start();
            Netmanager.Connect(_IP, _Port, _Key);
            Listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod, channel) =>
            {
                Console.WriteLine("We got: {0}", dataReader.GetString(100 /* max length of string */));
                dataReader.Recycle();
            };

        }

        public void stop()
        {
            Netmanager.Stop();
            Authority = Authority.None;
        }   

    }

    public enum Authority
    {
        Server,
        Client,
        None
    }
}
