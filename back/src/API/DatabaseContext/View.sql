CREATE OR ALTER VIEW vw_livros_autores AS
SELECT
    l.Id AS livro_id,
    l.Titulo AS livro_titulo,
    l.Assunto AS livro_assunto,
    a.Id AS autor_id,
    a.Nome AS autor_nome
FROM LivroAutores la
JOIN Livros l ON la.LivroId = l.Id
JOIN AutoreS a ON la.AutorId = a.Id;