namespace Enhancements.Misc
{
    public class OptidraSettings
    {
        public virtual bool Enabled { get; set; } = false;
        public virtual int InitialNotePoolSize { get; set; } = 25;
        public virtual int InitialBombPoolSize { get; set; } = 25;
        public virtual int InitialWallPoolSize { get; set; } = 25;
    }
}