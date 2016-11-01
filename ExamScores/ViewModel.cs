using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*using System.Collections.Generic;*/

namespace ExamScores
{
    class ViewModel: ObservableObject
    {
        string kOverallAverageLabel = "Overall";
        ModelLoader modelLoader = new ModelLoader();
        private ObservableCollection<Model> _marks;
        public ObservableCollection<Model> marks
        {
            get { return _marks; }
            set { _marks = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Averages> _sectionAverages = new ObservableCollection<Averages>();
        public ObservableCollection<Averages> sectionAverages
        {
            get { return _sectionAverages; }
            set { _sectionAverages = value;  OnPropertyChanged(); }
        }
        private int _maxScore;
        public int maxScore
        {
            get { return _maxScore; }
            set { _maxScore = value;  OnPropertyChanged(); }
        }
        private string _maxScoreSection;
        public string maxScoreSection
        {
            get { return _maxScoreSection; }
            set { _maxScoreSection = value;  OnPropertyChanged(); }
        }
        private int _minScore;
        public int minScore
        {
            get { return _minScore; }
            set { _minScore = value; OnPropertyChanged(); }
        }
        private string _minScoreSection;
        public string minScoreSection
        {
            get { return _minScoreSection; }
            set { _minScoreSection = value; OnPropertyChanged(); }
        }
        public void LoadData(string dataDirectory)
        {
            marks = modelLoader.LoadData(dataDirectory);
            findHighestGrade();
            findLowestGrade();
            findAverages();
        }
        private void findHighestGrade()
        {
            int maxValue = -1000000000;
            string maxSection = "";
            foreach (Model section in _marks)
            {
                int idx = findMaxValueIndex(section.sectionData);
                if (section.sectionData[idx] > maxValue)
                {
                    maxValue = section.sectionData[idx];
                    maxSection = section.sectionName;
                }
            }
            maxScore = maxValue;
            maxScoreSection = maxSection;
        }
        private void findLowestGrade()
        {
            int minValue = 1000000000;
            string minSection = "";
            foreach (Model section in _marks)
            {
                int idx = findMinValueIndex(section.sectionData);
                if (section.sectionData[idx] < minValue)
                {
                    minValue = section.sectionData[idx];
                    minSection = section.sectionName;
                }
            }
            minScore = minValue;
            minScoreSection = minSection;
        }
        private void findAverages()
        {
            double weight = 0;
            double sum = 0;
            _sectionAverages.Clear();
            foreach (Model section in _marks)
            {
                Averages average = new Averages();
                double avg = section.sectionData.Average();
                average.sectionAverage = avg;
                average.sectionName = section.sectionName;
                _sectionAverages.Add(average);
                weight += (double)section.sectionData.Count();
                sum += (double)section.sectionData.Count() * avg;
            }
            Averages overallAverage = new Averages();
            overallAverage.sectionName = kOverallAverageLabel;
            overallAverage.sectionAverage = sum / weight;
            _sectionAverages.Add(overallAverage);
            sectionAverages = _sectionAverages;
        }
        private int findMaxValueIndex<T>(ObservableCollection<T> lst) where T: IComparable
        {
            // Generic function to compare max values in List<int> and List<float>
            int maxIndex;
            if (lst.Count() < 1)
            {
                maxIndex = 0;
            }
            else {
                maxIndex = 0;
                T maxValue = lst[0];
                for (int i = 1; i < lst.Count(); i++)
                {
                    if (lst[i].CompareTo(maxValue) >= 0)
                    {
                        maxIndex = i;
                        maxValue = lst[i];
                    }
                }
            }
            return maxIndex;
        }
        private int findMinValueIndex<T>(ObservableCollection<T> lst) where T : IComparable
        {
            // Generic function to compare max values in List<int> and List<float>
            int minIndex;
            if (lst.Count() < 1)
            {
                minIndex = 0;
            }
            else
            {
                minIndex = 0;
                T minValue = lst[0];
                for (int i = 1; i < lst.Count(); i++)
                {
                    if (lst[i].CompareTo(minValue) <= 0)
                    {
                        minIndex = i;
                        minValue = lst[i];
                    }
                }
            }
            return minIndex;
        }
        public class Averages
        {
            public double sectionAverage { get; set; }
            public string sectionName { get; set; }
        }
    }
}
