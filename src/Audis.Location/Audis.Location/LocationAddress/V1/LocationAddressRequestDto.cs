﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Audis.Location.LocationAddress.V1
{
    public class LocationAddressRequestDto
    {
        // either address, placeid or both can be set in the analyser
        public string? Address { get; set; }
        public string? PlaceId { get; set; }
        public string LocationProvider { get; set; }
    }
}
