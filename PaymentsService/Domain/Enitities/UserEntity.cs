using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsService.Domain.Enitities
{
  [Table("payments")]
  public sealed class UserEntity
  {
    public Guid Id { get; set; }
    public int Amount { get; set; }
  }
}
