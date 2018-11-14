--Procurar amigos de um determinado usuario
select * from t_usuario where 
					user_id in(select t_amizade.usuario2 from t_amizade 
														inner join t_usuario  on t_amizade.usuario1 = t_usuario.user_id 
														where t_usuario.email = 'alex@outlook.com')

