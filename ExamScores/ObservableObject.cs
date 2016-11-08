/*
 * Exam Scores
 * Assignment #8 PROG8010, Group 1
 * Author: Priya
 * Conestoga College
 * 
 * Group members:
 *      Oleksandr Rudavka
 *      Jonathan Nagata
 *      Niral Gandhi
 *      Tanmay Teckchandani
 *      Priya Tharuman
 *      Krishna Kanhaiya
 *      Lucas Currah
 * 
 * 7 Nov, 2016
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExamScores
{
    class ObservableObject: INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
