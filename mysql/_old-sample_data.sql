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
("Samuel", "Koob", "AMOGUS", "80C5E536EEC8387CCCAD28B8B17B933832244998D85918ABF18CC9BADA5D4FE9"),               /*1*/
("Sebastian", "Zill", "DJ Firehydrant", "80C5E536EEC8387CCCAD28B8B17B933832244998D85918ABF18CC9BADA5D4FE9"),    /*2*/
("Mirco", "Janisch", "ca$h-flow-069", "80C5E536EEC8387CCCAD28B8B17B933832244998D85918ABF18CC9BADA5D4FE9"),      /*3*/
("Adam", "Angst", "Love & Bass", "80C5E536EEC8387CCCAD28B8B17B933832244998D85918ABF18CC9BADA5D4FE9"),           /*4*/
("Björn", "Bergmann", "Benderman", "80C5E536EEC8387CCCAD28B8B17B933832244998D85918ABF18CC9BADA5D4FE9"),         /*5*/
("Christian", "Crumminger", "Club Masta", "80C5E536EEC8387CCCAD28B8B17B933832244998D85918ABF18CC9BADA5D4FE9"),  /*6*/
("Daniel", "Dreißner", "DJ Divertido", "80C5E536EEC8387CCCAD28B8B17B933832244998D85918ABF18CC9BADA5D4FE9"),     /*7*/
("Max", ", the man, Mustermann", "MutantBeatz", "80C5E536EEC8387CCCAD28B8B17B933832244998D85918ABF18CC9BADA5D4FE9"); /*8*/

INSERT INTO `album`(`releaseDate`, `misc`, `albumName`, `artistID`)
VALUES
("1683374800", "", "SUZZY BEATS", 1),                                                                        /*1*/
("1683372800", "Christmas Edition", "Last Christmas", 2),                                                    /*2*/
("1683375327", "Deluxe Edition", "Sebibel", 2),                                                              /*3*/
("1683335963", "", "Mircos Beats", 3),                                                                       /*4*/
("1683305728", "$$$", "Ca$h in the club", 3),                                                                /*5*/
("1683330582", "", "Adams ästhetische Äpfel", 4),                                                            /*6*/
("1683312495", "", "Alle Jahre", 4),                                                                         /*7*/
("1683204863", "", "Come closer", 6),                                                                        /*8*/
("1683369702", "", "Chrisis Fire", 6),                                                                       /*9*/
("1683313900", "WasweißichaltermirgehendieIdeenaus", "RizzPlays", 8);                                       /*10*/

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
("Cash starts flowing", 120, 5),
("Ca$h keeps going", 130, 5),
("Casch stops showing", 140, 5),
("The garden of eden", 400, 6),
("God kicked us out :(", 110, 6),
("Chrisis Welle", 200, 9),
("Christians Strom", 220, 9),
("Christians Sturm", 215, 9),
("Toll, die Socken sind nass...", 10, 9),
("Ne ernsthaft keine Ahnung", 143, 10),
("WIRKLICH", 143, 10);