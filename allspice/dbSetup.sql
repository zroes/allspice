CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8mb4 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes (
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        title VARCHAR(255) NOT NULL,
        img VARCHAR(255) NOT NULL,
        instructions VARCHAR(3000) NOT NULL DEFAULT 'Wow so lazy no instructions',
        category VARCHAR(255) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

DROP TABLE recipes;

CREATE TABLE
    ingredients (
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        name VARCHAR(255) NOT NULL,
        quantity VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL
    ) DEFAULT CHARSET utf8mb4 COMMENT 'only the finest';

INSERT INTO
    recipes(title, img, category, creatorId)
VALUES (
        "Easy Eggs",
        "https://fitfoodiefinds.com/wp-content/uploads/2022/10/Over-Easy-Eggs-sq.jpg",
        "breakfast",
        "644301a5f2cfdf42d020c3eb"
    );

CREATE TABLE
    favorites(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        recipeId INT NOT NULL,
        accountId VARCHAR(255) NOT NULL,
        FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
        FOREIGN KEY(accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        UNIQUE(recipeId, accountId)
    ) DEFAULT CHARSET utf8mb4 COMMENT '';

INSERT INTO
    favorites(recipeId, accountId)
VALUES (1, '644301a5f2cfdf42d020c3eb');

SELECT fav.*, acct.*
FROM favorites fav
    JOIN accounts acct ON fav.accountId = acct.id
WHERE fav.recipeId = 1;