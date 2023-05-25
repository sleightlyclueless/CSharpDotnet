/* DROP PREV SCHEMA / DATABASE */
DROP DATABASE IF EXISTS `musicdb`;

/* CREATE DATABASE */
CREATE DATABASE `musicdb`;

/* USE DATABASE AS NAMESPACE */
/* This prevents having to write stuff like `musicdb`.`artist` etc. all the time */
USE `musicdb`;

/* DROP PREVIOUS TABLES IF THEY SURVIVED SOMEHOW */
DROP TABLE IF EXISTS `artist`;
DROP TABLE IF EXISTS `album`;
DROP TABLE IF EXISTS `song`;

/* CREATE TABLES ANEW */
CREATE TABLE `artist` (
  `artistID` int NOT NULL AUTO_INCREMENT,
  `fName` varchar(32) DEFAULT NULL,
  `lName` varchar(32) DEFAULT NULL,
  `aName` varchar(32) NOT NULL UNIQUE,
  `password` varchar(256) NOT NULL,
  PRIMARY KEY (`artistID`),
  UNIQUE KEY `ARTIST_PK` (`artistID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
ALTER TABLE `artist` AUTO_INCREMENT = 1;

CREATE TABLE `album` (
  `albumID` int NOT NULL AUTO_INCREMENT,
  `releaseDate` bigint NOT NULL,
  `misc` varchar(256) DEFAULT NULL,
  `albumName` varchar(32) NOT NULL,
  `artistID` int DEFAULT NULL,
  UNIQUE KEY `ALBUM_PK` (`albumID`),
  UNIQUE KEY `albumName_UNIQUE` (`albumName`),
  KEY `artistID_idx` (`artistID`),
  CONSTRAINT `album-artist` FOREIGN KEY (`artistID`) REFERENCES `artist` (`artistID`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
ALTER TABLE `album` AUTO_INCREMENT = 1;

CREATE TABLE `song` (
  `songID` int NOT NULL AUTO_INCREMENT,
  `title` varchar(32) NOT NULL,
  `length` int NOT NULL,
  `albumID` int NOT NULL,
  PRIMARY KEY (`songID`),
  UNIQUE KEY `SONG_PK` (`songID`),
  KEY `song-album_idx` (`albumID`),
  CONSTRAINT `song-album` FOREIGN KEY (`albumID`) REFERENCES `album` (`albumID`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
ALTER TABLE `song` AUTO_INCREMENT = 1;