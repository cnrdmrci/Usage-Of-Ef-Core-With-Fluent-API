using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFrameworkCoreExamples
{
    /*
     * There are two ways to configure domain classes in EF Core (same as in EF 6).
     * 1. By using Data Annotation Attributes
     * 2. By using Fluent API
     */

    /*
     * Everything what you can configure with DataAnnotations is also possible with the Fluent API.
     * The reverse is not true. So, from the viewpoint of configuration options and flexibility the Fluent API is "better".
     */

    //-----------------------------------------------------------------------------------
    // 1. Data Annotation

    [Table("DataAnnotation")]
    public class DataAnnotation
    {
        public DataAnnotation()
        {
        }

        [Key]
        public int SID { get; set; }

        [Column("Name", TypeName = "ntext")]
        [MaxLength(20)]
        public string StudentName { get; set; }

        [NotMapped]
        public int? Age { get; set; }


        public int StdId { get; set; }

        [ForeignKey("StdId")]
        public virtual Standard Standard { get; set; }
    }

    [Table("Standard")]
    public class Standard
    {
        public Standard()
        {
        }
        [Key]
        public int StdID { get; set; }

        [Column("StdName", TypeName = "ntext")]
        [MaxLength(20)]
        public string StandardName { get; set; }

        [ForeignKey("DataAnnotation")]
        public virtual DataAnnotation DataAnnotation { get; set; }
    }


}
