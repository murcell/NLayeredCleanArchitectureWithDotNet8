﻿namespace App.Repositories;

public interface IAuditEntity
{
	public DateTime CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }
	//public DateTime? DeletedDate { get; set; }
	//public bool IsDeleted { get; set; }
}
