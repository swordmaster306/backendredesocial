create table t_usuario(
	user_id integer identity(0,1) not null primary key ,
	nome varchar(255) not null ,
	email varchar(255) not null ,
	senha varchar(255) not null,
	foto_perfil varchar(Max)
);


create table t_amizade(
	id integer identity(0,1) not null primary key,
	usuario1 integer not null,
	usuario2 integer not null,
	status varchar(20),
	constraint FK_amizade_usuario1 foreign key(usuario1) references t_usuario(user_id),
	constraint FK_amizade_usuario2 foreign key(usuario2) references t_usuario(user_id)
);

create table t_historia(
	id integer identity(0,1) not null primary key,
	user_id integer not null,
	mensagem nvarchar(MAX),
	foto varchar(MAX),
	data datetime default current_timestamp,
	constraint FK_historia_user_id foreign key(user_id) references t_usuario(user_id)
)

create table t_like_dislike(
	id integer identity(0,1) not null primary key,
	user_id integer not null,
	historia_id integer not null,
	like_dislike bit,
	constraint FK_like_dislike_user_id foreign key(user_id) references t_usuario(user_id),
	constraint FK_like_dislike_historia_id foreign key(historia_id) references t_historia(id),
)



create table t_comentario(
	id integer identity(0,1) not null primary key,
	historia_id integer not null,
	user_id integer not null,
	mensagem nvarchar(MAX),
	data datetime default current_timestamp,
	constraint FK_comentario_historia_id foreign key (historia_id) references t_historia(id),
	constraint FK_comentario_user_id foreign key (user_id) references t_usuario(user_id)
)

