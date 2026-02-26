using SQLite;
using System.ComponentModel;

namespace Repository.Entities
{
    [Table("POSITION")]
    public partial class PositionEntity : BaseEntity, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            IsDirty = true;
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [Ignore]
        public bool IsDirty { get; set; }

        #endregion

        [PrimaryKey]
        [Column("ID")]
        public string ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged(nameof(ID));
                }
            }
        }
        private string _id = "";

        [Column("BEST")]
        public string BEST
        {
            get { return _best; }
            set
            {
                if (_best != value)
                {
                    _best = value;
                    NotifyPropertyChanged(nameof(BEST));
                }
            }
        }
        private string _best = "";

        [Column("WARN")]
        public string WARN
        {
            get { return _warn; }
            set
            {
                if (_warn != value)
                {
                    _warn = value;
                    NotifyPropertyChanged(nameof(WARN));
                }
            }
        }
        private string _warn = "";

        [Column("BAD")]
        public string BAD
        {
            get { return _bad; }
            set
            {
                if (_bad != value)
                {
                    _bad = value;
                    NotifyPropertyChanged(nameof(BAD));
                }
            }
        }
        private string _bad = "";

 
        [Column("DATEMAJ")]
        public DateTime DATEMAJ
        {
            get { return _datemaj; }
            set
            {
                if (_datemaj != value)
                {
                    _datemaj = value;
                    NotifyPropertyChanged(nameof(DATEMAJ));
                }
            }
        }
        private DateTime _datemaj;

 

    }
}
