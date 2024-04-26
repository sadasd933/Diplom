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
            var ws = new ClientWebSocket();

            await ws.ConnectAsync(new Uri("ws://localhost:5050/ws"), CancellationToken.None);
            byte[] buf = new byte[1056];

            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buf), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                    Console.WriteLine(result.CloseStatusDescription);
                }
                else
                {
                    Console.WriteLine(Encoding.ASCII.GetString(buf, 0, result.Count));
                }
            }
        }

        private void SendDataToServer_Click(object sender, RoutedEventArgs e)
        {

            SendToServer(msg);
        }

    }
}
