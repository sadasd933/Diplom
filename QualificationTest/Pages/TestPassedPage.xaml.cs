using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QualificationTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPassedPage.xaml
    /// </summary>
    public partial class TestPassedPage : Page
    {
        public string msg;
        public TestPassedPage()
        {
            InitializeComponent();
            string testerName = Application.Current.Properties["tester"].ToString();
            string numOfCorrectAnswers = Application.Current.Properties["corAnsCount"].ToString();

            msg = $"{testerName} прошёл тест и набрал {numOfCorrectAnswers} правильных ответов из 10!";
        }

        private void Results_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ResultsPage());
        }

        private async void SendToServer(string message)
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
                        if (!string.IsNullOrEmpty(message))
                        {
                            ArraySegment<byte> byteToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                            await client.SendAsync(byteToSend, WebSocketMessageType.Text, true, cTs.Token);
                            var responseBuffer = new byte[1024];
                            var offset = 0;
                            var packet = 1024;
                            while (true)
                            {
                                ArraySegment<byte> byteReceived = new ArraySegment<byte>(responseBuffer, offset, packet);
                                WebSocketReceiveResult response = await client.ReceiveAsync(byteReceived, cTs.Token);
                                var responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);
                                answersCount.Text = responseMessage.ToString();
                                if (response.EndOfMessage)
                                    break;
                            }
                        }
                    }
                }
                catch (WebSocketException ex)
                {
                    answersCount.Text = ex.Message.ToString();

                }
            }
        }

        private void SendDataToServer_Click(object sender, RoutedEventArgs e)
        {

            SendToServer(msg);
        }

    }
}
