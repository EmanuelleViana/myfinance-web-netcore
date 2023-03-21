CREATE TABLE logs(
id int identity(1,1) not null,
data datetime not null,
observacao varchar(255) not null,
tabela varchar(50) not null,
operacao char(1) not null CHECK (operacao IN('I', 'A', 'E')),
idregistro int not null,
primary key(id)
)

select * from logs;