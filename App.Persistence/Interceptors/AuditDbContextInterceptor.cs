using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Persistence.Interceptors;

public class AuditDbContextInterceptor:SaveChangesInterceptor
{
	// Action Delegate parametre alır, geriye bir şey dönmez
	// Func Delegate parametre alır, geriye istediğimiz bir tip döner
	// predicate parametre alır, geriye bool dönen func delegate
	private static readonly Dictionary<EntityState,Action<DbContext,IAuditEntity>> Behaviors = new()
	{
		#region Anoniymous Way
		//{ 
		//	EntityState.Added, (context, entity) => 
		//	{
		//		entity.CreatedDate = DateTime.UtcNow;
		//		// bu data güncellenirken değişmesin. yani oluşacak sql cümleciğine UpdatedDate ekleme
		//		context.Entry(entity).Property(x => x.UpdatedDate).IsModified = false;
		//	} 
		//},
		//{
		//	EntityState.Modified, (context, entity) => 
		//	{
		//		entity.UpdatedDate = DateTime.UtcNow;
		//		context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
		//	} 
		//} 
		#endregion
		{ EntityState.Added, AddBehaviors },
		{ EntityState.Modified, ModifiedBehaviors }
	};

	private static void AddBehaviors(DbContext context, IAuditEntity auditEntity)
	{
		auditEntity.CreatedDate = DateTime.UtcNow;
		// bu data güncellenirken değişmesin. yani oluşacak sql cümleciğine UpdatedDate ekleme
		context.Entry(auditEntity).Property(x => x.UpdatedDate).IsModified = false;

	}

	private static void ModifiedBehaviors(DbContext context, IAuditEntity auditEntity)
	{
		auditEntity.UpdatedDate = DateTime.Now;
		context.Entry(auditEntity).Property(x => x.CreatedDate).IsModified = false;
	}
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
	{
		foreach (var entry in eventData.Context!.ChangeTracker.Entries().ToList())
		{
			if (entry.Entity is not IAuditEntity auditEntity) continue;

			if (entry.State is not (EntityState.Added or EntityState.Modified)) continue;
			
			Behaviors[entry.State](eventData.Context, auditEntity);
			#region No Delegate
			//switch (entry.State)
			//{
			//	case EntityState.Added:

			//		AddBehaviors(eventData.Context, auditEntity);
			//		break;

			//	case EntityState.Modified:

			//		ModifiedBehaviors(eventData.Context, auditEntity);
			//		break;
			//} 
			#endregion
		}


		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	
}
