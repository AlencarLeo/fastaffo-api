using System.Text.Json.Serialization;

namespace fastaffo_api.src.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ABN { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<UserAdmin>? OwnersAndAdmins { get; set; }
    }
}
