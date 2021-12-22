namespace Backups.FileSystem
{
    public abstract class StorageObject
    {
        protected string PathToParentDirectory { get; set; }

        protected string Name { get; set; }

        public abstract void SetPath(string path);

        public abstract string GetPath();
    }
}