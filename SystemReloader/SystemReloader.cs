using Brutal.ImGuiApi;
using KSA;
using StarMap.API;
using System.Diagnostics;
using System.Text.Json;

namespace SystemReloader
{


    [StarMapMod]
    public class SystemReloader
    {
        private class SystemData
        {
            public string name { get; set; }
        }

        [StarMapAfterGui]
        public void guiInit(double dt)
        {
            ImGui.Begin("System Reloader");
            if(ImGui.Button("Reload System"))
            {
                Console.WriteLine("Reloading game");

                SystemData systemData = new SystemData { name = "test" };

                // write to config.json
                string jsonString = JsonSerializer.Serialize(systemData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("C:\\Users\\joost\\Documents\\config.json", jsonString);
                Console.WriteLine("JSON config saved!");


                string exePath = Process.GetCurrentProcess().MainModule.FileName;

                var startInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    WorkingDirectory = Path.GetDirectoryName(exePath), // Put the working directory back to StarMap
                    UseShellExecute = true
                };

                Process.Start(startInfo);
                Program.Instance.RequestExit(0);
            }
            ImGui.End();
        }
    }
}
