using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.DataTyps
{

    public class Headline
    {
        public string Title { get; set; }
        public int Index { get; set; }
    }

    public class ArticleText
    {
        public string Content { get; set; }
        public int Index { get; set; }
    }

    public class ArticleImage
    {
        public string Path { get; set; }
        public int Index { get; set; }
        public ImagePosition Position { get; set; }
    }

    public enum ImagePosition
    {
        Left,
        Right,
        Center
    }

    public class Article
    {
        public string Id { get; private set; }
        public string Title { get; set; }
        List<ArticleText> Textes { get; set; }
        List<ArticleImage> Images { get; set; }
        List<Headline> Headlines { get; set; }

    }    
}
