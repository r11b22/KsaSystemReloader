using Brutal.ImGuiApi;
using StarMap.API;
using KSA;
using System.Diagnostics;

namespace SystemReloader
{
    [StarMapMod]
    public class SystemReloader
    {
        [StarMapAfterGui]
        public void guiInit(double dt)
        {
            ImGui.Begin("System Reloader");
            if(ImGui.Button("Reload System"))
            {
                Console.WriteLine("Reloading game");

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
