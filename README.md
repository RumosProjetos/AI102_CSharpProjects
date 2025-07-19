# AI-102 C# Projects

Este repositório contém exemplos práticos adaptados para a linguagem **C#**, com base nos laboratórios oficiais do curso preparatório para a certificação **Microsoft Azure AI Engineer Associate (AI-102)**.

> ⚠️ Observação: Todos os exemplos foram modificados para remover o uso do arquivo `appsettings.json`. As configurações de acesso aos serviços da Azure AI devem ser fornecidas diretamente no código ou via variáveis de ambiente, conforme boas práticas de segurança e portabilidade.

---

## 📚 Estrutura dos Projetos

### 🧠 Computer Vision

#### 🔍 OCR - Read Text

Implementação do recurso de **Leitura Óptica de Caracteres (OCR)** usando o serviço **Azure Computer Vision**.

- 💡 Baseado no laboratório oficial:  
  [AI Vision - OCR Lab](https://microsoftlearning.github.io/mslearn-ai-vision/Instructions/Labs/02-ocr.html)

- Funcionalidades:
  - Extração de texto impresso a partir de imagens
  - Exibição dos resultados no console

- 📁 Projeto: `ComputerVision/OcrReadText`

---

#### 😊 Face API - Análise Facial

Demonstração de uso da **Face API** para detectar e analisar rostos humanos em imagens.

- 💡 Baseado no laboratório oficial:  
  [AI Vision - Face Service Lab](https://microsoftlearning.github.io/mslearn-ai-vision/Instructions/Labs/03-face-service.html)

- Funcionalidades:
  - Detecção de rostos
  - Análise de atributos como emoção, idade estimada, gênero, etc.

- 📁 Projeto: `ComputerVision/FaceApiDemo`

---

## 🛠️ Requisitos

- [.NET 6 ou superior](https://dotnet.microsoft.com/)
- Conta Azure com os serviços de AI (Computer Vision, Face API)
- Chave de API e endpoint dos serviços configurados no código-fonte ou via variáveis de ambiente

---

## 🚀 Como Executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/AI102_CSharpProjects.git
   cd AI102_CSharpProjects
````

2. Restaure os pacotes NuGet:

   ```bash
   dotnet restore
   ```

3. Navegue até o projeto desejado e execute:

   ```bash
   cd ComputerVision/OcrReadText
   dotnet run
   ```

---

## 📦 Pacotes Utilizados

* `Azure.AI.FormRecognizer`
* `Azure.AI.Face`
* `Microsoft.Extensions.Configuration` (com suporte opcional a variáveis de ambiente)

---

## 🧑‍🏫 Sobre

Este repositório é parte das aulas práticas voltadas à preparação para o exame **AI-102: Designing and Implementing an Azure AI Solution**, com foco em alunos e desenvolvedores .NET/C#.

---

## 📄 Licença

Este projeto é distribuído sob a licença [MIT](LICENSE).
