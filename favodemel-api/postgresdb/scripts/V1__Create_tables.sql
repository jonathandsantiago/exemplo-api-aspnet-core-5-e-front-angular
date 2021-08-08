delete from "Produto";

insert into "Produto"("IdProduto", "Nome", "Preco", "Ativo")Values('7459eff5-51cf-45f4-a17c-c758cfc56dfc','Coca Cola 2LTS', 15, true),
('49dd0ba2-d474-41f6-87a2-c24e1df2156c', 'Guaraná 2LTS', 10, true),
('9755e588-c32c-4cca-9bf0-946b3c62a7d6', 'Coca Cola 350ML', 5.50, true),
('04d49bf8-c83f-412c-a620-51cc22ff69ff', 'Guaraná 350ML', 5.50, true),
('dad7cd54-b14b-4e79-916a-85b9ed804a8d', 'Heineken 250ML', 5.50, true),
('dffd160a-2a8b-4946-908a-539643d01f6d', 'Pizza G', 70, true),
('9e967016-dacb-4187-bece-10c2cc54a943', 'Pizza M', 55.90, true),
('a64bc6d4-92e7-4428-9210-cd2237129232', 'Pizza P', 35.9, true);

delete from "Usuario";
insert into "Usuario"("IdUsuario", "Nome", "Login", "Password", "Perfil", "Comissao", "Ativo")
Values('95701f5d-29cd-4a86-bdf4-30b53b411897', 'Admin', 'Admin', 'E3AFED0047B08059D0FADA10F400C1E5', 3, 0, true),
('1fb02066-3f8e-45f5-8882-a44ba66d8125', 'Garçom', 'Garçom', '93608B5DFA90227E9F3E2910CD021A4F', 1, 0, true),
('6770799e-178f-4f38-94e2-e87cbfc66790', 'Cozinheiro', 'Cozinheiro', '6962DF739EAA91873CBBBE41807072EA', 2, 0, true);