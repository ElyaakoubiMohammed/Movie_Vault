using ControlInventoryManagment.DTOs.Etage;
using ControlInventoryManagment.DTOs.Local;

namespace ControlInventoryManagment.DTOs;

public class EntrepriseCreateDTO
{
    public required string Name { get; set; }
    public int CityId { get; set; }
    public required string Adresse { get; set; }
    public  required List<LocalCreateDTO> Locals { get; set; }  
    public required List<EtageCreateDTO> Etages { get; set; }  
}
