CREATE DATABASE  IF NOT EXISTS `pharmdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `pharmdb`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: localhost    Database: pharmdb
-- ------------------------------------------------------
-- Server version	5.6.17

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `detail_vente`
--

DROP TABLE IF EXISTS `detail_vente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detail_vente` (
  `num_vente` int(11) NOT NULL AUTO_INCREMENT,
  `date_vente` date DEFAULT NULL,
  `prix_total` decimal(10,0) NOT NULL,
  `num_client` int(11) DEFAULT NULL,
  `num_empl` varchar(20) DEFAULT NULL,
  `heure` time DEFAULT NULL,
  `etat` bit(1) DEFAULT NULL,
  PRIMARY KEY (`num_vente`),
  KEY `num_client` (`num_client`),
  KEY `ffff_idx` (`num_empl`),
  CONSTRAINT `client_keyf` FOREIGN KEY (`num_client`) REFERENCES `clienttbl` (`id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detail_vente`
--

LOCK TABLES `detail_vente` WRITE;
/*!40000 ALTER TABLE `detail_vente` DISABLE KEYS */;
INSERT INTO `detail_vente` VALUES (2,'2019-05-25',1000,1272,'103','17:49:10',''),(3,'2019-05-25',900,1272,'103','17:49:59','');
/*!40000 ALTER TABLE `detail_vente` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-05-25 18:23:35
