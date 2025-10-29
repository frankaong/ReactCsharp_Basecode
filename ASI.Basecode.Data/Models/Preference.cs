using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ASI.Basecode.Data.Models;

public partial class Preference
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool? ShowStats { get; set; }

    public bool? ShowSatisfaction { get; set; }

    public string CardOrder { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [NotMapped]
    public List<string> CardOrderList
    {
        get => string.IsNullOrEmpty(CardOrder)
            ? new List<string>()
            : JsonConvert.DeserializeObject<List<string>>(CardOrder);
        set => CardOrder = JsonConvert.SerializeObject(value);
    }
}
