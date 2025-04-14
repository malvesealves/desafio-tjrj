INSERT INTO "Assuntos" ("CodAs", "Descricao") 
VALUES (1, 'Literatura nacional'),
	   (2, 'Arquitetura'),
	   (3, 'Culinária')
ON CONFLICT ("CodAs") DO NOTHING;


INSERT INTO "Autores" ("CodAu", "Nome") 
VALUES (1, 'Machado de Assis'), 
	   (2, 'Clarice Lispector')
ON CONFLICT ("CodAu") DO NOTHING;

INSERT INTO "FormasCompra" ("CodForComp", "Descricao")
VALUES (1, 'Balcão'),
	   (2, 'Self-service'),
	   (3, 'Internet'),
	   (4, 'Evento')
ON CONFLICT ("CodForComp") DO NOTHING;

INSERT INTO "Livros" ("CodL", "Titulo", "AnoPublicacao", "Editora", "Edicao", "Preco", "AssuntoId", "FormaCompraId") 
VALUES (1, 'Dom Casmurro', 1899, 'Moderna', 5, 59.90, 1, 3),
	   (2, 'A Hora da Estrela', 1977, 'Rocco', 1, 89.90, 1, 3)
ON CONFLICT ("CodL") DO NOTHING;

INSERT INTO "LivrosAutores" ("CodL", "CodAu") 
VALUES (1, 1),
	   (2, 2)
ON CONFLICT ("CodL", "CodAu") DO NOTHING;