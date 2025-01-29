using BookmarksManager.Models;

namespace JSON_DAL
{
    public class DB
    {
        #region singleton setup
        private static readonly DB instance = new DB();
        public static DB Instance { get { return instance; } }
        #endregion

        public static Repository<Bookmark> Bookmarks { get; set; } = new Repository<Bookmark>();

    }
}