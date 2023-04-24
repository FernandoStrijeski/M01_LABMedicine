# LAB Medicine API
![version](https://img.shields.io/static/v1?label=version&message=1.0.0&color=blue)
![status](https://img.shields.io/badge/status-em%20avalia%C3%A7%C3%A3o-yellow)
![release-date](https://img.shields.io/badge/release%20date-04--2023-green)
 ![GitHub Org's stars](https://img.shields.io/github/stars/FernandoStrijeskiLinx?style=social)

LabMedicineApi é uma aplicação Dotnet desenvolvida com o objetivo de gerir atendimentos e gerenciar o armazenamento das informações referentes aos pacientes, enfermeiros e médicos.
<p align="center">
  <img src="https://github.com/FernandoStrijeskiLinx/M01_LABMedicine/blob/main/logo_bk1.png" alt="LabMedicine Logo"/>  
</p>

# 📄**Documentação da API**
Para um melhor entendimento das funcionalidades existentes na API, utilizamos a interface do swagger.<br>
A aplicação é dividida em 4 seções, definidas para uma melhor organização das ações disponíveis, sendo elas:
1) Atendimentos
   <br>
   Responsável pela realização do atendimento entre o médico e o paciente e consulta do histórico de atendimento.
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/atendimentos</td>
   <td>Inclui um novo atendimento, informando a identificação do médico, paciente e descrição do atendimento</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/atendimentos</td>
   <td>Exibe o histórico de atendimentos realizados, buscando opcionalmente pela identificação do médico.</td>
   </tr>
   </table>
       
2) Enfermeiro
   <br>
   Responsável por gerenciar o cadastro dos enfermeiros.
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>POST</td>
   <td>/api/enfermeiros</td>
   <td>Inclui um novo enfermeiro no sistema.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/enfermeiros</td>
   <td>Exibe todos os enfermeiros cadastrados no sistema.</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/enfermeiros/{identificador}</td>
   <td>Altera o cadastro de um enfermeiro, a partir do identificador fornecido.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/enfermeiros/{identificador}</td>
   <td>Busca o cadastro de um determinado enfermeiro, a partir do identificador informado.</td>
   </tr>
   <tr>
   <td>DELETE</td>
   <td>/api/enfermeiros/{identificador}</td>
   <td>Remove do cadastro o enfermeiro informado no identificador da requisição.</td>
   </tr>
   </table>
   
3) Medico
   <br>
   Responsável por gerenciar o cadastro dos médicos.
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>POST</td>
   <td>/api/medicos</td>
   <td>Inclui um novo médico no sistema.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/medicos</td>
   <td>Exibe todos os médicos cadastrados no sistema, opcionalmente filtrando pelo status 'Ativo, Inativo'.</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/medicos/{identificador}</td>
   <td>Altera o cadastro de um médico, a partir do identificador fornecido.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/medicos/{identificador}</td>
   <td>Busca o cadastro de um determinado médico, a partir do identificador informado.</td>
   </tr>
   <tr>
   <td>DELETE</td>
   <td>/api/medicos/{identificador}</td>
   <td>Remove do cadastro o médico informado no identificador da requisição.</td>
   </tr>
   </table>
   
3) Paciente
   <br>
   Responsável por gerenciar o cadastro dos pacientes.
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>POST</td>
   <td>/api/pacientes</td>
   <td>Inclui um novo paciente no sistema.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/pacientes</td>
   <td>Exibe todos os pacientes cadastrados no sistema, opcionalmente filtrando pelo status de atendimento:  'AGUARDANDO_ATENDIMENTO,EM_ATENDIMENTO,ATENDIDO,NAO_ATENDIDO'.</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/pacientes/{identificador}</td>
   <td>Altera o cadastro de um paciente, a partir do identificador fornecido.</td>
   </tr>
   <tr>
   <td>DELETE</td>
   <td>/api/pacientes/{identificador}</td>
   <td>Remove do cadastro o paciente informado no identificador da requisição.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/pacientes/{identificador}</td>
   <td>Busca o cadastro de um determinado paciente, a partir do identificador informado.</td>
   </tr>   
   </table>

# 🗂️**Acesso ao projeto**

Você pode [acessar o código fonte do projeto](https://github.com/FernandoStrijeskiLinx/M01_LABMedicine) ou [baixá-lo](https://github.com/FernandoStrijeskiLinx/M01_LABMedicine/archive/refs/heads/main.zip).

## Abrir e rodar o projeto

Após baixar o projeto, você pode abrir com o `Visual Studio 2022` ou com o `VS Code`.
<br>
As tecnologias utilizadas são:
* C#
* .Net 7.0
* SQL Server

# ⚙️**Configurações**
Para execução dessa aplicação é necessário criar a base de dados, conforme definido na [classe de programa](https://github.com/FernandoStrijeskiLinx/M01_LABMedicine/blob/main/Program.cs). Devido a utilização do Entity Framework (EF), as tabelas utilizadas foram definidas dentro da aplicação em models, sendo necessário a execução de alguns comandos com a finalidade de criar e popular as tabelas para seu primeiro uso, confome o tópico [Comandos utilizados](https://github.com/FernandoStrijeskiLinx/M01_LABMedicine/edit/main/README.md#vs-2022-commands-1)

### No `VS Code` pode ser necessário instalar o EF: dotnet tool install --global dotnet-ef

# 📜**Comandos utilizados**
### Visual Studio 2022
* Add-Migration InitialCreate
* Update-Database
### VS Code
* dotnet ef migrations add InitialCreate 
* dotnet ef database update


# ⚠️**Aviso**
No momento esta API ainda não possui métodos de autenticação, não necessitando a utilização de tokens para acesso. 
<br>Outro ponto a ser aprimorado é a utilização de enumeradores para os itens de status atendimento e situação (opcional).

# 📸**Preview**
<img src="https://user-images.githubusercontent.com/88670789/233817606-cbbce862-0013-46de-b3be-ab36c510464e.png">

[![Watch the video](https://user-images.githubusercontent.com/88670789/233853883-af652a40-3233-499f-982b-bebdbe3114e3.png)](https://drive.google.com/file/d/19srk71iRFDpri6srAlw_ErJqcVr5pWIN/view)

# 💻**Autor**

- [@FernandoStrijeski](https://github.com/FernandoStrijeskiLinx)

Nos vemos no próximo projeto! 👋✌️
