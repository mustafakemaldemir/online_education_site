using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class online_educationContext : DbContext
    {
        public online_educationContext()
        {
        }

        public online_educationContext(DbContextOptions<online_educationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Cnumber> Cnumbers { get; set; }
        public virtual DbSet<CourseStudent> CourseStudents { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conf = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();


                var connectionString = conf.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("branch_ID");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("branch_Name");
            });

            modelBuilder.Entity<Cnumber>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("PK_Class");

                entity.ToTable("CNumber");

                entity.Property(e => e.ClassId).HasColumnName("class_ID");

                entity.Property(e => e.ClassNumber).HasColumnName("class_Number");
            });

            modelBuilder.Entity<CourseStudent>(entity =>
            {
                entity.HasKey(e => new { e.CourseLessonId, e.CourseStudentId });

                entity.ToTable("CourseStudent");

                entity.Property(e => e.CourseLessonId).HasColumnName("course_lessonID");

                entity.Property(e => e.CourseStudentId).HasColumnName("course_studentID");

                entity.HasOne(d => d.CourseLesson)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.CourseLessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseStudent_Lesson");

                entity.HasOne(d => d.CourseStudentNavigation)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.CourseStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseStudent_Student");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.Property(e => e.DocumentId).HasColumnName("document_ID");

                entity.Property(e => e.DocumentClassId).HasColumnName("document_ClassID");

                entity.Property(e => e.DocumentDate)
                    .HasColumnType("date")
                    .HasColumnName("document_Date");

                entity.Property(e => e.DocumentLessonId).HasColumnName("document_lessonID");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("document_Name");

                entity.Property(e => e.DocumentPrefix)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("document_Prefix");

                entity.Property(e => e.DocumentRealname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("document_Realname");

                entity.Property(e => e.DocumentTeacherId).HasColumnName("document_TeacherID");

                entity.HasOne(d => d.DocumentClass)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_CNumber");

                entity.HasOne(d => d.DocumentLesson)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentLessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_Lesson");

                entity.HasOne(d => d.DocumentTeacher)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentTeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_Teacher");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lesson");

                entity.Property(e => e.LessonId).HasColumnName("lesson_ID");

                entity.Property(e => e.LessonClassId).HasColumnName("lesson_ClassID");

                entity.Property(e => e.LessonName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lesson_Name");

                entity.Property(e => e.LessonTeacherId).HasColumnName("lesson_teacherID");

                entity.HasOne(d => d.LessonClass)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.LessonClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lesson_CNumber");

                entity.HasOne(d => d.LessonTeacher)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.LessonTeacherId)
                    .HasConstraintName("FK_Lesson_Teacher");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId).HasColumnName("student_ID");

                entity.Property(e => e.StudentClassId).HasColumnName("student_ClassID");

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("student_Name");

                entity.Property(e => e.StudentSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("student_Surname");

                entity.Property(e => e.StudentUserId).HasColumnName("student_userID");

                entity.HasOne(d => d.StudentClass)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StudentClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_CNumber");

                entity.HasOne(d => d.StudentUser)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StudentUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_User");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_ID");

                entity.Property(e => e.TeacherBranchId).HasColumnName("teacher_branchID");

                entity.Property(e => e.TeacherName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("teacher_name");

                entity.Property(e => e.TeacherSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("teacher_surname");

                entity.Property(e => e.TeacherUserId).HasColumnName("teacher_userID");

                entity.HasOne(d => d.TeacherBranch)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.TeacherBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_Branches");

                entity.HasOne(d => d.TeacherUser)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.TeacherUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("user_ID");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_Email");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_Password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
