using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CareerCloud.Pocos;
using CareerCloud.ADODataAccessLayer;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        private readonly string _connectionString;

        public CareerCloudContext()
        {
            _connectionString = Connection.GetConnectionString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        // entities
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobsDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configs
            modelBuilder.Entity<ApplicantProfilePoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<ApplicantEducationPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<ApplicantSkillPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyProfilePoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyJobPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyLocationPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();
            
            modelBuilder.Entity<CompanyJobEducationPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<SecurityLoginPoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .Property(item => item.TimeStamp)
                .IsRowVersion();


            // Relationships
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(item => item.SystemCountryCode)
                .WithMany(item => item.ApplicantProfiles)
                .HasForeignKey(item => item.Country);
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(item => item.SecurityLogin)
                .WithMany(item => item.ApplicantProfiles)
                .HasForeignKey(item => item.Login);

            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantEducations)
                .HasForeignKey(item => item.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantJobApplications)
                .HasForeignKey(item => item.Applicant);
            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(item => item.CompanyJob)
                .WithMany(item => item.ApplicantJobApplications)
                .HasForeignKey(item => item.Job);

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantResumes)
                .HasForeignKey(item => item.Applicant);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantSkills)
                .HasForeignKey(item => item.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(item => item.ApplicantProfile)
                .WithMany(item => item.ApplicantWorkHistorys)
                .HasForeignKey(item => item.Applicant);
            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(item => item.SystemCountryCode)
                .WithMany(item => item.ApplicantWorkHistories)
                .HasForeignKey(item => item.CountryCode);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(item => item.SystemLanguageCode)
                .WithMany(item => item.CompanyDescriptions)
                .HasForeignKey(item => item.LanguageId);
            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(item => item.CompanyProfile)
                .WithMany(item => item.CompanyDescriptions)
                .HasForeignKey(item => item.Company);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(item => item.CompanyProfile)
                .WithMany(item => item.CompanyJobs)
                .HasForeignKey("Company");
            modelBuilder.Entity<CompanyJobPoco>().HasMany(item => item.CompanyJobDescriptions);
            modelBuilder.Entity<CompanyJobPoco>().HasMany(item => item.CompanyJobEducations);

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(item => item.CompanyJob)
                .WithMany(item => item.CompanyJobDescriptions)
                .HasForeignKey("Job");

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(item => item.CompanyProfile)
                .WithMany(item => item.CompanyLocations)
                .HasForeignKey(item => item.Company);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(item => item.SystemCountryCode)
                .WithMany(item => item.CompanyLocations)
                .HasForeignKey(item => item.CountryCode);

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(item => item.CompanyJob)
                .WithMany(item => item.CompanyJobEducations)
                .HasForeignKey(item => item.Job);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(item => item.CompanyJob)
                .WithMany(item => item.CompanyJobSkills)
                .HasForeignKey(item => item.Job);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(item => item.SecurityLogin)
                .WithMany(item => item.SecurityLoginsLogs)
                .HasForeignKey(item => item.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(item => item.SecurityRole)
                .WithMany(item => item.SecurityLoginsRoles)
                .HasForeignKey(item => item.Role);
            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(item => item.SecurityLogin)
                .WithMany(item => item.SecurityLoginsRoles)
                .HasForeignKey(item => item.Login);

        }
    }
}

