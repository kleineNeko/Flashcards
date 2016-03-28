
namespace Flashcards.DataTyps
{
    public struct DirectoryTree
    {
        public string DirectoryName { get; set; }
        public string Parent { get; set; }
        public string Path { get; set; }
        public bool HasImage { get; set; }
        public string[] Subdirectorys { get; set; }
    }
    
}
