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

/* RE-ADD CONSTRAINTS */
ALTER TABLE `album` ADD CONSTRAINT `album-artist` FOREIGN KEY (`artistID`) REFERENCES `artist` (`artistID`) ON DELETE RESTRICT ON UPDATE CASCADE;
ALTER TABLE `song` ADD CONSTRAINT `song-album` FOREIGN KEY (`albumID`) REFERENCES `album` (`albumID`) ON DELETE RESTRICT ON UPDATE CASCADE;


/* INSERT NEW DEFAULT DATA */
INSERT INTO `artist`(`fname`, `lName`, `aName`, `password`)
VALUES
("Samuel", "Koob", "AMOGUS", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Sebastian", "Zill", "DJ Firehydrant", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Mirco", "Janisch", "ca$h-flow-069", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Adam", "Angst", "Love & Bass", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Björn", "Bergmann", "Benderman", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Christian", "Crumminger", "Club Masta", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Daniel", "Dreißner", "DJ Divertido", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq"),
("Max", ", the man, Mustermann", "MutantBeatz", "$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq");

INSERT INTO `album`(`releaseDate`, `misc`, `albumName`, `artistID`)
VALUES
("1683374800", "", "SUZZY BEATS", 1),
("1683372800", "Christmas Edition", "Last Christmas", 1),
("1683375327", "Deluxe Edition", "Sebibel", 2),
("1683335963", "", "Mircos Beats", 3),
("1683305728", "$$$", "Ca$h in the club", 3),
("1683330582", "", "Adams ästhetische Äpfel", 4),
("1683312495", "", "Alle Jahre", 4),
("1683204863", "", "Come closer", 6),
("1683369702", "", "Chrisis Fire", 6),
("1683313900", "WasweißichaltermirgehendieIdeenaus", "RizzPlays", 8);

INSERT INTO `song`(`title`, `length`, `albumID`)
VALUES
("Arrival in Space", 120, 1),
("Imposters everywhere", 180, 1),
("The lights are out", 150, 1),
("They fall like flies", 144, 1),
("Ejected by false friends", 100, 1),
("Electrical genocide", 80, 1),
("Betrayed in Cams", 200, 1),
("The imposters won", 200, 1),
("How could this happen to me", 190, 1),
("Sebis-Song1", 200, 2),
("Sebis last song already", 320, 2),
("Cash starts flowing", 120, 3),
("Ca$h keeps going", 130, 3),
("Casch stops showing", 140, 3),
("The garden of eden", 400, 4),
("God kicked us out :(", 110, 4),
("Chrisis Welle", 200, 6),
("Christians Strom", 220, 6),
("Christians Sturm", 215, 6),
("Toll, die Socken sind nass...", 10, 6),
("Ears are rizzing", 154, 7),
("Hide your... and shes gone", 143, 7),
("Ne ernsthaft keine Ahnung", 143, 10),
("WIRKLICH", 143, 10);