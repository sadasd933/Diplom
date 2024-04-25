using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QualificationTest
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        bool loginConfirmed = false;
        public Authorization()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (ClientWebSocket client = new ClientWebSocket())
            {
                Uri serviceUri = new Uri("ws://localhost:5000/send");
                var cTs = new CancellationTokenSource();
                cTs.CancelAfter(TimeSpan.FromSeconds(120));
                try
                {
                    await client.ConnectAsync(serviceUri, cTs.Token);
                    while (client.State == WebSocketState.Open)
                    {
                        string login = loginTB.Text.ToString();
                        string password = passwordTB.Password.ToString();
                        if(!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                        {
                            ArraySegment<byte> byteToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(login));
                            await client.SendAsync(byteToSend, WebSocketMessageType.Text, true, cTs.Token);
                            var responseBuffer = new byte[1024];
                            var offset = 0;
                            var packet = 1024;
                            while (true)
                            {
                                ArraySegment<byte> byteReceived = new ArraySegment<byte>(responseBuffer, offset, packet);
                                WebSocketReceiveResult response = await client.ReceiveAsync(byteReceived, cTs.Token);
                                var responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);
                                clientTB.Text = responseMessage.ToString();
                                if (response.EndOfMessage)
                                    break;
                            }


                            ArraySegment<byte> byteToSend1 = new ArraySegment<byte>(Encoding.UTF8.GetBytes(password));
                            await client.SendAsync(byteToSend1, WebSocketMessageType.Text, true, cTs.Token);
                            var responseBuffer1 = new byte[1024];
                            var offset1 = 0;
                            var packet1 = 1024;
                            while (true)
                            {
                                ArraySegment<byte> byteReceived1 = new ArraySegment<byte>(responseBuffer1, offset1, packet1);
                                WebSocketReceiveResult response1 = await client.ReceiveAsync(byteReceived1, cTs.Token);
                                var responseMessage1 = Encoding.UTF8.GetString(responseBuffer1, offset1, response1.Count);
                                clientTB.Text = responseMessage1.ToString();
                                if (response1.EndOfMessage)
                                    break;
                            }
                        }
                    }
                }
                catch (WebSocketException ex)
                {
                    clientTB.Text = ex.Message.ToString();

                }
            }
            if (loginConfirmed)
            {
                NavigationService.Navigate(new MainProgram());
            }
        }
    }
}
