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
        // CONSTANT: for the label for the average of all sections
        string kOverallAverageLabel = "Overall";
        // PROPERTY: ModelLoader loads text files from a specified directory
        ModelLoader modelLoader = new ModelLoader();
        // PROPERTY: This property stores a lists of models (model.sectionName, model.sectionData) and binds it to a single ListContorl
        private ObservableCollection<Model> _marks;
        public ObservableCollection<Model> marks
        {
            get { return _marks; }
            set { _marks = value; OnPropertyChanged(); }
        }
        // PROPERTY: This stores an observable list of the calculated marks
        private ObservableCollection<Averages> _sectionAverages = new ObservableCollection<Averages>();
        public ObservableCollection<Averages> sectionAverages
        {
            get { return _sectionAverages; }
            set { _sectionAverages = value;  OnPropertyChanged(); }
        }
        // PROPERTY: stores the maxScore among all sections
        private int _maxScore;
        public int maxScore
        {
            get { return _maxScore; }
            set { _maxScore = value;  OnPropertyChanged(); }
        }
        // PROPERTY: Stores the section the max mark occured in
        private string _maxScoreSection;
        public string maxScoreSection
        {
            get { return _maxScoreSection; }
            set { _maxScoreSection = value;  OnPropertyChanged(); }
        }
        // PROPERTY: Stores the minimum mark
        private int _minScore;
        public int minScore
        {
            get { return _minScore; }
            set { _minScore = value; OnPropertyChanged(); }
        }
        // PROPERTY: Stores the mark the minimum score occurred in
        private string _minScoreSection;
        public string minScoreSection
        {
            get { return _minScoreSection; }
            set { _minScoreSection = value; OnPropertyChanged(); }
        }
        // This function loads the data from the directory selects in the MainWindow.xaml.cs file
        public void LoadData(string dataDirectory)
        {
            marks = modelLoader.LoadData(dataDirectory);
            findHighestGrade();
            findLowestGrade();
            findAverages();
        }
        private void findHighestGrade()
        {
            // This is a function that finds the maximum value among all models in the marks property
            int maxValue = -1000000000;
            string maxSection = "";
            // Iterate over each model in a list of the marks
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
            // Finds the lowest grade in all of the Models in the marks property
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
            // function to calculate the averages of each section
            double weight = 0;
            double sum = 0;
            _sectionAverages.Clear();
            // Iterate over each model. The Averages class is local and stores the average as well as the section name
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
            // Weighted average formula for all Models/Sections
            overallAverage.sectionAverage = sum / weight;
            _sectionAverages.Add(overallAverage);
            sectionAverages = _sectionAverages;
        }
        private int findMaxValueIndex<T>(ObservableCollection<T> lst) where T: IComparable
        {
            // Generic function to compare max values in List<int> and List<float> to determine maximum value
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
            // Generic function to compare max values in List<int> and List<float> to determine minimum value
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
            // a class to store the section averages and name for a given Model/Section
            public double sectionAverage { get; set; }
            public string sectionName { get; set; }
        }
    }
}
