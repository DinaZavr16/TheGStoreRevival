using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGStore.Bll.Models
{
    public record CountryUpdateModel : CountryModel
    {
        public int Id { get; init; }
    }
}
