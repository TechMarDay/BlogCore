namespace DAL.Models
{
    public class DefinedTableType
    {
        public string TableName { set; get; }
        public string ColumnName { set; get; }
        public int Length { set; get; }
        public bool IsNull { set; get; }
    }
}