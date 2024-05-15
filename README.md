## Teste manutenção de um blog pessoal;
## Requisitos Técnicos:

Utilize a arquitetura monolítica organizando as responsabilidades, como autenticação,
gerenciamento de postagens e notificações em tempo real.

Aplique os princípios SOLID, especialmente o princípio da Responsabilidade Única (SRP) e o
Princípio da Inversão de Dependência (DIP).

Utilize o Entity Framework para interagir com o banco de dados para armazenar informações
sobre usuários e postagens.

Implemente WebSockets para notificações em tempo real. Pode ser uma notificação simples
para interface do usuário sempre que uma nova postagem é feita.

## Teste do codigo;
Antes de rodar deve ser executo so comandos para criação do banco de dados 

add-migration v01 -context BaseContext
update-database -context BaseContext

A aplicação tem swagger para teste da API 
E uma pagina index.html para teste a comunicação do websocket 
ao enviar pela api  endpoint /notification/ 

Com a pagina index aberta ao adicionar um blog na api controller /blog/ {post}
tambem aparecera uma noticação na pagina;
