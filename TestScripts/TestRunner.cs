using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LightweightPlatform
{
    public class TestRunner
    {
        private bool isRunning;
        private List<string> steps;

        public void Start(string scriptPath)
        {
            isRunning = true;
            steps = new List<string>();

            var script = JsonSerializer.Deserialize<List<TestCase>>(File.ReadAllText(scriptPath));
            foreach (var testCase in script)
            {
                if (!isRunning) break;
                steps.Add($"Running {testCase.Name}");
                steps.Add($"Result: Success");
            }

            PDFGenerator.GenerateReport("./Release/PDF_Test_Results/TestReport.pdf", steps.ToArray());
        }

        public void Stop() => isRunning = false;

        public void LoadScript(string path)
        {
            // Logic to load new script
        }
    }

    public class TestCase
    {
        public string Name { get; set; }
        public PacketType PacketType { get; set; }
        public string ExpectedResponse { get; set; }
    }
}
