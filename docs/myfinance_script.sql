CREATE DATABASE myfinance

use myfinance

create table planoconta(
id int identity(1,1) not null,
descricao varchar(50) not null,
tipo char(1) not null,
primary key(id)
);

create table transacao(
id int identity(1,1) not null,
data datetime not null,
valor decimal(9,2),
historico text,
planocontaid int not null,
primary key(id),
foreign key(planocontaid) references planoconta(id)
);

SELECT id, data, valor, historico, planocontaid FROM transacao

insert into planoconta(descricao, tipo)
values('Combustivel', 'D');
insert into planoconta(descricao, tipo)
values('Alimentação', 'D');
insert into planoconta(descricao, tipo)
values('Plano de Saúde', 'D');
insert into planoconta(descricao, tipo)
values('IPTU', 'D');
insert into planoconta(descricao, tipo)
values('Salário', 'R');
insert into planoconta(descricao, tipo)
values('Dividendos de Ações', 'R');

SELECT * FROM planoconta

insert into transacao(data, valor, historico, planocontaid)
values('20230214 21:47', 100, 'Gasolina Blazer',1)

insert into transacao(data, valor, historico, planocontaid)
values('20230110 20:47', 345.67, 'Gasolina Blazer',1)

insert into transacao(data, valor, historico, planocontaid)
values('20230110 20:47', 35, 'Almoço',2)

insert into transacao(data, valor, historico, planocontaid)
values('20230210 20:47', 300, 'Almoço',3)

insert into transacao(data, valor, historico, planocontaid)
values('20230105 07:00', 1000, 'Pró-Labore',5)

insert into transacao(data, valor, historico, planocontaid)
values('20230115 07:00', 0.50, 'Americanas',5)

SELECT * FROM transacao


--Todas as transações de Despesas no mês de janeiro
select t.data, t.valor, p.descricao
from transacao t
inner join planoconta p on t.planocontaid = p.id
where p.tipo = 'D' and t.data >= '20230101' and t.data <='20230131'

--Todas as transações maiores que 200 reais
SELECT * FROM transacao where valor > 200

SELECT * FROM planoconta

--Somatório de transações de receitas e despesas de todo o período
select sum(t.valor) as total FROM 
planoconta p 
inner join transacao t 
on p.id = t.planocontaid
--- GROUP BY
select p.tipo as tipo, sum(t.valor) as total FROM 
planoconta p 
inner join transacao t 
on p.id = t.planocontaid
group by p.tipo
---- SUBQUERY
SELECT d.total_despesas, r.total_receitas from 
(select sum(valor) as total_despesas
from transacao t join planoconta p on p.id = t.planocontaid
where p.tipo = 'D') as d,
(select sum(valor) as total_receitas
from transacao t join planoconta p on p.id = t.planocontaid
where p.tipo = 'R') as r

--Média de transações por mês
SELECT * FROM transacao
SELECT * FROM planoconta

select avg(valor) as media,MONTH(data) as mes from 
transacao 
group by MONTH(data);

--Transações e seus itens de planos de contas
select t.id,t.data,t.valor,p.descricao, p.tipo
from transacao t join planoconta p
on t.planocontaid = p.id