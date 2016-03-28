//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.Entity;
//using Flashcards.DatabaseTyps;
//using System.Windows.Media.Imaging;

//namespace Flashcards
//{
//    struct DatabaseInteraction
//    {
//        //FlashcardsContext context;

//        private void GetException(Exception ex)
//        {/*
//            ExceptionHandling exHandling = new ExceptionHandling("Es ist ein Fehler in Verbindung mit der Datenbank aufgetreten.", 
//                "DatabaseInteraction", 
//                DateTime.Now, 
//                ex.Message, 
//                ex.TargetSite.ToString(), 
//                ex.StackTrace);*/
//        }

//        #region Article
//        /// <summary>
//        /// Legt die einzelnen Artikel die zu einer Seite gehören an, der Schlüssel der Seite 
//        /// muss dabei an jedes Item angehängt werden
//        /// </summary>
//        /// <param name="articles"></param>
//        /// <param name="pageId"></param>
//        private void SaveArticles(List<Article> articles, int pageId)
//        {
//            foreach (Article item in articles)
//            {
//                item.PageId = pageId;
//                try
//                {
//                    using (var context = new FlashcardsContext())
//                    {
//                        context.Articles.Add(item);
//                        context.SaveChanges();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    GetException(ex);
//                }
//            }
//        }

//        /// <summary>
//        /// Durchläuft alle Artikel und editiert sie 
//        /// </summary>
//        /// <param name="articles"></param>
//        private void EditArticles(List<Article> articles)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    foreach (Article item in articles)
//                    {
//                        Article oldItem = context.Articles.Single(n => n.Id == item.Id);
//                        oldItem = item;
//                        context.SaveChanges();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//        }

//        /// <summary>
//        /// Selektiert alle Artikel die zu einer Page gehören und gibt diese in einer Liste zurück
//        /// </summary>
//        /// <param name="pageId"></param>
//        /// <returns></returns>
//        public List<Article> GetArticles(int pageId)
//        {
//            List<Article> articles = new List<Article>();
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    articles = context.Articles.Where(n => n.PageId == pageId).ToList();
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//            return articles;
//        }

//        /// <summary>
//        /// Löscht alle Artikel die zu einem Page-Objekt gehören
//        /// </summary>
//        /// <param name="pageId"></param>
//        private void DeleteArticles(int pageId)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    List<Article> items = new List<Article>();
//                    items = context.Articles.Where(n => n.PageId == pageId).ToList();
//                    context.Articles.RemoveRange(items);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//        }

//        #endregion

//        #region Card
//        /// <summary>
//        /// Zählt die Anzahl Karten die einer bestimmten Kategorie zugeordnet sind
//        /// </summary>
//        /// <param name="subjectId">Id der Kategorie deren Karten gezählt werden sollen</param>
//        /// <returns>Anzahl der Karten als Integer</returns>
//        public int AmountOfCards(int subjectId)
//        {
//            int carts = 0;
//            try {
//                using (var context = new FlashcardsContext())
//                {
//                    carts = context.Cards.Count(n=> n.SubjectId == subjectId);
//                }
//            }
//            catch(Exception ex) 
//            {
//                GetException(ex);
//            }
//            return carts;
//        }

//        /// <summary>
//        /// Selektiert alle Karten-Objekte die einer bestimmten Kategorie angehören und gibt diese zurück
//        /// </summary>
//        /// <param name="subjectId"></param>
//        /// <returns></returns>
//        public List<Card> GetCardsBySubject(int subjectId)
//        {
//            List<Card> cards = new List<Card>();
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    cards = context.Cards.Where(n => n.SubjectId == subjectId).ToList();
//                }
//            }
//            catch(Exception ex) 
//            {
//                GetException(ex);
//            }
//            return cards;
//        }

//        /// <summary>
//        /// gibt eine Liste mit KartenIds zurück, die zu einer Kategorie gehören
//        /// </summary>
//        /// <param name="subjectId"></param>
//        /// <returns></returns>
//        public List<Int64> GetCardIds(int subjectId)
//        {
//            List<Int64> ids = new List<Int64>();
//            try
//            {
//                using(var context = new FlashcardsContext()){
//                    ids = context.Cards.Where(n=> n.SubjectId == subjectId).Select(n=> n.CardId).ToList();
//                }
//            }
//            catch(Exception ex) 
//            {
//                GetException(ex);
//            }
//            return ids;
//        }

//        /// <summary>
//        /// Gibt ein einzelnes Karten-Objekt zurück 
//        /// </summary>
//        /// <param name="cardId">Id der Karte</param>
//        /// <returns></returns>
//        public Card GetCard(Int64 cardId)
//        {
//            Card card = new Card();
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    card = context.Cards.Single(n => n.CardId == cardId);
//                }
//            }
//            catch(Exception ex) {
//                GetException(ex);
//            }
//            return card;
//        }

//        /// <summary>
//        /// Speichert einen Karten-Datensatz und fügt diesem zuvor noch eine Id in Form eines DateTime.Now Objektes hinzu
//        /// </summary>
//        /// <param name="card"></param>
//        /// <returns></returns>
//        public bool SaveCard(Card card)
//        {
//            bool saved = false;
//            try
//            {
//                if (card.SubjectId != 0)
//                {
//                    using (var context = new FlashcardsContext())
//                    {
//                        context.Cards.Add(card);
//                        context.SaveChanges();                        
//                        Card savedCard = context.Cards.SingleOrDefault(n => n.Question.Equals(card.Question) && n.Answer.Equals(card.Answer) && n.SubjectId == card.SubjectId);
//                        if (savedCard != null)
//                        {
//                            bool stateSaved = SaveCardState(savedCard.CardId, savedCard.SubjectId);
//                            if (stateSaved == true)
//                            {
//                                saved = true;
//                            }
//                        }
//                    }
//                }                
//            }
//            catch (Exception ex) { GetException(ex); }
//            return saved;
//        }

//        /// <summary>
//        /// Löscht eine Karte mit ihrem zugehörigem Status
//        /// </summary>
//        /// <param name="cardId"></param>
//        public void DeleteCard(Int64 cardId)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {                    
//                    Card item = context.Cards.Single(n => n.CardId == cardId);
//                    DeleteCardState(cardId);
//                    context.Cards.Remove(item);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//        }

//        /// <summary>
//        /// Überträgt Änderungen an einem Card-Objekt in die Datenbank
//        /// </summary>
//        /// <param name="card"></param>
//        /// <returns></returns>
//        public bool UpdateCard(Card card)
//        {
//            bool saved = false;
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    Card item = context.Cards.Single(n => n.CardId == card.CardId);
//                    item.Question = card.Question;
//                    item.QPicture = card.QPicture;
//                    item.Answer = card.Answer;
//                    item.APicture = card.APicture;
//                    //zugeordnete Page noch nicht mit eingebunden
//                    //context.Cards.Add(item);
//                    context.SaveChanges();
//                    ResetCardState(card.CardId);
//                    saved = true;
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//            return saved;
//        }

//        /// <summary>
//        /// Hebt die Verknüpfung einer Karte mit einem bestimmtem Artikel auf
//        /// </summary>
//        /// <param name="pageId"></param>
//        private void RemovePageLinking(int pageId)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    List<Card> items = new List<Card>();
//                    items = context.Cards.Where(n => n.PageId == pageId).ToList();
//                    foreach (Card card in items)
//                    {
//                        card.PageId = null;
//                    }
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }

//        }
//        #endregion

//        #region CardState

//        /// <summary>
//        /// Legt einen neuen Kartenstatus an
//        /// </summary>
//        /// <param name="cardId"></param>
//        /// <returns></returns>
//        private bool SaveCardState(Int64 cardId, int subjectId)
//        {
//            bool saved = false;
//            CardState state = new CardState();
//            state.CardId = cardId;
//            state.SubjectId = subjectId;
//            state.CardValue = 0;
//            state.Counter = 0;
//            state.Grading = 0;
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    context.CardStates.Add(state);
//                    context.SaveChanges();
//                    Int64 id = 0;
//                    id = context.CardStates.SingleOrDefault(n => n.CardId == cardId).CardId;
//                    if (id == cardId)
//                    {
//                        saved = true;
//                    }
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//            return saved;
//        }

//        /// <summary>
//        /// Updatet einen Kartenstatus
//        /// </summary>
//        /// <param name="state"></param>
//        public void EditCardState(CardState state)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    CardState oldState = context.CardStates.Single(n => n.CardId == state.CardId);
//                    oldState.Counter = state.Counter;
//                    oldState.CardValue = state.CardValue;
//                    oldState.Grading = state.Grading;
//                    //context.CardStates.Add(oldState);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//        }

//        /// <summary>
//        /// Selektiert einen CardState-Datensatz abhängig von der übergebenen KartenId
//        /// </summary>
//        /// <param name="cardId"></param>
//        /// <returns></returns>
//        public CardState GetCardState(Int64 cardId)
//        {
//            CardState state = new CardState();
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    state = context.CardStates.Single(n => n.CardId == cardId);
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//            return state;
//        }

//        /// <summary>
//        /// Löscht den Status einer Karte 
//        /// </summary>
//        /// <param name="cardId"></param>
//        /// <param name="subjectId"></param>
//        private void DeleteCardState(Int64 cardId)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    CardState item = context.CardStates.Single(n => n.CardId == cardId);
//                    context.CardStates.Remove(item);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//        }

//        /// <summary>
//        /// Setzt den Status einer Karte zurück
//        /// </summary>
//        /// <param name="cardId"></param>
//        private void ResetCardState(Int64 cardId)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    CardState item = context.CardStates.Single(n => n.CardId == cardId);
//                    item.Counter = 0;
//                    item.CardValue = 0;
//                    item.Grading = 0;
//                    //context.CardStates.Add(item);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//        }

//        /// <summary>
//        /// Gibt die Ids der Karten zurück deren Status auf "neu" gesetzt ist
//        /// </summary>
//        /// <param name="subjectId"></param>
//        /// <returns></returns>
//        public List<Int64> NewStatedCards(int subjectId)
//        {
//            List<Int64> newCards = new List<Int64>();
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    newCards = context.CardStates.Where(n => n.SubjectId == subjectId && n.Grading == 0).Select(n => n.CardId).ToList();
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//            return newCards;
//        }

        
//        /// <summary>
//        /// Gibt die Ids der Karten zurück deren Status auf "hard" oder "veryhard" gesetzt ist
//        /// </summary>
//        /// <param name="subjectId"></param>
//        /// <returns></returns>
//        //public List<CardState> GetSortedStatedCards(int subjectId)
//        //{
//        //    List<CardState> cardStates = new List<CardState>();
//        //    try
//        //    {
//        //        using (var context = new FlashcardsContext())
//        //        {
//        //            List<CardState> cards = new List<CardState>();
//        //            cards = context.CardStates.Where(n => n.SubjectId == subjectId && n.Grading != 0).ToList();
//        //            Flashcards.Classes.CardComparer comparer = new Flashcards.Classes.CardComparer();
//        //            cards.Sort(comparer);  //Sortierungsversuch  (Sortieren nach Value und Counter)
//        //            cardStates = cards; 
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        GetException(ex);
//        //    }
//        //    return cardStates;
//        //}
        
//        #endregion

//        #region Page

//        /// <summary>
//        /// Selektiert alle Page-Objekte die zu einer Kategorie gehören und lieft diese zurück
//        /// </summary>
//        /// <param name="subjectId">Id der Kategorie</param>
//        /// <returns></returns>
//        public List<Page> GetPagesBySubject(int subjectId)
//        {
//            List<Page> pages = new List<Page>();
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    pages = context.Pages.Where(n => n._SubjectId == subjectId).ToList();
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//            return pages;
//        }

//        /// <summary>
//        /// Editiert einen bestehenden Page-Datensatz mit allen seinen Artikeln
//        /// </summary>
//        /// <param name="pageName"></param>
//        /// <param name="pageId"></param>
//        /// <param name="list"></param>
//        /// <param name="subjectId"></param>
//        public void EditPage(string pageName, int pageId, List<Article> list)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    Page page = new Page();
//                    page = context.Pages.Single(n => n.PageId == pageId);
//                    page.Headline = pageName;
//                    context.SaveChanges();
//                }
//                EditArticles(list);
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }

//        }

//        /// <summary>
//        /// Speichert eine Page und die dazugeörigen Artikel (von außen auffrufbar) 
//        /// </summary>
//        /// <param name="pageName"></param>
//        /// <param name="subjectId"></param>
//        /// <param name="articles"></param>
//        /// <returns></returns>
//        public bool SavePage(string pageName, int subjectId, List<Article> articles)
//        {
//            bool saved = false;
//            int pageId = SavePageEntry(pageName, subjectId);
//            if (pageId != 0)
//            {
//                SaveArticles(articles, pageId);
//                saved = true;
//            }
//            return saved;
//        }

//        /// <summary>
//        /// Legt einen neuen Artikel in der Datenbank an
//        /// </summary>
//        /// <param name="pageName"></param>
//        /// <param name="subjectId"></param>
//        private int SavePageEntry(string pageName, int subjectId)
//        {
//            int pageId = 0;
//            if (subjectId > 0)
//            {
//                Page page = new Page();
//                page.Headline = pageName;
//                page._SubjectId = subjectId;

//                try
//                {                    
//                    using (var context = new FlashcardsContext())
//                    {
//                        if (PageExists(page) == false)
//                        {
//                            context.Pages.Add(page);
//                            context.SaveChanges();
//                            Page item = context.Pages.SingleOrDefault(n => n.Headline == page.Headline && n._SubjectId == page._SubjectId);
//                            if (item != null)
//                            {
//                                pageId = item.PageId;
//                            }
//                        }                       
//                    }
//                }
//                catch (Exception ex)
//                {
//                    GetException(ex);
//                }                
//            }
//            return pageId;
//        }

//        /// <summary>
//        /// Prüft ob der übergebene Artikel bereits existiert
//        /// </summary>
//        /// <param name="page"></param>
//        /// <returns>false wenn des die Überschrift in dieser Kategorie noch nicht gibt</returns>
//        private bool PageExists(Page page)
//        {
//            bool exists = true;
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    Page item = new Page();
//                    item = context.Pages.SingleOrDefault(n => n._SubjectId == page._SubjectId && n.Headline == page.Headline);
//                    if (item == null)
//                    {
//                        exists = false;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//            return exists;
//        }

//        /// <summary>
//        /// Fragt die Überschrift einer Page ab
//        /// </summary>
//        /// <param name="pageId"></param>
//        /// <returns></returns>
//        public Page GetPage(int pageId)
//        {
//            Page page = new Page();
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    page = context.Pages.SingleOrDefault(n=> n.PageId == pageId);
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//            return page;
//        }

//        /// <summary>
//        /// Löscht eine Page samt all ihrer zugeordneten Artikel, prüft ob die Seite an Karten angehängt wurde
//        /// und löscht die Verbindung.
//        /// </summary>
//        /// <param name="pageId"></param>
//        public void DeletePage(int pageId)
//        {
//            try
//            {
//                DeleteArticles(pageId);
//                RemovePageLinking(pageId);
//                using (var context = new FlashcardsContext())
//                {
//                    Page page = new Page();
//                    page = context.Pages.Single(n => n.PageId == pageId);
//                    context.Pages.Remove(page);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//        }

//        /// <summary>
//        /// Leitet die Löschung aller Artikel ein die zu einer Kategorie gehören
//        /// </summary>
//        /// <param name="subjectId"></param>
//        private void DeleteAllPages(int subjectId)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    List<Page> list = new List<Page>();
//                    list = context.Pages.Where(n => n._SubjectId == subjectId).ToList();
//                    foreach (Page item in list)
//                    {
//                        DeletePage(item.PageId);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//        }

//        #endregion

//        #region Setting
//        /// <summary>
//        /// Selektiert ein Setting Objekt abhängig von der angegebenen Id der Kategorie, für welche die Optionen gelten
//        /// </summary>
//        /// <param name="subjectId">Id der Kategorie</param>
//        /// <returns>Setting-Objekt</returns>
//        public Setting GetSetting(int subjectId)
//        {
//            Setting setting = new Setting();
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    setting = context.Settings.SingleOrDefault(n => n.SubjectId == subjectId);
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//            return setting;
//        }

//        /// <summary>
//        /// Speichert die Einstellungen für die Durcharbeitung der Karten einer Kategorie
//        /// </summary>
//        /// <param name="setting"></param>
//        /// <returns>true wenn der Speichervorgang erfolgreich war</returns>
//        private void SettingSave(Setting setting)
//        {
//            setting.ShowPage = true; //test
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    context.Settings.Add(setting);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//        }

//        /// <summary>
//        /// Editiert die Einstellungen für einen Kartendurchlauf 
//        /// </summary>
//        /// <param name="setting"></param>
//        public void EditSetting(Setting setting)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    Setting item = new Setting();
//                    item = context.Settings.Single(n => n.SubjectId == setting.SubjectId);
//                    item.RoleBothCardSides = setting.RoleBothCardSides;
//                    item.ShowPage = setting.ShowPage;
//                    item.CardAmountPerSession = setting.CardAmountPerSession;
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//        }


//        /// <summary>
//        /// Löscht einen Setting-Datensatz 
//        /// </summary>
//        /// <param name="subjectId"></param>
//        private void DeleteSetting(int subjectId)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    Setting item = context.Settings.Single(n => n.SubjectId == subjectId);
//                    context.Settings.Remove(item);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//        }
//        #endregion

//        #region Subject
//        /// <summary>
//        /// Legt einen neuen Datensatz für eine Kategorie in der Datenbank an
//        /// </summary>
//        /// <param name="subject"></param>
//        private void Subject_Save(Subject subject)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    Subject item = new Subject
//                    {
//                        Name = subject.Name,
//                        Picture = subject.Picture
//                    };
//                    context.Subjects.Add(item);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex) { GetException(ex); }            
//        }

//        /// <summary>
//        /// Prüft ob der Bezeichner für die Kategorie bereits in der Datenbank vorhanden ist oder nicht
//        /// </summary>
//        /// <param name="name">Kategorie-Bezeichner</param>
//        /// <returns>true wenn der Bezeichner bereits vergeben ist</returns>
//        private int SubjectExists(string name)
//        {
//            int id = 0;
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    int count = 0;
//                    count = context.Subjects.Count(n => n.Name == name);//SingleOrDefault(n=> n.Name == subject).Name;
//                    if (count != 0)
//                    {
//                        id = context.Subjects.Single(n => n.Name == name).SubjectId;
//                    }
//                }
//            }
//            catch (Exception ex) { GetException(ex); }           
//            return id;
//        }

//        /// <summary>
//        /// Aufzurufende Methode zur Speicherung eines Subject-Datensatzes und dazu gehörigen Setting-Datensatzes.
//        /// Leitet Validierungen und Typumvormungen ein bis zum Aufruf der Save_Methode
//        /// </summary>
//        /// <param name="name">Kategorie-Bezeichner</param>
//        /// <param name="picture"></param>
//        /// <returns>true, wenn der Speichervorgang erfolgreich war</returns>
//        public Subject CreateNewSubject(string name, BitmapImage picture)
//        {
//            byte[] pictureArray;
//            int id = SubjectExists(name);
//            Subject subject = new Subject();
//            subject.SubjectId = 0;

//            if (id == 0)
//            {                
//                subject.Name = name;
//                if (picture != null)
//                {
//                    BitmapImage pic = ImageHandling.ResizePicture(picture);
//                    pictureArray = ImageHandling.BitmapImageToByteArray(pic);
//                    subject.Picture = pictureArray;
//                }
//                Subject_Save(subject);
//                id = SubjectExists(name);
//                if (id != 0)
//                {
//                    subject.SubjectId = id;
//                    Setting setting = new Setting();
//                    setting.SubjectId = id;
//                    setting.ShowPage = false;
//                    setting.RoleBothCardSides = false;
//                    setting.CardAmountPerSession = 30;
//                    SettingSave(setting);
//                }
//            }
//            return subject;
//        }

//        /// <summary>
//        /// Selektiert ein einzelnes Subject-Objekt aus der Datenbank abhängig von seiner Id
//        /// </summary>
//        /// <param name="subjectId"></param>
//        /// <returns></returns>
//        public Subject GetSubject(int subjectId)
//        {
//            Subject item = new Subject();
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    item = context.Subjects.Single(n => n.SubjectId == subjectId);
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//            return item;
//        }

//        /// <summary>
//        /// Selektiert alle Subject-Datensätze 
//        /// </summary>
//        /// <returns></returns>
//        public List<Subject> ListOfSubjects()
//        {
//            List<Subject> list = new List<Subject>();
//            try {
//                using (var context = new FlashcardsContext())
//                {
//                    list = context.Subjects.ToList();
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//            return list;
//        }

//        /// <summary>
//        /// Löscht eine Kategorie samt ihrer Karten, CardStates und ihres Settings.
//        /// </summary>
//        /// <param name="id">Id der Kategorie</param>
//        public void DeleteSubject(int id)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    DeleteSetting(id);
//                    List<Card> cards = GetCardsBySubject(id);
//                    foreach (Card card in cards)
//                    {
//                        DeleteCard(card.CardId);
//                    }
//                    DeleteAllPages(id);
//                    Subject item = context.Subjects.Single(n => n.SubjectId == id);
//                    context.Subjects.Remove(item);
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex) { GetException(ex); }
//        }

//        /// <summary>
//        /// Erlaubt das editieren eines Subject-Datensatzes
//        /// </summary>
//        /// <param name="subject"></param>
//        public void UpdateSubject(Subject subject)
//        {
//            try
//            {
//                using (var context = new FlashcardsContext())
//                {
//                    Subject item = new Subject();
//                    item = context.Subjects.Single(n => n.SubjectId == subject.SubjectId);
//                    item.Picture = subject.Picture;
//                    item.Name = subject.Name;
//                    context.SaveChanges();
//                }
//            }
//            catch (Exception ex)
//            {
//                GetException(ex);
//            }
//        }
//        #endregion
        
//    }
//}
