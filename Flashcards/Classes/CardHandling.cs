using System;
using System.Collections.Generic;
using System.Linq;
using Flashcards.DataTyps;
using Flashcards.Classes;

namespace Flashcards
{
    public class CardHandling
    {
        List<Card> Cards;
        Category Subject;
        int Stack;

        List<Card> SelectedCards;

        public CardHandling(List<Card> cards, Category category)
        {
            CardComparer comparer = new CardComparer();
            cards.Sort(comparer);
            Cards = cards;
            Subject = category;
            SelectedCards = new List<Card>();
        }

        /// <summary>
        /// Selektiert einen Stapel aus Karten und berücksichtigt dabei die in den Einstellungen vorgegebene maximale Anzahl an Karten pro Durchlauf
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private List<Card> SelectBySetting()
        {
            Stack = Subject.CardAmountPerSession;
            if (Cards.Count > Stack)
            {
                List<Card> restCards = new List<Card>();  
                restCards = AmountOfNewCards(Stack);
                int restamount = Stack - SelectedCards.Count();
                DivideStack(restamount, restCards);
            }
            else
            {
                SelectedCards = Cards;
            }
            return SelectedCards;
        }

        /// <summary>
        /// Funktion die Karten für einen Durchlauf selektiret und deren Ids zurückliefert
        /// </summary>
        /// <returns></returns>
        public List<Card> GetCards()
        {            
            return SelectBySetting();
        }

        /// <summary>
        /// Holt alle neuen Karten die für einen Kartendurchlauf genutzt werden können,
        /// die Anzahl der neuen Karten darf maximal die Hälfte der gesammten Karten betragen
        /// </summary>
        /// <param name="stack">Anzahl Karten die für einen Durchlauf genutzt werden sollen</param>
        /// <param name="cards">Liste für die Karten die für den Durchlauf genutzt werden sollen</param>
        /// <returns>Liefert die Ids der Überschüssigen neuen Karten zurück</returns>
        private List<Card> AmountOfNewCards(int stack)
        {
            List<Card> openCards = new List<Card>();
            int maxNewCards = stack / 2;
            List<Card> allNewCard = Cards.Where(x => x.Grading == (byte)EnumCardGrading.Unused).ToList();
            int newCards = allNewCard.Count();

            if (newCards > maxNewCards)
            {
                //int gap = stack - maxNewCards;
                for (int i = 0; i < maxNewCards; i++)
                {
                    SelectedCards.Add(allNewCard[0]);
                    allNewCard.RemoveAt(0);
                }
                openCards.AddRange(allNewCard);
            }
            else
            {
                SelectedCards = allNewCard;
            }
            return openCards;
        }

        /// <summary>
        /// Setzt fest wie viele Karten in welcher Schwirigkeit verteilt werden sollen
        /// </summary>
        /// <param name="stack"></param>
        private void DivideStack(int stack, List<Card> openNormalCards)//open unused cards
        {
            
            List<Int64> ids = new List<long>();

            int hardCards = 0;
            int easyCards = 0;
            int normalCards = 0;
            
            hardCards = normalCards = (stack * 40) / 100;
            easyCards = (stack * 20) / 100;
            int restCards = stack - hardCards - normalCards - easyCards;
            normalCards += restCards;

            GetCardsByDevision(easyCards, normalCards, hardCards, openNormalCards, stack);
        }

        /// <summary>
        /// Selektiert Karten Ids, abhängig von ihrer Schwierigkeit und in abhängigkeit zu einer gewünschten Anzahl
        /// </summary>
        /// <param name="grading">Gibt den Schwierigkeitsgrad an nach dem selektiert werden soll</param>
        /// <param name="amount">gibt an wie viele Karten dem schwierigkeitsgrad zugeordnet werden sollen</param>
        /// <param name="list">gibt eine Liste an CardStates an aus der selektiert werden soll</param>
        /// <param name="ids">Liste mit CardIs denen weitere hinzugefügt werden sollen</param>
        /// <returns>Menge der Karten die nach Abzug von amount noch für grading zur verfügung stehen</returns>
        private List<Card> GetCardsByDifficulty(EnumCardGrading grading, int amount)
        {
            List<Card> cardsWithSameGrading = Cards.Where(x => x.Grading == (Byte)grading).ToList();
            List<Card> openCards = new List<Card>();
            if(cardsWithSameGrading.Count() >= amount)
            {
                SelectedCards.AddRange(cardsWithSameGrading.GetRange(0, amount));
                cardsWithSameGrading.RemoveRange(0, amount);
            }
            else
            {
                SelectedCards.AddRange(cardsWithSameGrading);
                cardsWithSameGrading = new List<Card>();
            }
            openCards.AddRange(cardsWithSameGrading);
            return openCards;
        }

        /// <summary>
        /// Füllt den Kartenstapel mit Karten der verschiedenen Schwirigkeitsstufen,
        /// versucht dabei die erhalltene Verteilung beizubehalten. Sind nicht genügend Karten für einen
        /// Schwierigkeitsgrad vorhanden, wird die fehlende Menge durch Restkarten der anderen Schwirigkeitsgrade aufgefüllt
        /// </summary>
        /// <param name="easy"></param>
        /// <param name="normal"></param>
        /// <param name="hard"></param>
        /// <param name="openCards"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private void GetCardsByDevision(int easy, int normal, int hard, List<Card> openCards, int amount)
        {
            openCards.AddRange(GetCardsByDifficulty(EnumCardGrading.Easy, easy));
            openCards.AddRange(GetCardsByDifficulty(EnumCardGrading.Normal, normal));
            openCards.AddRange(GetCardsByDifficulty(EnumCardGrading.Hard, hard));

            int missingCards = Stack - SelectedCards.Count();
            if(missingCards > 0)
            {
                SelectedCards.AddRange(openCards.GetRange(0, missingCards));
            }
        }
    }
}
