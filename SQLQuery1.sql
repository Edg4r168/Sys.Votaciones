DROP DATABASE IF EXISTS PROYECTO_VOTACIONES;
CREATE DATABASE PROYECTO_VOTACIONES;
USE PROYECTO_VOTACIONES;


DROP TABLE IF EXISTS A�O;
CREATE TABLE A�O(
	ID INT PRIMARY KEY IDENTITY(1,1),
	A�O_CARRERA VARCHAR(10)  NOT NULL
);

DROP TABLE IF EXISTS TOTAL_A�OS;
CREATE TABLE TOTAL_A�OS(
	ID INT PRIMARY KEY IDENTITY(1,1),
	TOTAL INT  NOT NULL
);

INSERT INTO TOTAL_A�OS (TOTAL) VALUES (0);

DROP TABLE IF EXISTS CARRERA;
CREATE TABLE CARRERA(
	ID INT PRIMARY KEY IDENTITY(1,1),
	NOMBRE VARCHAR(60) NOT NULL
);

DROP TABLE IF EXISTS TOTAL_CARRERAS;
CREATE TABLE TOTAL_CARRERAS(
	ID INT PRIMARY KEY IDENTITY(1,1),
	TOTAL INT  NOT NULL
);

INSERT INTO TOTAL_CARRERAS (TOTAL) VALUES (0);

DROP TABLE IF EXISTS ESTUDIANTE;
CREATE TABLE ESTUDIANTE(
	CODIGO VARCHAR(20) PRIMARY KEY,
	ID_A�O INT REFERENCES A�O(ID),
	ID_CARRERA INT REFERENCES CARRERA(ID),
	CONTRASE�A VARCHAR(40) NOT NULL
);

DROP TABLE IF EXISTS TOTAL_ESTUDIANTES;
CREATE TABLE TOTAL_ESTUDIANTES(
	ID INT PRIMARY KEY IDENTITY(1,1),
	TOTAL INT  NOT NULL
);

INSERT INTO TOTAL_ESTUDIANTES (TOTAL) VALUES (0);

DROP TABLE IF EXISTS CATEGORIA;
CREATE TABLE CATEGORIA(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	NOMBRE VARCHAR(100) NOT NULL
);

DROP TABLE IF EXISTS TOTAL_CATEGORIAS;
CREATE TABLE TOTAL_CATEGORIAS(
	ID INT PRIMARY KEY IDENTITY(1,1),
	TOTAL INT  NOT NULL
);

INSERT INTO TOTAL_CATEGORIAS (TOTAL) VALUES (0);

DROP TABLE IF EXISTS TIPOCONCURSO;
CREATE TABLE TIPOCONCURSO(
	ID INT PRIMARY KEY IDENTITY(1,1),
	NOMBRE VARCHAR(100) NOT NULL
);

DROP TABLE IF EXISTS TOTAL_TIPOSCONCURSO;
CREATE TABLE TOTAL_TIPOSCONCURSO(
	ID INT PRIMARY KEY IDENTITY(1,1),
	TOTAL INT  NOT NULL
);

INSERT INTO TOTAL_TIPOSCONCURSO(TOTAL) VALUES (0);

DROP TABLE IF EXISTS CATEGORIACONCURSO;
CREATE TABLE CATEGORIACONCURSO(
	ID_TIPOCONCURSO INT NOT NULL,
	ID_CATEGORIA INT NOT NULL,
);


DROP TABLE IF EXISTS CONCURSO;
CREATE TABLE CONCURSO(
	ID INT PRIMARY KEY IDENTITY(1,1),
	NOMBRE VARCHAR(100) NOT NULL,
	DESCRIPCION VARCHAR(255) NOT NULL,
	ESTADO BIT NOT NULL,
	ID_TIPOCONCURSO INT REFERENCES TIPOCONCURSO(ID)
);

DROP TABLE IF EXISTS TOTAL_CONCURSOS;
CREATE TABLE TOTAL_CONCURSOS(
	ID INT PRIMARY KEY IDENTITY(1,1),
	TOTAL INT  NOT NULL
);

INSERT INTO TOTAL_CONCURSOS(TOTAL) VALUES (0);

DROP TABLE IF EXISTS PARTICIPANTE;
CREATE TABLE PARTICIPANTE(
	ID INT PRIMARY KEY IDENTITY(1,1)NOT NULL,
	NOMBRE VARCHAR(100)NOT NULL,
	APELLIDO VARCHAR(100) NOT NULL,
	CODIGO VARCHAR(100) NOT NULL,
	ID_CONCURSO INT REFERENCES CONCURSO(ID)
);

DROP TABLE IF EXISTS TOTAL_PARTICIPANTES;
CREATE TABLE TOTAL_PARTICIPANTES(
	ID INT PRIMARY KEY IDENTITY(1,1),
	TOTAL INT  NOT NULL
);

INSERT INTO TOTAL_PARTICIPANTES (TOTAL) VALUES (0);
	

DROP TABLE IF EXISTS VOTO;
CREATE TABLE VOTO(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ID_ESTUDIANTE VARCHAR(20) REFERENCES ESTUDIANTE(CODIGO),
	ID_PARTICIPANTE INT REFERENCES PARTICIPANTE(ID)
);

CREATE TABLE ADMINISTRADOR(
	ID INT PRIMARY KEY IDENTITY(1,1),
	USUARIO VARCHAR(30) NOT NULL,
	CONTRASE�A VARCHAR(40) NOT NULL
);

INSERT INTO ADMINISTRADOR (USUARIO, CONTRASE�A)
			VALUES ('Admin' , '1234567890');


/**********************************************/
/************ Stored procedures ***************/
/**********************************************/
DROP PROCEDURE IF EXISTS sp_UpdateYear;
CREATE PROCEDURE sp_UpdateYear
(
	@id INT,
	@careerYear VARCHAR(10)
)
AS
BEGIN
	UPDATE A�O SET
		A�O_CARRERA = ISNULL(@careerYear, A�O_CARRERA)
	WHERE ID = @id
END;


DROP PROCEDURE IF EXISTS sp_UpdateCareer;
CREATE PROCEDURE sp_UpdateCareer
(
	@id INT,
	@name VARCHAR(60)
)
AS
BEGIN
	UPDATE CARRERA SET 
		NOMBRE = ISNULL(@name, NOMBRE)
	WHERE ID = @id
END;


DROP PROCEDURE IF EXISTS sp_UpdateStudent;
CREATE PROCEDURE sp_UpdateStudent
(
	@studentCode VARCHAR(20),
	@yearId INT ,
	@careerId INT,
	@password VARCHAR(40)  
)
AS
BEGIN
	UPDATE ESTUDIANTE SET
		ID_A�O = ISNULL(@yearId, ID_A�O),
		ID_CARRERA = ISNULL(@careerId, ID_CARRERA),
		CONTRASE�A = ISNULL(@password, CONTRASE�A)
	WHERE CODIGO = @studentCode
END;


DROP PROCEDURE IF EXISTS sp_UpdateContest;
CREATE PROCEDURE sp_UpdateContest
(
  @id INT,
  @name VARCHAR(100),
  @description VARCHAR(255),
  @state BIT,
  @typeContestId INT
)
AS
BEGIN
  UPDATE CONCURSO 
     SET NOMBRE = ISNULL(@name, NOMBRE),
         DESCRIPCION = ISNULL(@description, DESCRIPCION),
         ESTADO = ISNULL(@state, ESTADO),
		 ID_TIPOCONCURSO = ISNULL(@typeContestId, ID_TIPOCONCURSO)
     WHERE ID = @id
END;

DROP PROCEDURE IF EXISTS sp_UpdateTypeContest;
CREATE PROCEDURE sp_UpdateTypeContest
(
	@id INT,
	@name VARCHAR(100)
)
AS
BEGIN
	UPDATE TIPOCONCURSO 
		SET NOMBRE = ISNULL(@name, NOMBRE)
	WHERE ID = @id
END;

DROP PROCEDURE IF EXISTS sp_UpdateCategory;
CREATE PROCEDURE sp_UpdateCategory
(
	@id INT,
	@name VARCHAR(100)
)
AS
BEGIN
	UPDATE CATEGORIA 
		SET NOMBRE = ISNULL(@name, NOMBRE)
	WHERE ID = @id
END;


DROP PROCEDURE IF EXISTS sp_UpdateParticipant;
CREATE PROCEDURE sp_UpdateParticipant
(
	@id INT,
	@name VARCHAR(100),
	@lastName VARCHAR(100),
	@code VARCHAR(100),
	@constestId INT
)
AS
BEGIN
	UPDATE PARTICIPANTE 
		SET NOMBRE = ISNULL(@name, NOMBRE),
			APELLIDO = ISNULL(@lastName, APELLIDO),
			CODIGO = ISNULL(@code, CODIGO),
			ID_CONCURSO = ISNULL(@constestId, ID_CONCURSO)
	WHERE ID = @id
END;


/**********************************************/
/**********************************************/
SELECT s.*, y.A�O_CARRERA, c.NOMBRE 
	FROM ESTUDIANTE s
	JOIN A�O y ON s.ID_A�O = y.ID
	JOIN CARRERA c ON s.ID_CARRERA = c.ID;

SELECT s.*, y.A�O_CARRERA, c.NOMBRE
    FROM ESTUDIANTE s 
    JOIN A�O y ON s.ID_A�O = y.ID
    JOIN CARRERA c ON s.ID_CARRERA = c.ID
    WHERE s.CODIGO = 'SL0054';


SELECT con.*, cat.NOMBRE
	FROM CONCURSO con
	JOIN CATEGORIA cat ON con.ID_CATEGORIA = cat.ID;

SELECT con.*, cat.NOMBRE
	FROM CONCURSO con
	JOIN CATEGORIA cat ON con.ID_CATEGORIA = cat.ID
	WHERE con.ID = 1;

SELECT * FROM CATEGORIA ORDER BY ID OFFSET 0 ROWS FETCH NEXT 6 ROWS ONLY;

INSERT INTO CATEGORIA (NOMBRE) VALUES ('Pintura'); SELECT SCOPE_IDENTITY() AS ID;

SELECT s.*, y.A�O_CARRERA, c.NOMBRE
FROM ESTUDIANTE s
JOIN A�O y ON s.ID_A�O = y.ID 
JOIN CARRERA c ON  s.ID_CARRERA = c.ID
WHERE CODIGO LIKE 's' + '%'

SELECT con.*, tcon.NOMBRE 
FROM CONCURSO con 
JOIN TIPOCONCURSO tcon ON con.ID_TIPOCONCURSO = tcon.ID
WHERE con.NOMBRE LIKE 'h' + '%'
