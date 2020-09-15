namespace Common.Models
{
    public abstract class BaseQueries : BaseRepository
    {
        public UserSession LoginSession { set; get; }
    }
}