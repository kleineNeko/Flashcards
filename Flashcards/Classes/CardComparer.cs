using System.Collections.Generic;
using Flashcards.DataTyps;

namespace Flashcards.Classes
{
    public class CardComparer : IComparer<Card>
    {
        /// <summary>
        /// Sortierung der Kartenbewertung nach der Einstufung ihrer Schwierigkeit, 
        /// der Häufigkeit ihrer Nutzung und dem Wert (der sich aus den Kartendurchläufen berechnet)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Card x, Card y)
        {
            int result = x.Grading.CompareTo(y.Grading);
            if(result == 0)
            {
                int value = x.CardValue.CompareTo(y.CardValue);
                int timesUsed = x.Counter.CompareTo(y.Counter);
                if(timesUsed == 0)
                {
                    value = (value == 1 && value != 0) ? -1 : 1;
                    return value;
                }
                if(value == 0)
                {
                    timesUsed = (timesUsed == 1 && timesUsed != 0) ? -1 : 1;
                    return timesUsed;
                }

                int difValue = (value == 1) ? (x.CardValue - y.CardValue) : (y.CardValue - x.CardValue);
                int difTimesUsed = (timesUsed == 1) ? (x.Counter - y.Counter) : (y.Counter - x.Counter);

                if(difValue < difTimesUsed)
                {
                    timesUsed = (timesUsed == 1 && timesUsed != 0) ? -1 : 1;
                    return timesUsed;
                }
                if(difValue > difTimesUsed)
                {
                    value = (value == 1 && value != 0) ? -1 : 1;
                    return value;
                }
            }
            result = (result == 1 && result != 0) ? -1 : 1;
            return result;
        }
    }
}
