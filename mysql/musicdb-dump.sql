CREATE DATABASE  IF NOT EXISTS `musicdb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `musicdb`;
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: musicdb
-- ------------------------------------------------------
-- Server version	8.0.33

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `album`
--

DROP TABLE IF EXISTS `album`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `album`
--

LOCK TABLES `album` WRITE;
/*!40000 ALTER TABLE `album` DISABLE KEYS */;
INSERT INTO `album` VALUES (1,1683374800,'','SUZZY BEATS',1),(2,1683372800,'Christmas Edition','Last Christmas',2),(3,1683375327,'Deluxe Edition','Sebibel',2),(4,1683335963,'','Mircos Beats',3),(5,1683305728,'$$$','Ca$h in the club',3),(6,1683330582,'','Adams ästhetische Äpfel',4),(7,1683312495,'','Alle Jahre',4),(8,1683204863,'','Come closer',6),(9,1683369702,'','Chrisis Fire',6),(10,1683313900,'WasweißichaltermirgehendieIdeenaus','RizzPlays',8);
/*!40000 ALTER TABLE `album` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `artist`
--

DROP TABLE IF EXISTS `artist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `artist` (
  `artistID` int NOT NULL AUTO_INCREMENT,
  `fName` varchar(32) DEFAULT NULL,
  `lName` varchar(32) DEFAULT NULL,
  `aName` varchar(32) NOT NULL,
  `password` varchar(256) NOT NULL,
  PRIMARY KEY (`artistID`),
  UNIQUE KEY `ARTIST_PK` (`artistID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `artist`
--

LOCK TABLES `artist` WRITE;
/*!40000 ALTER TABLE `artist` DISABLE KEYS */;
INSERT INTO `artist` VALUES (1,'Samuel','Koob','AMOGUS','80C5E536EEC8387CCCAD28B8B17B933832244998D85918ABF18CC9BADA5D4FE9'),(2,'Sebastian','Zill','DJ Firehydrant','$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq'),(3,'Mirco','Janisch','ca$h-flow-069','$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq'),(4,'Adam','Angst','Love & Bass','$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq'),(5,'Björn','Bergmann','Benderman','$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq'),(6,'Christian','Crumminger','Club Masta','$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq'),(7,'Daniel','Dreißner','DJ Divertido','$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq'),(8,'Max',', the man, Mustermann','MutantBeatz','$2y$10$dJyJqk4D.5xCt18C/i.YI.dTOv1Dc8Jsmdmfhz4tbTQ/y2b9TSVuq');
/*!40000 ALTER TABLE `artist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `song`
--

DROP TABLE IF EXISTS `song`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `song` (
  `songID` int NOT NULL AUTO_INCREMENT,
  `title` varchar(32) NOT NULL,
  `length` int NOT NULL,
  `albumID` int NOT NULL,
  PRIMARY KEY (`songID`),
  UNIQUE KEY `SONG_PK` (`songID`),
  KEY `song-album_idx` (`albumID`),
  CONSTRAINT `song-album` FOREIGN KEY (`albumID`) REFERENCES `album` (`albumID`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `song`
--

LOCK TABLES `song` WRITE;
/*!40000 ALTER TABLE `song` DISABLE KEYS */;
INSERT INTO `song` VALUES (1,'Arrival in Space',120,1),(2,'Imposters everywhere',180,1),(3,'The lights are out',150,1),(4,'They fall like flies',144,1),(5,'Ejected by false friends',100,1),(6,'Electrical genocide',80,1),(7,'Betrayed in Cams',200,1),(8,'The imposters won',200,1),(9,'How could this happen to me',190,1),(10,'Sebis-Song1',200,2),(11,'Sebis last song already',320,2),(12,'Cash starts flowing',120,5),(13,'Ca$h keeps going',130,5),(14,'Casch stops showing',140,5),(15,'The garden of eden',400,6),(16,'God kicked us out :(',110,6),(17,'Chrisis Welle',200,9),(18,'Christians Strom',220,9),(19,'Christians Sturm',215,9),(20,'Toll, die Socken sind nass...',10,9),(21,'Ne ernsthaft keine Ahnung',143,10),(22,'WIRKLICH',143,10);
/*!40000 ALTER TABLE `song` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-24 20:48:45
