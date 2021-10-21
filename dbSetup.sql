CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS recipes(
  id int NOT NULL primary key AUTO_INCREMENT COMMENT 'primary key',
  name varchar(255) COMMENT 'User Name',
  creatorId VARCHAR(255) COMMENT 'User Id',
  Instructions varchar(255) COMMENT 'Recipe Instructions',

  FOREIGN KEY(creatorId) REFERENCES accounts(Id)
) default charset utf8 COMMENT '';
Drop TABLE recipes;
DROP TABLE accounts;

SELECT * FROM accounts;
SELECT
      r.*,
      a.*
      FROM recipes r
      JOIN accounts a on a.id = r.creatorId
      WHERE r.id = @recipeId;
