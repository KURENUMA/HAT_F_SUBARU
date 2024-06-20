namespace HAT_F_api.CustomModels
{
    public class BlobFileInfo
    {
        public bool Checked { get; set; } = false;
        public string Name { get; set; }
        public long ContentLength { get; set; }
        public string CreatedOn { get; set; }
        public string LastModified { get; set; }
    }
}