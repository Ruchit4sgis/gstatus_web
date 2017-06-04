namespace gstatus
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class datagstatus : DbContext
    {
        public datagstatus()
            : base("name=datagstatus")
        {
        }

        public DbSet<Login> Login { get; set; }
            public DbSet<student_data> student_data { get; set; }
            public DbSet<gcode> gcode { get; set; }
            public DbSet<record> record { get; set; }
    }


}