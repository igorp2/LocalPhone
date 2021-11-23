using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public abstract class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public virtual int Id { get; set; }

        [Column(TypeName = "int")]
        public virtual ModelStatuses Status { get; set; }

        [Column(TypeName = "datetime")]
        public virtual DateTime? CreationDate { get; set; }

        [Column(TypeName = "varchar(450)")]
        public virtual string CreatorUser { get; set; }

        [Column(TypeName = "datetime")]
        public virtual DateTime? LastModificationDate { get; set; }

        [Column(TypeName = "varchar(450)")]
        public virtual string UserThatMadeTheLastModification { get; set; }

        protected virtual void CloneBaseModelAttributesExcludingStatusFromModel(BaseModel modelToClone)
        {
            CreationDate = modelToClone.CreationDate;
            CreatorUser = modelToClone.CreatorUser;
            LastModificationDate = DateTime.Now;
            UserThatMadeTheLastModification = modelToClone.UserThatMadeTheLastModification;
        }

        public abstract void CloneFromModel(BaseModel modelToClone);

        public virtual void DeleteSelfLogically()
        {
            Status = ModelStatuses.INACTIVE;
            LastModificationDate = DateTime.Now;
        }
    }
}
