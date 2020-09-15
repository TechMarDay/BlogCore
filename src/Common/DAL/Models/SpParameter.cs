namespace DAL.Models
{
    public class SpParameter
    {
        public string SpName { set; get; }
        public string ParamName { set; get; }
        public string TypeName { set; get; }
        public int ParamLength { set; get; }
        public bool IsOutput { set; get; }
        public bool IsTableType { set; get; }
    }
}