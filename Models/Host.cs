namespace DHCPManagerAPI.Models
{
    public class DHCPHost
    {
        public int Id { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string NomeNetBIOS { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;
        public string Vlan { get; set; } = string.Empty;

        public DHCPHost() { }

        public DHCPHost(int id, string ipAddress, string nomeNetBIOS, string macAddress, string vlan)
        {
            Id = id;
            IpAddress = ipAddress;
            NomeNetBIOS = nomeNetBIOS;
            MacAddress = macAddress;
            Vlan = vlan;
        }
    }
}
