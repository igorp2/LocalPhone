using LocalPhoneDomain;
using LocalPhoneDomain.Areas.Identity.Data;
using LocalPhoneDomain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LocalPhoneAdmin.Data
{
    public class LocalPhoneAdminContext : IdentityDbContext<UserModel>
    {
        public LocalPhoneAdminContext(DbContextOptions<LocalPhoneAdminContext> options)
            : base(options) { }

        public virtual DbSet<AddressModel> Addresses { get; set; }
        public virtual DbSet<CityModel> Cities { get; set; }
        public virtual DbSet<ContactModel> Contacts { get; set; }
        public virtual DbSet<CountryModel> Countries { get; set; }
        public virtual DbSet<CustomerModel> Customers { get; set; }
        public virtual DbSet<GenderModel> Genders { get; set; }
        public virtual DbSet<MessageModel> Messages { get; set; }
        public virtual DbSet<NumberModel> Numbers { get; set; }
        public virtual DbSet<PaymentModel> Payments { get; set; }
        public virtual DbSet<StateModel> States { get; set; }
        public virtual DbSet<AvailableNumberModel> AvailableNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<AddressModel>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.IdCity).HasColumnName("idCity");

                entity.Property(e => e.IdCountry).HasColumnName("idCountry");

                entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");

                entity.Property(e => e.IdState).HasColumnName("idState");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int) x, x => (ModelStatuses) x)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Street)
                    .HasMaxLength(200)
                    .HasColumnName("street");

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.Property(e => e.Zip).HasMaxLength(30);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.IdCity)
                    .HasConstraintName("FK_AddressCity");

                entity.HasOne(d => d.Country)
                    .WithMany()
                    .HasForeignKey(d => d.IdCountry)
                    .HasConstraintName("FK_AddressCountry");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK_AddressCustomer");

                entity.HasOne(d => d.State)
                    .WithMany()
                    .HasForeignKey(d => d.IdState)
                    .HasConstraintName("FK_AddressState");
            });

            modelBuilder.Entity<CityModel>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.Property(e => e.IdState).HasColumnName("idState");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int) x, x => (ModelStatuses) x)
                    .HasDefaultValueSql("((1))");

                entity.HasMany(d => d.Addresses)
                    .WithOne(p => p.City)
                    .HasForeignKey(d => d.IdCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityAddress");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.IdState)
                    .HasConstraintName("FK_CityState");
            });

            modelBuilder.Entity<ContactModel>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int) x, x => (ModelStatuses) x)
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactCustomer");
            });

            modelBuilder.Entity<CountryModel>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.Iso)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("iso")
                    .IsFixedLength(true);

                entity.Property(e => e.Iso3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("iso3")
                    .IsFixedLength(true);

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                entity.Property(e => e.Nicename)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("nicename");

                entity.Property(e => e.Numcode).HasColumnName("numcode");

                entity.Property(e => e.Phonecode).HasColumnName("phonecode");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int) x, x => (ModelStatuses) x)
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CustomerModel>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasKey(customer => customer.PhoneNumber);

                entity.Property(e => e.Avatar).HasColumnName("avatar");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("firstName");

                entity.Property(e => e.IdCountry).HasColumnName("idCountry");

                entity.Property(e => e.Status)
                    .HasConversion(x => (int) x, x => (CustomerStatuses)x);

                entity.Property(e => e.IdGender).HasColumnName("idGender");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("lastName");

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                entity.Property(e => e.OperationalSystem)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("operationalSystem");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.ValidationCodeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("validationCodeDate");

                entity.Property(e => e.VerificationCode).HasColumnName("verificationCode");

                entity.Property(e => e.VerificationCodeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("verificationCodeDate");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.IdCountry)
                    .HasConstraintName("FK_CustomerCountry");

                entity.HasOne(d => d.Gender)
                    .WithMany()
                    .HasForeignKey(d => d.IdGender)
                    .HasConstraintName("FK_CustomerGender")
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<GenderModel>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Gender)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Gender");

                entity.Property(e => e.IdCountry).HasColumnName("idCountry");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int) x, x => (ModelStatuses) x)
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Genders)
                    .HasForeignKey(d => d.IdCountry)
                    .HasConstraintName("FK_GenderCountry");
            });

            modelBuilder.Entity<MessageModel>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.IdCustomerReceiving).HasColumnName("idCustomerReceiving");

                entity.Property(e => e.IdCustomerSending).HasColumnName("idCustomerSending");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int) x, x => (ModelStatuses) x)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Text).IsUnicode(false);

                entity.HasOne(d => d.CustomerReceiving)
                    .WithMany(p => p.MessageIdCustomerReceiving)
                    .HasForeignKey(d => d.IdCustomerReceiving)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageCustomerReceiving");

                entity.HasOne(d => d.CustomerSending)
                    .WithMany(p => p.MessageIdCustomerSending)
                    .HasForeignKey(d => d.IdCustomerSending)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageCustomerSending");
            });

            modelBuilder.Entity<NumberModel>(entity =>
            {
                entity.ToTable("Number");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");

                entity.Property(e => e.IdPayment).HasColumnName("idPayment");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int) x, x => (ModelStatuses) x)
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Numbers)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NumberCustomer");

                //entity.HasOne(d => d.Payment)
                //    .WithMany(p => p.Numbers)
                //    .HasForeignKey(d => d.IdPayment)
                //    .HasConstraintName("FK_NumberPayment");
            });

            modelBuilder.Entity<PaymentModel>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CardNumber).HasMaxLength(16);

                //entity.Property(e => e.ClientSecret).HasMaxLength(100);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.Cvc).HasColumnName("CVC");

                entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                //entity.Property(e => e.MethodTypes).HasMaxLength(100);

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                //entity.Property(e => e.PaymentIntentId).HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int) x, x => (ModelStatuses) x)
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK_PaymentCustomer");
            });

            modelBuilder.Entity<StateModel>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.Abbreviation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.IdCountry).HasColumnName("idCountry");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int) x, x => (ModelStatuses) x)
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StateCountry");

                entity.HasMany(d => d.Cities)
                    .WithOne(p => p.State)
                    .HasForeignKey(d => d.IdState)
                    .HasConstraintName("FK_StateCity");
            });
            
            modelBuilder.Entity<AvailableNumberModel>(entity =>
            {
                entity.ToTable("AvailableNumber");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.idCountry).HasColumnName("idCountry");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.AvailableNumbers)
                    .HasForeignKey(d => d.idCountry)
                    .HasConstraintName("FK_AvailableNumberCountry");

                entity.Property(e => e.idCity).HasColumnName("idCity");

                entity.HasOne(d => d.City)
                  .WithMany(p => p.AvailableNumbers)
                  .HasForeignKey(d => d.idCountry)
                  .HasConstraintName("FK_AvailableNumberCity");

                entity.Property(e => e.phoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(x => (int)x, x => (ModelStatuses)x)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creationDate");

                entity.Property(e => e.CreatorUser)
                    .HasMaxLength(450)
                    .HasColumnName("creatorUser");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastModificationDate");

                entity.Property(e => e.UserThatMadeTheLastModification)
                    .HasMaxLength(450)
                    .HasColumnName("UserThatMadeTheLastModification");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
