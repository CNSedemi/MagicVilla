using MgicVilla_API.Modelos.Dto;

namespace MgicVilla_API.Datos
{
    public static class VillaStore
    {

        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto{Id=1, Nombre="Visita a la Piscina", Ocupantes=3, MetrosCuadrados=80},
            new VillaDto{Id=2, Nombre="Visita a la playa",Ocupantes=2, MetrosCuadrados=550}
        };

    }
}
