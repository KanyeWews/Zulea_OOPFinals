using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class FileManager
    {
        private List<string> lines = new List<string>();
        private string filePath = null;
        private bool status = false;

        public FileManager(string path)
        {
            filePath = path;
            status = Read();
        }

        public List<string> getLines() { return lines; }
        public bool getStatus() { return status; }

        public bool Read()
        {
            lines = new List<string>();
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                    return true;
                }

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading file: {e.Message}");
                return false;
            }
        }

        public void Write(List<string> content, bool append = true)
        {
            if (append)
            {
                foreach (string c in content)
                    lines.Add(c);
            }
            else
            {
                lines = content;
            }

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                foreach (string line in lines)
                    sw.WriteLine(line);
            }
        }
    }
}