using System.Windows;

namespace LightweightPlatform
{
    public partial class MainWindow : Window
    {
        private TestRunner testRunner;

        public MainWindow()
        {
            InitializeComponent();
            testRunner = new TestRunner();
        }

        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {
            testRunner.Start("./TestScripts/TestScript.json");
        }

        private void StopTestButton_Click(object sender, RoutedEventArgs e)
        {
            testRunner.Stop();
        }

        private void LoadScriptButton_Click(object sender, RoutedEventArgs e)
        {
            testRunner.LoadScript("./TestScripts/NewTestScript.json");
        }
    }
}
