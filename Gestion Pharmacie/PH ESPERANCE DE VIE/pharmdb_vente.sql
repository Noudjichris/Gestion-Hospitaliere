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
-- Table structure for table `vente`
--

DROP TABLE IF EXISTS `vente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vente` (
  `num_vente` int(11) NOT NULL,
  `num_medi` varchar(25) NOT NULL,
  `prix_achat` decimal(10,0) NOT NULL,
  `prix_vente` decimal(10,0) NOT NULL,
  `quantite` int(11) NOT NULL,
  `prixTotal` decimal(10,0) NOT NULL,
  `id` int(11) NOT NULL,
  PRIMARY KEY (`num_vente`,`num_medi`,`id`),
  KEY `vente_ibfk_2` (`num_medi`),
  CONSTRAINT `rttt` FOREIGN KEY (`num_medi`) REFERENCES `medicament` (`num_medi`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vente`
--

LOCK TABLES `vente` WRITE;
/*!40000 ALTER TABLE `vente` DISABLE KEYS */;
INSERT INTO `vente` VALUES (2,'58976665',500,500,2,1000,0),(3,'58976665',500,450,2,900,0);
/*!40000 ALTER TABLE `vente` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-05-25 18:23:36
