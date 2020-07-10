using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            DateAdded = DateTime.UtcNow;
        }
        [Required]
        public Guid Id { get; set; }

        [Display (Name ="Назва (заголовок)")]
        public virtual string Title { get; set; }

        [Display(Name = "Короткий опис")]
        public virtual string SubTitle { get; set; }

        [Display(Name = "Повний опис")]
        public virtual string Text { get; set; }

        [Display(Name = "Титульна сторінка")]
        public virtual string TitleImagePath { get; set; }

        [Display(Name = "SEO метатег Title")]
        public string MetaTitle { get; set; }

        [Display(Name = "SEO метатег Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "SEO метатег KeyWord")]
        public string MetaKeyWord{ get; set; }

        [DataType(DataType.Time)]
        public DateType DateAdded { get; set; }
    }
}
