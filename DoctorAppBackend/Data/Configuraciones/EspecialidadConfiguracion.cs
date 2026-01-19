using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;

namespace Data.Configuraciones
{
    public class EspecialidadConfiguracion : IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(e => e.NombreEspecialidad).IsRequired().HasMaxLength(60);
            builder.Property(e => e.Descripcion).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Estado).IsRequired();
        }
    }
}
