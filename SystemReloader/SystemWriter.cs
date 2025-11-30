using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SystemReloader
{

    internal class SystemWriter
    {
        private string m_tempPath;

        public SystemWriter(string name)
        {
            m_tempPath = Path.Combine(Path.GetTempPath(), name);
        }

        public void writeSystem(string systemName)
        {
            SystemData systemData = new SystemData { name = systemName };

            // write to config.json
            string jsonString = JsonSerializer.Serialize(systemData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(m_tempPath, jsonString);
            Console.WriteLine("JSON config saved To: " + m_tempPath);
        }
    }
}
