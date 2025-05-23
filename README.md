# DHCP-MANAGER-API
# DHCPManager

Sistema web desenvolvido para o gerenciamento de reservas DHCP com integração ao Google Sheets como banco de dados remoto.

## 🎯 Objetivo
Oferecer uma solução visual e intuitiva para gerenciamento de IPs, hosts e VLANs, facilitando o controle de redes em ambientes corporativos ou educacionais.

## 🚀 Tecnologias Utilizadas

- **Backend:** ASP.NET Core 9
- **Frontend:** React.js (Create React App)
- **Banco de Dados:** Google Sheets API
- **Autenticação:** JWT Bearer Authentication
- **Documentação/Testes:** Swagger, Postman
- **Ambiente de Desenvolvimento:** Visual Studio 2022, VS Code, Node.js

## 📂 Estrutura do Projeto

DHCPManagerAPI
├── Controllers/
├── Models/
├── Services/
├── Program.cs
├── appsettings.Development.json
├── credentials.json

/dhcpmanager-frontend
├── src/components/Navbar.js
├── src/pages/CadastroHost.js
├── src/pages/Hosts.js
├── src/pages/VLANManager.js
├── src/services/api.js

## 🔧 Funcionalidades

- Cadastro de hosts com IP, MAC, nome NetBIOS e associação à VLAN.
- Listagem e gerenciamento de VLANs.
- Integração dinâmica entre frontend e Google Sheets.
- Interface moderna com React Router e Axios.

## ✅ Status

- [x] Backend com integração à Google Sheets
- [x] Frontend com cadastro e listagem de Hosts
- [x] Dropdown dinâmico de VLANs
- [x] Autenticação com JWT
- [ ] CRUD completo para VLANs (em andamento)

## 📎 Repositório

[GitHub - DHCPManager API](https://github.com/ProjRedes/DHCP-MANAGER-API)

## 📄 Licença

Este projeto é livre para fins educacionais e está sob a licença MIT.
