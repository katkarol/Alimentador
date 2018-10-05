# PetFeeder
> Desenvolvido por Kathlen, Andrieli, Eduardo e Amanda, alunos da turma de **C#** do Entra21 2018, este é o **PetFeeder**.
## O que é
É um alimentador reciclado com feito em _Arduino_, pensamos
em fazer isso para qualquer um poder fazer e com um bom custo benefício.
> É fácil e qualquer um pode fazer.
## Como foi feito
### Hardware
Os componentes eletrônicos foram:
* Um [Arduino Uno](https://en.wikipedia.org/wiki/Arduino_Uno "Wikipedia");
* Um [Motor de Passo](https://pt.wikipedia.org/wiki/Motor_de_passo "Wikipedia"). 
### Software
As ferramentas de desenvolvimento utilizadas foram:
* [Visual Studio](https://visualstudio.microsoft.com/ "Mais sobre a IDE") para o desenvolvimento de todo o código em torno do C#;
* [Arduino IDE](https://www.arduino.cc/en/Main/Software "Mais sobre a IDE") para o desenvolvimento do programa do Arduino;
* Sistema Gerenciador de Bancos de Dados (SGBD) [Microsoft SQL Server](https://pt.wikipedia.org/wiki/Microsoft_SQL_Server "Wikipedia");
* ORM [Entity Framework](https://docs.microsoft.com/pt-br/dotnet/framework/data/adonet/ef/overview "Documentação do EF").
## Arduino
### Motor de Passo
Na parte de código do arduino, testamos uma biblioteca para o motor de passo. Porém para o controle das aberturas, não funcionou da forma que queríamos. Então, primeiro estudamos como funciona o motor de passo.
Criamos uma função para acionamento na ordem correta das bobinas
motor.
O motor de passo funciona através de lógicas digitais, pois é através do acionamento em sequência das bobinas que se consegue adquirir movimento.
Por isso definimos as cores do cabo que tem no motor e os números do lado são as portas do arduino que o motor está conectado.
Quando um manda o pulso o outro conta os números de pulso e assim vai sucessivamente até atingir os tipos de aberturas que programamos.
### Loops
Na função loop criamos dois tipos de `for`, um que tem uma abertura que vai até 1024 passo e um até 1023 para ele fechar, usamos 1023 porque com 1024 ele não voltava corretamente ao eixo. Fizemos outro `for` com 512 e um com 511 para fechar.
### Serial
[Serial](https://en.wikipedia.org/wiki/Serial_communication "Wikipedia") é um protocolo para comunicação de dispositivos que vem como padrão em quase todo PC e é utilizado por muitos dispositivos para instrumentação. A maioria inclui duas portas seriais baseadas em [RS-232](https://en.wikipedia.org/wiki/RS-232).
## Database
### Entity Framework
O Entity Framework é um compilado de tecnologias que nos auxilia ao desenvolver aplicações com bancos de dados.
A ferramenta cria para nós, maneiras de enxergar nossas tabelas em forma de objetos e nos auxilia a fazer operações no banco de dados. Sendo assim, escrevemos menos código para realizar tais operações.
Além da facilidade em trabalhar as operações com o banco, o Entity Framework nos permite a geração do nosso banco de dados a partir do nosso código, ou seja, não precisamos de tabelas previamente criadas para iniciar o desenvolvimento de nossa aplicação, podemos desenvolver a camada de acesso a dados utilizando o Entity, e com o auxílio do recurso chamado [Migrations](https://msdn.microsoft.com/pt-br/library/jj856238.aspx "Artigo sobre Migrations") geramos nossas tabelas com base no nosso código. Esta forma de trabalho é conhecida como Code First, onde criamos o código da aplicação antes de criarmos o banco de dados.
## Interface
### Menu
Na parte da interface Menu, fizemos uma função com o `timer`, onde ele é executado a cada 1 segundo.
Se o horário atual estiver presente na lista de intervalos cadastrados ele cria o objeto `PortaSerial` com as configurações explicadas anteriormente.
Após, ele entra no `for`, para cada virada definida nos parâmetros, e transforma o caractere A ou o caractere B para um vetor de bytes, sendo eles:
* **1024** = _A_
* **512** = _B_
E por fim manda o vetor inteiro para o arduino.
Após pressionado o botão iniciar, ele começa lendo o último registro no banco de dados da tabela horários, e então chama a função `CalcularHorários` (que será explicada posteriormente), que define todos os horários aos quais a aplicação irá efetuar a comunicação com o Arduino, então, depois de definido os horários, a aplicação lê os parâmetros para calcular o número de viradas e a angulação necessária.
Por fim ele define o intervalo de 1 segundo que será usado para a checagem de horários e então inicia.
Se pressionado novamente, ele para a aplicação.
## Análise de Horários
Criamos uma função que analisa os dados do banco de inicio e fim e calcula os horários em que o arduino deve ser acionado de acordo com o campo _intervalo_. 
Por exemplo, se colocarmos 15:00 de inicio e 15:10 de fim e colocamos intervalos de 10 segundos, a função `CalcularHorarios` irá gerar uma lista de 60 horários.
## Viradas
Classe auxiliar que contém o numero de viradas e a angulação necessária para o arduino funcionar.
Ulizizanmos um `enum` de acordo com o porte do animal para que fossem efetuadas as aberturas e utilizamos as propriedades no formato `string` e `int` para serem calculadas a quantidade de viradas e o tamanho da abertura.
## Parâmetros
Classe que também tem uma função chamada `CalcularViradas` (que foi o objeto que vimos antes), que, de acordo com o tamanho do animal e o tipo do animal, calcula o numero de viradas e qual a angulação necessária, por exemplo:
> Se for filhote, vire 2 duas vezes com a angulação B (512). Mas se for filhote, vire também duas vezes, entretanto use a angulação A (1024) onde o arduino faz uma rotação maior (caindo mais ração =D)
## Considerações Finais
> Todo o trabalho foi desenvolvido durante as aulas finais do curso.
