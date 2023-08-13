namespace FileSearchApp
{
    internal class DirectoryResult
    {
        public string Directory { get; set; }
        public int FileCount { get; set; }
        public long TotalFileSize { get; set; }

        public override string ToString()
        {
            return $"{Directory} - Files: {FileCount}, Size: {FileSizeToString(TotalFileSize)}";
        }

        private string FileSizeToString(long size)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int suffixIndex = 0;
            double sizeInBytes = size;

            while (sizeInBytes >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                sizeInBytes /= 1024;
                suffixIndex++;
            }

            return $"{sizeInBytes:N2} {suffixes[suffixIndex]}";
        }
    }
}
