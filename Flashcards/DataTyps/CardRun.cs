using System.Collections.Generic;

namespace Flashcards.DataTyps
{
    /// <summary>
    /// Klasse für die Auswertung eines Durchlaufs der Karten, enthält eine Liste mit den Ids,
    /// der zu verwendenen Karten (Int64)
    /// </summary>
    public class CardRun
    {
        public List<Card> Cards;
        public List<Card> UsedCards;
        public string CategoryId;
        public bool CardFlippingOn = false;
        public bool PagesOn = false;
        public int Counter = 0;
        public int CardsManaged = 0;
        public int EasyGraded = 0;
        public int NormalGraded = 0;
        public int HardGraded = 0;
        public int Unsolved = 0;
    }
}
