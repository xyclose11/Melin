﻿namespace Melin.Server.Models.DTO;

public class AddTagsRequest
{
    public int RefId { get; set; }
    public List<Tag> Tags { get; set; }
}