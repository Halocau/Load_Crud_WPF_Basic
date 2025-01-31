﻿using System;
using System.Collections.Generic;

namespace Review1q1.BusinessObjects;

public partial class Service
{
    public int Id { get; set; }

    public string? RoomTitle { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public string? FeeType { get; set; }

    public decimal? Amount { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public int? Employee { get; set; }

    public virtual Employee? EmployeeNavigation { get; set; }

    public virtual Room? RoomTitleNavigation { get; set; }
}
