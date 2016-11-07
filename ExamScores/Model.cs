using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamScores
{
    class Model
    {
        // Model to store data for a section: both the section name and a list of marks for that section
        public ObservableCollection<int> sectionData { get; set; } 
        public string sectionName { get; set; }

        public Model()
        {
            sectionData = new ObservableCollection<int>();
        }
    }
}
