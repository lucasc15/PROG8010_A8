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
        public ObservableCollection<int> sectionData { get; set; } 
        public string sectionName { get; set; }

        public Model()
        {
            sectionData = new ObservableCollection<int>();
        }
    }
}
