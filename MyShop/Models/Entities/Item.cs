using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models.Entities
{
    public class Item
    {
        
        public int Id { get; set; }

        [Required, DisplayName("Кодовое имя"), StringLength(50)]
        public string Path { get; set; }

        [Required, DisplayName("Название"), StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required, DisplayName("Цена"), Range(0, int.MaxValue)]
        public double Price { get; set; }

        [Required, DisplayName("Цена (на экране)")]
        public string PriceDesc { get; set; }

        [Required, DisplayName("Тип")]
        public ItemType Type { get; set; }

        [Required, DisplayName("Порядковый номер в списке")]
        public int Priority { get; set; }

        [Required, DisplayName("Количество"), Range(0, int.MaxValue)]
        public int Count { get; set; }

        public bool IsNew { get; set; }
        public bool IsSale { get; set; }
        public Menu Catalog { get; set; }
        public Region Region { get; set; }
        
        [DisplayName("Тэги для поиска")]
        public string Tags { get; set; }

        [DisplayName("Файл изображения")]
        public string ImageName { get; set; }

        [DisplayName("Дополнительная информация")]
        public string[] AdditionalInfo { get; set; }
    }

    public enum ItemType
    {
        STANDART = 0,

    }
}