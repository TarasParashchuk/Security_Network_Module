using SQLite;

namespace Security_network_module.Model
{
    [Table("ConstantCoordinates")]
    class ModelTabelNetworkParameter
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public string Array_coordinates { get; set; }
        public int Id_Parameter { get; set; }
    }
}
