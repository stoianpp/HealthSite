using System.ComponentModel.DataAnnotations;

namespace AptSystems.Models
{
    public enum SubCategories
    {
        [Display(Name = "Здраве Новини")]
        HealthNews,

        [Display(Name = "Здраве Тяло")]
        HealthBody,

        [Display(Name = "Здраве Духовност")]
        HealthFaith,

        [Display(Name = "Хранене Новини")]
        NutritionNews,

        [Display(Name = "Хранене Диета")]
        NutritionDiet,

        [Display(Name = "Хранене Лечение")]
        NutritionCure,

        [Display(Name = "Фитнес Новини")]
        FitnessNews,

        [Display(Name = "Фитнес Фитнес")]
        FitnessPrograms,

        [Display(Name = "Фитнес Добавки")]
        FitnessAdditions,

        [Display(Name = "Медицина Новини")]
        MedicineNews,

        [Display(Name = "Медицина Лечения")]
        MedicineMedicines,

        [Display(Name = "Медицина Алтернативи")]
        MedicineAlternatives,

        [Display(Name = "Семейство Новини")]
        FamilyNews,

        [Display(Name = "Семейство Взаимоотношения")]
        FamilyRelations,

        [Display(Name = "Семейство Деца")]
        FamilyChildren,

        [Display(Name = "Красота Новини")]
        BeatyNews,

        [Display(Name = "Красота Стил")]
        BeatyStyle,

        [Display(Name = "Красота Знаменитости")]
        BeautyCelebrities
    }
}