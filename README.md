
Etapa 1 – Modelagem do Domínio
Nesta etapa foi criada a estrutura inicial do projeto e modelada a classe principal, chamada Prompt. O objetivo foi definir os atributos essenciais da entidade que será usada nas demais fases da aplicação.
A branch concentra apenas a criação do domínio e a preparação do ambiente, sem implementação de endpoints, serviços ou rotinas adicionais. A ideia foi manter esta fase isolada, seguindo a divisão proposta na prova, para que cada parte da evolução do projeto fique organizada e rastreável.

Etapa 2 – Controller, Service, Repository e Injeção de Dependência
Nesta fase, o projeto passou a ter funcionamento real. Foram criados o controller, a camada de serviço e o repositório, estruturando a lógica da aplicação em três camadas.
Os endpoints básicos foram implementados (criação, busca, listagem, atualização e remoção).
A camada de serviço ficou responsável pela lógica intermediária e validações simples.
O repositório concentrou as operações de persistência.
Também foi configurada a injeção de dependência no Program.cs, conectando as camadas e permitindo que o fluxo da API funcione de forma correta.
Esta branch representa a implementação central do sistema.

Etapa 3 – Validações, melhorias e documentação
A etapa final foi dedicada a ajustes gerais, validações e tratamento simples de erros.
Foram adicionadas respostas mais claras, padronização das mensagens e finalizado o tratamento básico de exceções.
Também foi criada a documentação do projeto, explicando de forma objetiva o que foi desenvolvido em cada fase.
O foco desta etapa foi deixar o projeto organizado, finalizado e de acordo com o que foi solicitado na prova, sem incluir novas funcionalidades além das especificadas.
