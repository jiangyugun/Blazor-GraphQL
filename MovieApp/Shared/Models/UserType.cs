﻿using System;
using System.Collections.Generic;

namespace MovieApp.Server.Models
{
    public partial class UserType
    {
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; } = null!;
    }
}
