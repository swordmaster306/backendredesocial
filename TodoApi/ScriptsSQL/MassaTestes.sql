
------Usuarios de teste
insert into t_usuario(nome,email,senha) values ('Alex','alex@outlook.com','alex123');
insert into t_usuario(nome,email,senha) values ('Daniel','daniel@outlook.com','daniel123');
insert into t_usuario(nome,email,senha) values ('Henrique','henrique@outlook.com','henrique123');
insert into t_usuario(nome,email,senha) values ('Helder','helder@outlook.com','helder123');
insert into t_usuario(nome,email,senha) values ('Lucas','lucas@outlook.com','lucas123');
insert into t_usuario(nome,email,senha) values ('Vitor','vitor@outlook.com','vitor123');
insert into t_usuario(nome,email,senha) values ('Jose','jose@outlook.com','jose123');
insert into t_usuario(nome,email,senha) values ('Pedro','pedro@outlook.com','pedro123');





------Amizades de teste
insert into t_amizade(usuario1,usuario2,status) values(0,1,'Aprovada');
insert into t_amizade(usuario1,usuario2,status) values(1,0,'Aprovada');

insert into t_amizade(usuario1,usuario2,status) values(0,2,'Aprovada');
insert into t_amizade(usuario1,usuario2,status) values(2,0,'Aprovada');

insert into t_amizade(usuario1,usuario2,status) values(0,3,'Aprovada');
insert into t_amizade(usuario1,usuario2,status) values(3,0,'Aprovada');

insert into t_amizade(usuario1,usuario2,status) values(0,4,'Aprovada');
insert into t_amizade(usuario1,usuario2,status) values(4,0,'Aprovada');

insert into t_amizade(usuario1,usuario2,status) values(1,2,'Aprovada');
insert into t_amizade(usuario1,usuario2,status) values(2,1,'Aprovada');


insert into t_amizade(usuario1,usuario2,status) values(4,5,'Pendente');
insert into t_amizade(usuario1,usuario2,status) values(5,4,'Pendente');

insert into t_amizade(usuario1,usuario2,status) values(5,6,'Aprovada');
insert into t_amizade(usuario1,usuario2,status) values(6,5,'Aprovada');

insert into t_amizade(usuario1,usuario2,status) values(7,0,'Aprovada');
insert into t_amizade(usuario1,usuario2,status) values(0,7,'Aprovada');

insert into t_amizade(usuario1,usuario2,status) values(7,3,'Negada');
insert into t_amizade(usuario1,usuario2,status) values(3,7,'Negada');

insert into t_amizade(usuario1,usuario2,status) values(6,7,'Pendente');
insert into t_amizade(usuario1,usuario2,status) values(7,6,'Pendente');


-------Historias de teste

insert into t_historia(user_id,mensagem) values(0,'Esse RED DEAD REDEMPTION 2 EH BOM DEMAIS PQP');
insert into t_historia(user_id,mensagem) values(1,'Gosto de python');
insert into t_historia(user_id,mensagem) values(2,'Javascript é um krl, c# é muito bravo');
insert into t_historia(user_id,mensagem) values(3,'Só falo merda');
insert into t_historia(user_id,mensagem) values(6,'Procurando um emprego que preste');
insert into t_historia(user_id,mensagem) values(7,'Preciso comer um hamburguer gourmet');



-------Like_Dislike de teste

insert into t_like_dislike(user_id,historia_id,like_dislike) values(3,0,1)
insert into t_like_dislike(user_id,historia_id,like_dislike) values(1,0,0)
insert into t_like_dislike(user_id,historia_id,like_dislike) values(1,4,1)
insert into t_like_dislike(user_id,historia_id,like_dislike) values(4,5,0)
insert into t_like_dislike(user_id,historia_id,like_dislike) values(5,5,1)
insert into t_like_dislike(user_id,historia_id,like_dislike) values(7,2,1)
insert into t_like_dislike(user_id,historia_id,like_dislike) values(1,3,0)
insert into t_like_dislike(user_id,historia_id,like_dislike) values(4,4,0)



-------Comentarios de teste

insert into t_comentario(historia_id,user_id,mensagem) values (0,1,'Bom mesmo!') 
insert into t_comentario(historia_id,user_id,mensagem) values (0,2,'Concordo') 
insert into t_comentario(historia_id,user_id,mensagem) values (0,3,'Foda demais!') 
insert into t_comentario(historia_id,user_id,mensagem) values (4,0,'De fato') 
insert into t_comentario(historia_id,user_id,mensagem) values (3,0,'Calma shodi') 
insert into t_comentario(historia_id,user_id,mensagem) values (3,3,'Tb acho, na moral...') 
insert into t_comentario(historia_id,user_id,mensagem) values (3,1,'Não uso muito, mas sei lá, vamos fazer um simulador de Dória') 