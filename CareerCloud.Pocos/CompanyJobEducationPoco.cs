﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Educations")]
    public class CompanyJobEducationPoco : IPoco
	{
        [Key]
        public Guid Id { get; set; }

        [Column("Job")]
        public Guid Job { get; set; }

        [Column("Major")]
        public string? Major { get; set; }

        [Column("Importance")]
        public Int16 Importance { get; set; }

        [Column("Time_Stamp")]
        public byte[]? TimeStamp { get; set; }

        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}

