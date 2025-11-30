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
        private SystemWriter? m_systemWriter;
        private SystemReader? m_systemReader;


        [StarMapBeforeMain]
        public void immediateLoad()
        {
            m_systemWriter = new SystemWriter("system_reload_temp.json");

            
        }

        [StarMapImmediateLoad]
        public void test(Mod mod)
        {
            m_systemReader = new SystemReader("system_reload_temp.json");

            

            if (m_systemReader.wasReloaded())
            {
                 GameSettings.Current.System.ConfigOnStart = false;
            }
        }

        [StarMapAfterGui]
        public void guiInit(double dt)
        {
            ImGui.Begin("System Reloader");
            if(ImGui.Button("Reload System"))
            {
                Console.WriteLine("Reloading game");
                if(m_systemWriter == null)
                {
                    throw new NullReferenceException("SystemWriter is null! This is an error contact the mod author to fix the issue!");
                }
                m_systemWriter.writeSystem(Universe.CurrentSystem.Id);

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
