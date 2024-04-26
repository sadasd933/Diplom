using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WebSocketSharp;

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
            SendToServer(msg);
        }

        private void Results_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ResultsPage());
        }

        private void SendToServer(string message)
        {
            using (WebSocketSharp.WebSocket webSocket = new WebSocketSharp.WebSocket(
                 "ws://172.17.0.51:3000/"))  // write the URL of server to
                                                                  // connect to
            {
                webSocket.OnMessage += webSocket_OnMessage;  // message for the function below

                webSocket.Connect();                          // connection to the server
                webSocket.Send(message);  // sending a message to the server
            }
        }

        public static void webSocket_OnMessage(object viewer, MessageEventArgs a)  // function for server to respond
        {
            Console.WriteLine("Recived from the server " + a.Data);
        }
    }
}
