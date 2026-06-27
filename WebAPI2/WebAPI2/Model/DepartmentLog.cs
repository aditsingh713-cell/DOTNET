using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI2.Model
{
    [Table("DepartmentLog")]
    public class DepartmentLog
    {
        [Key]
        public int LogID { get; set; }  // Primary Key
        public string DepartmentName { get; set; }
        public string ActionName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
