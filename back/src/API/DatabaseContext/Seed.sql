INSERT INTO Assuntos ("CodA", "Nome") 
VALUES (1, 'Machado de Assis'), 
	   (2, 'Clarice Lispector');

INSERT INTO "Autores" ("CodA", "Nome") 
VALUES (1, 'Machado de Assis'), 
	   (2, 'Clarice Lispector');

INSERT INTO "Livros" ("CodL", "Titulo", "Ano") 
VALUES (1, 'Dom Casmurro', 1899),
	   (2, 'A Hora da Estrela', 1977);

INSERT INTO "LivroAutores" ("LivroId", "AutorId") 
VALUES (1, 1),
	   (2, 2);