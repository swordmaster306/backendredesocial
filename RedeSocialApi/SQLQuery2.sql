create table t_usuario(
	user_id integer identity(0,1) not null primary key ,
	nome varchar(255) not null ,
	email varchar(255) not null ,
	senha varchar(255) not null,
	qtd_amigos integer default 0,
	qtd_historias integer default 0,
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
	likes integer default 0,
	dislikes integer default 0,
	qtd_comentarios integer default 0,
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

create trigger trg_t_like_dislike_ai
on t_like_dislike
after insert
as
begin
	declare
	@like_dislike bit,
	@historia_id integer,
	@current_like_dislike integer

	select @like_dislike = like_dislike, @historia_id = historia_id from inserted

	if @like_dislike = 1
	begin
		select @current_like_dislike = likes from t_historia  where id = @historia_id
		set @current_like_dislike = @current_like_dislike + 1
		update t_historia set likes = @current_like_dislike where id = @historia_id
		end
	else
	begin
		select @current_like_dislike = dislikes from t_historia  where id = @historia_id
		set @current_like_dislike = @current_like_dislike + 1;
		update t_historia set dislikes = @current_like_dislike where id = @historia_id
	end
end


create trigger trg_t_comentario_ai
on t_comentario
after insert
as
begin
	declare
	@historia_id integer,
	@current_qtd_comentario integer

	select @historia_id = historia_id from inserted

	select @current_qtd_comentario = qtd_comentarios from t_historia  where id = @historia_id
	set @current_qtd_comentario = @current_qtd_comentario + 1
	update t_historia set qtd_comentarios = @current_qtd_comentario where id = @historia_id
end




create trigger trg_t_historia_ai
on t_historia
after insert
as
begin
	declare
	@user_id integer,
	@current_qtd_historias integer

	select @user_id = user_id from inserted

	select @current_qtd_historias = qtd_historias from t_usuario  where user_id = @user_id
	set @current_qtd_historias = @current_qtd_historias + 1
	update t_usuario set qtd_historias = @current_qtd_historias where user_id = @user_id
end



create trigger trg_t_amizade_ai
on t_amizade
after update
as
begin
	declare
	@status varchar(20),
	@usuario1 integer,
	@usuario2 integer,
	@qtd_amigos1 integer,
	@qtd_amigos2 integer

	select @status = status from inserted
	select @usuario1 = usuario1 from inserted
	select @usuario2 = usuario2 from inserted
	select @qtd_amigos1 = qtd_amigos from t_usuario where user_id = @usuario1
	select @qtd_amigos2 = qtd_amigos from t_usuario where user_id = @usuario2

	if @status = 'Aprovada'
		begin
		set @qtd_amigos1 = @qtd_amigos1 + 1
		set @qtd_amigos2 = @qtd_amigos2 + 1
		update t_usuario set qtd_amigos = @qtd_amigos1 where user_id = @usuario1
		update t_usuario set qtd_amigos = @qtd_amigos2 where user_id = @usuario2
		end
	if @status = 'Desfeita'
		begin
		set @qtd_amigos1 = @qtd_amigos1 - 1
		set @qtd_amigos2 = @qtd_amigos2 - 1
		update t_usuario set qtd_amigos = @qtd_amigos1 where user_id = @usuario1
		update t_usuario set qtd_amigos = @qtd_amigos2 where user_id = @usuario2
		end

end




create trigger trg_t_like_dislike_au
on t_like_dislike
after update
as
begin
	declare
	@like_dislike bit,
	@historia_id integer,
	@current_like_dislike integer

	select @like_dislike = like_dislike, @historia_id = historia_id from inserted

	if @like_dislike = 1
	begin
		select @current_like_dislike = dislikes from t_historia  where id = @historia_id
		set @current_like_dislike = @current_like_dislike - 1
		update t_historia set dislikes = @current_like_dislike where id = @historia_id
		select @current_like_dislike = likes from t_historia  where id = @historia_id
		set @current_like_dislike = @current_like_dislike + 1
		update t_historia set likes = @current_like_dislike where id = @historia_id
		end
	else
	begin
		select @current_like_dislike = likes from t_historia  where id = @historia_id
		set @current_like_dislike = @current_like_dislike -1;
		update t_historia set likes = @current_like_dislike where id = @historia_id
		select @current_like_dislike = dislikes from t_historia  where id = @historia_id
		set @current_like_dislike = @current_like_dislike +1;
		update t_historia set dislikes = @current_like_dislike where id = @historia_id
	end
end




create trigger trg_t_like_dislike_bd
on t_like_dislike
for delete
as
begin
	declare
	@like_dislike bit,
	@historia_id integer,
	@current_like_dislike integer

	select @like_dislike = like_dislike, @historia_id = historia_id from deleted

	if @like_dislike = 1
	begin
		select @current_like_dislike = likes from t_historia  where id = @historia_id
		set @current_like_dislike = @current_like_dislike - 1
		update t_historia set likes = @current_like_dislike where id = @historia_id
		end
	else
	begin
		select @current_like_dislike = dislikes from t_historia  where id = @historia_id
		set @current_like_dislike = @current_like_dislike -1;
		update t_historia set dislikes = @current_like_dislike where id = @historia_id
	end
end






drop table t_usuario;
drop table t_amizade;
drop table t_historia;
drop table t_like_dislike;
drop table t_comentario;



select * from t_usuario;
select * from t_amizade;
select * from t_historia;
select * from t_like_dislike;
select * from t_comentario;

update t_historia set likes = 0;