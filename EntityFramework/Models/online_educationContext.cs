using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
                optionsBuilder.UseSqlServer("Server=DESKTOP-U2SBDLE;Database=online_education;Trusted_Connection=Yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Cnumber>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("PK_Class");

                entity.ToTable("CNumber");

                entity.Property(e => e.ClassId).HasColumnName("class_Id");

                entity.Property(e => e.ClassNumber).HasColumnName("class_Number");
            });

            modelBuilder.Entity<CourseStudent>(entity =>
            {
                entity.HasKey(e => new { e.LessonId, e.StudentId });

                entity.ToTable("CourseStudent");

                entity.Property(e => e.LessonId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("lesson_Id");

                entity.Property(e => e.StudentId).HasColumnName("student_Id");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseStudent_Lesson");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseStudents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseStudent_Student");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.Property(e => e.DocumentId).HasColumnName("document_Id");

                entity.Property(e => e.DocumentClass).HasColumnName("document_Class");

                entity.Property(e => e.DocumentDate)
                    .HasColumnType("date")
                    .HasColumnName("document_Date");

                entity.Property(e => e.DocumentLesson).HasColumnName("document_Lesson");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("document_Name");

                entity.Property(e => e.DocumentTeacher).HasColumnName("document_Teacher");

                entity.HasOne(d => d.DocumentClassNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_CNumber");

                entity.HasOne(d => d.DocumentLessonNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentLesson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_Lesson");

                entity.HasOne(d => d.DocumentTeacherNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_Teacher");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lesson");

                entity.Property(e => e.LessonId).HasColumnName("lesson_Id");

                entity.Property(e => e.LessonClass).HasColumnName("lesson_Class");

                entity.Property(e => e.LessonName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lesson_Name");

                entity.Property(e => e.LessonTeacher).HasColumnName("lesson_Teacher");

                entity.HasOne(d => d.LessonClassNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.LessonClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lesson_CNumber");

                entity.HasOne(d => d.LessonTeacherNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.LessonTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lesson_Teacher");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId).HasColumnName("student_Id");

                entity.Property(e => e.StudentClass).HasColumnName("student_Class");

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

                entity.Property(e => e.UserId).HasColumnName("user_Id");

                entity.HasOne(d => d.StudentClassNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StudentClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_CNumber");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_User");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_Id");

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

                entity.Property(e => e.UserId).HasColumnName("user_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("user_Id");

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
