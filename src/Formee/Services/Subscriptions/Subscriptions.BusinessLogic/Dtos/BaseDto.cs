﻿namespace Subscriptions.BusinessLogic.Dtos;

public class BaseDto
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
