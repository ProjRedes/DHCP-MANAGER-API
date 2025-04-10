using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DHCPManagerAPI.Models;

namespace DHCPManagerAPI.Services
{
    public class GoogleSheetsService
    {
        private readonly SheetsService _sheetsService;
        private readonly string _spreadsheetId;
        private readonly string _sheetName = "bd-DHCPManager";

        public GoogleSheetsService(string credentialsPath, string spreadsheetId)
        {
            _spreadsheetId = spreadsheetId ?? throw new ArgumentNullException(nameof(spreadsheetId));

            GoogleCredential credential;
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(SheetsService.Scope.Spreadsheets);
            }

            _sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DHCPManagerAPI"
            });
        }

        public async Task<List<DHCPHost>> GetHostsFromSheet()
        {
            var range = $"{_sheetName}!A:E";
            var request = _sheetsService.Spreadsheets.Values.Get(_spreadsheetId, range);
            var response = await request.ExecuteAsync();
            var values = response.Values;

            var hosts = new List<DHCPHost>();
            if (values != null && values.Count > 1) // Ignorar cabeçalhos
            {
                foreach (var row in values.Skip(1))
                {
                    if (row.Count < 5) continue;

                    int id = 0;
                    int.TryParse(row[0].ToString(), out id);

                    hosts.Add(new DHCPHost
                    {
                        Id = id,
                        IpAddress = row[1]?.ToString() ?? "",
                        NomeNetBIOS = row[2]?.ToString() ?? "",
                        MacAddress = row[3]?.ToString() ?? "",
                        Vlan = row[4]?.ToString() ?? ""
                    });
                }
            }
            return hosts;
        }

        public async Task AddHostToSheet(string ipAddress, string nomeNetBIOS, string macAddress, string vlan)
        {
            var hosts = await GetHostsFromSheet();
            int newId = hosts.Count > 0 ? hosts[^1].Id + 1 : 1; // Gerar ID sequencial

            var range = $"{_sheetName}!A:E";
            var valueRange = new ValueRange
            {
                Values = new List<IList<object>> { new List<object> { newId, ipAddress, nomeNetBIOS, macAddress, vlan } }
            };

            var appendRequest = _sheetsService.Spreadsheets.Values.Append(valueRange, _spreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            await appendRequest.ExecuteAsync();
        }
    }
}
