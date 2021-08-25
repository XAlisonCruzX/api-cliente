select  * from REDES_SOCIAIS 


insert into CLIENTES(NOME,DATA_NASCIMENTO,CPF,RG) values ('sena', '2021-08-10 20:00','23156','1651561')

insert into REDES_SOCIAIS (NOME,REFERENCIA,ID_CLIENTE) values ('face', 'sena.cruz', 22)


insert into TELEFONES(TIPO,NUMERO,ID_CLIENTE) values ('Residen', '19998999', 22)

insert into ENDERECOS(TIPO,CEP,RUA, NUMERO,ID_CLIENTE) values ('Comercial', '19998999', 'r aaaa', '35a', 22)

Delete from REDES_SOCIAIS where id > 3;

Delete from CLIENTES where id != 1;

SELECT * FROM CLIENTES 
SELECT * FROM ENDERECOS
SELECT * FROM REDES_SOCIAIS
SELECT * FROM TELEFONES

DELETE FROM CLIENTES WHERE ID > 0; 