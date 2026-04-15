using LiteNetLib;
using Microsoft.Xna.Framework;
using MonoGameLibrary.General.Utility;
using System;
using System.Collections.Generic;
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

        public string Key { get; private set; } = string.Empty;

        private int MaxPlayers { get; set; }

        public NetworkManager()
        {
            Authority = Authority.None;
            Listener = new EventBasedNetListener();
            Netmanager = new NetManager(Listener);
        }

        public void CreateServer(int _Port, int _MaxPlayers)
        {
            Authority = Authority.Server;
            MaxPlayers = _MaxPlayers;
            Key = GenerateKey();
            System.Diagnostics.Debug.WriteLine(Key);
            Netmanager.Start(_Port);

            Listener.ConnectionRequestEvent += request =>
            {
                if (Netmanager.ConnectedPeersCount < MaxPlayers)
                    request.AcceptIfKey(Key);
                else
                    request.Reject();
            };

            Listener.PeerConnectedEvent += peer =>
            {
                System.Diagnostics.Debug.WriteLine($"Player connected: {peer.Address}");
            };
        }

        public void CreateClient(string _IP, int _Port, string _Key)
        {
            Authority = Authority.Client;
            Netmanager.Start();
            Netmanager.Connect(_IP, _Port, _Key);

            Listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod, channel) =>
            {
                dataReader.Recycle();
            };
        }

        public override void Update(GameTime gametime)
        {
            if (Authority == Authority.None) return;
            Netmanager.PollEvents();
        }

        public void Stop()
        {
            Netmanager.Stop();
            Authority = Authority.None;
            Key = string.Empty;
        }

        private string GenerateKey()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }

    public enum Authority
    {
        Server,
        Client,
        None
    }
}