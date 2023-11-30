using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
	[Table("System_Language_Codes")]
	public class SystemLanguageCodePoco
	{
		[Key]
		public string LanguageID { get; set; }

		[Column("Name")]
		public string Name { get; set; }

		[Column("Native_Name")]
		public string NativeName { get; set; }

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
    }
}

