using LinkDev.Talabat.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Configs.Base
{
    internal class BaseAuditableEntityConfigurations<TEntity, TKey> : BaseEntityConfigurations<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            /// builder.Property(e => e.CreatedOn).HasDefaultValue("GETUTCDATE()");
            /// builder.Property(e => e.LastModifiedBy).HasComputedColumnSql("GETUTCDATE()");
        }
    }
}
