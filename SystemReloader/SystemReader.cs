using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SystemReloader
{
    internal class SystemReader
    {
        private string m_tempPath;

        private SystemData? m_systemData;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the temp file where the data was stored</param>
        public SystemReader(string name)
        {
            m_tempPath = Path.Combine(Path.GetTempPath(), name);


            if (!File.Exists(m_tempPath))
            {
                // No temp file found, nothing to read
                return;
            }

            string jsonString = File.ReadAllText(m_tempPath);
            m_systemData = JsonSerializer.Deserialize<SystemData>(jsonString);

            if(m_systemData == null)
            {
                File.Delete(m_tempPath);
                throw new Exception("Failed to deserialize system data from " + m_tempPath);
            }

            File.Delete(m_tempPath);
            Console.WriteLine("JSON config read and delted from: " + m_tempPath);
        }

        public string getSystemName()
        {
            return m_systemData!.name;
        }

        public bool wasReloaded()
        {
            return m_systemData != null;
        }
    }
}
