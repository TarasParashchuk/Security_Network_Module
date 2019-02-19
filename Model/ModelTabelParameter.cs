using SQLite;

namespace Security_network_module.Model
{
    [Table("Parameter")]
    class ModelTabelParameter
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public string Name_parameter { get; set; }
        public int Id_gaps { get; set; }
    }
}
