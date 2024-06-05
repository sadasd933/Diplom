using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QualificationTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestSelectionPage.xaml
    /// </summary>
    public partial class TestSelectionPage : Page
    {
        string selectedTest;
        public TestSelectionPage()
        {
            InitializeComponent();
            testSelectionCB.Items.Add(1);
            testSelectionCB.Items.Add(2);
        }

        private void startTestButton_Click(object sender, RoutedEventArgs e)
        {
            selectedTest = testSelectionCB.SelectedItem.ToString();
            switch (selectedTest)
            {
                case "1":
                    AssignTestID(selectedTest);
                    break;
                case "2":
                    AssignTestID(selectedTest);
                    break;
                default:
                    MessageBox.Show("Попробуйте выбрать другой тест", "Возникла непредвиденная ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
        private void AssignTestID(string testToAssign)
        {
            Application.Current.Properties["testID"] = testToAssign;
            NavigationService.Navigate(new MainProgram());
        }
    }
}
