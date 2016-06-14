using System.ComponentModel.DataAnnotations;

namespace AptSystems.Models
{
    public enum CategoryTypes
    {
        [Display(Name = "Здраве")]
        Health,

        [Display(Name = "Хранене")]
        Nutrition,

        [Display(Name = "Фитнес")]
        Fitness,

        [Display(Name = "Медицина")]
        Medicine,

        [Display(Name = "Семейство")]
        Family,

        [Display(Name = "Красота")]
        Beauty,

        [Display(Name = "Слайдер 668-328")]
        Slider,

        All
    }
}
