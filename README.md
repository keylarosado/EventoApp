# Cadastro de Eventos — .NET MAUI

Aplicativo Android desenvolvido em C# com .NET MAUI para cadastrar eventos e exibir um resumo com duração e custo total.

## Funcionalidades
- Cadastro de nome, local, datas, número de participantes e custo por participante.
- `BindingContext` associa a tela à model `Evento`.
- `DateTime` e `TimeSpan` calculam a duração do evento. A contagem considera o primeiro e o último dia (período inclusivo).
- Cálculo automático: número de participantes × custo por participante.
- Validação dos campos e navegação para a página de resumo.

