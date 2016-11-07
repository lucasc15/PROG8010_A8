using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExamScores
{
    class ModelLoader
    {
        // Class that loads all sections in a list of models
        // All text files in the specified folder are attempted to be loaded
        public ObservableCollection<Model> marks;
        public ObservableCollection<Model> LoadData(string directory)
        {
            // Loads all the files in specified directory
            marks = new ObservableCollection<Model>();
            string line;
            string[] dataFiles = Directory.GetFiles(directory, "*.txt"); // an array of all text files in the directory
            // iterate over all the datafiles (i.e. dataFiles = string[]{".../section1.txt", ..., ".../section3.txt"}
            for (int i=0; i < dataFiles.Length; i++)
            {
                Model m = new Model();
                m.sectionName = getSectionName(dataFiles[i]);
                // open a file connection
                using (System.IO.StreamReader f =
                    new System.IO.StreamReader(dataFiles[i]))
                {
                    // iterate over lines until end of file (i.e. ReadLine() returns null)
                    while ((line = f.ReadLine()) != null)
                    {
                        m.sectionData.Add(int.Parse(line));
                    }
                }
                marks.Add(m);
            }
            return marks;
        }
        private string getSectionName(string fileName)
        {
            return Path.GetFileNameWithoutExtension(
                fileName);
        }
    }
}
