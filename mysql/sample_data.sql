/* USE NAMESPACE */
USE `musicdb`;

/* DELETE CONSTRAINTS TMP */
ALTER TABLE `song` DROP CONSTRAINT `song-album`;
ALTER TABLE `album` DROP CONSTRAINT `album-artist`;

/* DELETE OLD DATA */
TRUNCATE TABLE `song`;
ALTER TABLE `song` AUTO_INCREMENT = 1;

TRUNCATE TABLE `album`;
ALTER TABLE `album` AUTO_INCREMENT = 1;

TRUNCATE TABLE `artist`;
ALTER TABLE `artist` AUTO_INCREMENT = 1;

/* READD CONSTRAINTS */
ALTER TABLE `album` ADD CONSTRAINT `album-artist` FOREIGN KEY (`artistID`) REFERENCES `artist` (`artistID`) ON DELETE RESTRICT ON UPDATE CASCADE;
ALTER TABLE `song` ADD CONSTRAINT `song-album` FOREIGN KEY (`albumID`) REFERENCES `album` (`albumID`) ON DELETE RESTRICT ON UPDATE CASCADE;


/* INSERT NEW DEFAULT DATA */
INSERT INTO `artist`(`fname`, `lName`, `aName`, `password`)
VALUES
("Samuel", "Koob", "AMOGUS", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Sebastian", "Zill", "DJ Firehydrant", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Mirco", "Janisch", "ca$h-flow-069", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Max", ", the man, Mustermann", "DJ REEEEE", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq");

INSERT INTO `album`(`releaseDate`, `misc`, `albumName`, `artistID`)
VALUES
("1683374800", "", "SUZZY BEATS", 1),
("1683372800", "Christmas Edition", "Last Christmas", 2),
("1683314800", "Now with 69", "PekkaRockz", 3);

INSERT INTO `song`(`title`, `length`, `albumID`)
VALUES
("Title1", 120, 1),
("Title2", 180, 1),
("Title3", 200, 1),
("Title1", 200, 2),
("Title2", 200, 2),
("Title1", 200, 3);
