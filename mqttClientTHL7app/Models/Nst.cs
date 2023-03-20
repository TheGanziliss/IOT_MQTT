using System;
using System.Collections.Generic;

namespace mqttClientTHL7app.Models;

public partial class Nst
{
    public int NstId { get; set; }

    public int? Fhr { get; set; }

    public int? Ua { get; set; }
}
