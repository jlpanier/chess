using Repository.Entities;

namespace Repository.Dbo
{
    public class PositionDbo: BaseDbo
    {
        public static PositionDbo Instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new PositionDbo();
                }
                return _instance;
            }
        }
        private static PositionDbo? _instance;

        public IEnumerable<PositionEntity> All()
        {
            lock (dbLock)
            {
                return Db.Query<PositionEntity>(@"Select * from POSITION");
            }
        }

        public IEnumerable<PositionEntity> GetByPosition(string position)
        {
            lock (dbLock)
            {
                return Db.Query<PositionEntity>(@"Select * from POSITION WHERE ID = ?", position);
            }
        }

        public int RemoveById(Guid id)
        {
            lock (dbLock)
            {
                return Db.Execute(@"DELETE FROM POSITION WHERE ID = ?", id);
            }
        }
    }
}
