using SQLite;

namespace Security_network_module.Model
{
    [Table("Gaps")]
    class ModelGaps
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public string Name_gaps { get; set; }
        public string Array_gaps { get; set; }
    }
}
