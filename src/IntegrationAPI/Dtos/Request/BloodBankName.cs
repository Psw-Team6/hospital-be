namespace IntegrationAPI.Dtos.Request
{
    public class BloodBankName
    {
        public BloodBankName(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
