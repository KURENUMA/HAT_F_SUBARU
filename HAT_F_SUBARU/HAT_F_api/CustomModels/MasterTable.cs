namespace HAT_F_api.CustomModels
{
    public class MasterTable
    {
        public string Name {  get; set; }
        public string LogicalName { get; set; }
        public List<MasterTableColumn> Columns { get; set; }
    }
}
